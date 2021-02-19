using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeCrawlDotnet.Server.Data;
using YoutubeCrawlDotnet.Server.Manager;
using YoutubeCrawlDotnet.Shared.DTOs;

namespace YoutubeCrawlDotnet.Server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class PrivateVideosController : Controller
  {
    private readonly ApplicationDbContext dbContext;
    private readonly IPrivateVideoManager privateVideoManager;
    public PrivateVideosController(IPrivateVideoManager privateVideoManager, ApplicationDbContext dbContext)
    {
      this.privateVideoManager = privateVideoManager;
      this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ResponseDTO<List<VideoDTO>>> PrivateVideosAsync([FromQuery] int page = 1, [FromQuery] int max = 50000)
    {
      List<VideoDTO> privateVideoDTOList = await privateVideoManager.getAllPrivateVideosAsync(page, max);
      return new ResponseDTO<List<VideoDTO>>
      {
        currentPage = page,
        data = privateVideoDTOList,
        itemsPerPage = max,
        success = true,
        totalData = await privateVideoManager.getPrivateVideosTotalCount()
      };
    }

    [HttpGet("{id}")]
    public async Task<ResponseDTO<VideoDTO>> Get(int id = 0)
    {
      if (id < 1)
      {
        return new ResponseDTO<VideoDTO>
        {
          code = -1,
          success = false,
          responseMessage = "no id"
        };
      }
      var privateVideo = dbContext.PrivateCrawled.Include(x => x.Crawled).FirstOrDefault(x => x.Crawled.Id == id);
      VideoDTO videoDTO = VideoDTO.CrawledToVideoDTO(privateVideo);
      return new ResponseDTO<VideoDTO>
      {
        success = true,
        data = videoDTO
      };
    }

    [HttpGet("next/{order}")]
    public async Task<ResponseDTO<VideoDTO>> GetNextOrderPrivateVideoAsync(int order = 0)
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
      var privateVideo = (from pb in dbContext.PrivateCrawled
                          where pb.Order > order
                          orderby pb.Order
                          select pb).Include(x => x.Crawled).FirstOrDefault();
      if (privateVideo == null)
      {
        return new ResponseDTO<VideoDTO>
        {
          code = 0,
          success = false,
          responseMessage = "end of videos"
        };
      }

      VideoDTO videoDTO = VideoDTO.CrawledToVideoDTO(privateVideo);
      return new ResponseDTO<VideoDTO>
      {
        success = true,
        data = videoDTO
      };
    }
  }
}
