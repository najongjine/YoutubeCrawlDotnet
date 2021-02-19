using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeCrawlDotnet.Server.Data;
using YoutubeCrawlDotnet.Server.Manager;
using YoutubeCrawlDotnet.Shared.DTOs;
using YoutubeCrawlDotnet.Shared.DTOs.PublicWatchPage;

namespace YoutubeCrawlDotnet.Server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class PublicVideosController : ControllerBase
  {
    private readonly ApplicationDbContext dbContext;
    private readonly IPublicVideoManager publicVideoManager;
    public PublicVideosController(IPublicVideoManager publicVideoManager, ApplicationDbContext dbContext)
    {
      this.publicVideoManager = publicVideoManager;
      this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ResponseDTO<List<VideoDTO>>> PublicVideosAsync([FromQuery] int page = 1, [FromQuery] int max = 50000)
    {
      List<VideoDTO> publicVideoDTOList = await publicVideoManager.getAllPublicVideosAsync(page, max);
      return new ResponseDTO<List<VideoDTO>>
      {
        currentPage = page,
        data = publicVideoDTOList,
        itemsPerPage = max,
        success = true,
        totalData = await publicVideoManager.getPublicVideosTotalCount()
      };
    }

    [HttpGet("{videoId}")]
    public async Task<ResponseDTO<VideoDTO>> Get(string videoId)
    {
      if (string.IsNullOrWhiteSpace(videoId))
      {
        return new ResponseDTO<VideoDTO>
        {
          code = -1,
          success = false,
          responseMessage = "no video id"
        };
      }
      var publicVideo = dbContext.PublicCrawled.Include(x => x.Crawled).FirstOrDefault(x => x.Crawled.VideoId == videoId);
      VideoDTO videoDTO = VideoDTO.CrawledToVideoDTO(publicVideo);
      return new ResponseDTO<VideoDTO>
      {
        success = true,
        data = videoDTO
      };
    }

    [HttpGet("next/{order}")]
    public async Task<ResponseDTO<VideoDTO>> GetNextOrderPublicVideoAsync(int order = 0)
    {
      if (order < 1)
      {
        return new ResponseDTO<VideoDTO>
        {
          code = -1,
          success = false,
          responseMessage = "no video id"
        };
      }
      var publicVideo = (from pb in dbContext.PublicCrawled
                         where pb.Order > order
                         orderby pb.Order
                         select pb).Include(x => x.Crawled).FirstOrDefault();
      if (publicVideo == null)
      {
        return new ResponseDTO<VideoDTO>
        {
          code = 0,
          success = false,
          responseMessage = "end of videos"
        };
      }

      VideoDTO videoDTO = VideoDTO.CrawledToVideoDTO(publicVideo);
      return new ResponseDTO<VideoDTO>
      {
        success = true,
        data = videoDTO
      };
    }
    [HttpPut("changeorder")]
    public async Task<ResponseDTO<long>> ChangePublicVideoOrder([FromBody] ChangeOrderDTO changeOrderDTO)
    {
      if (changeOrderDTO.currentOrder == changeOrderDTO.wantToChangeToThisOrder)
      {
        return new ResponseDTO<long> { success = false, data = -1, responseMessage = "invalid order" }; 
      }
      if (changeOrderDTO.wantToChangeToThisOrder < 1 || changeOrderDTO.currentOrder < 1)
      {
        return new ResponseDTO<long> { success = false, data = -1, responseMessage = "invalid order" };
      }
      long changedOrder = await publicVideoManager.changePublicVideoOrderAsync(changeOrderDTO.currentOrder, changeOrderDTO.wantToChangeToThisOrder);
      if (changedOrder < 1)
      {
        return new ResponseDTO<long> { success = false, data = -1, responseMessage="invalid order"};
      }
      return new ResponseDTO<long> { success = true, data = changedOrder, };
    }
  }
}
