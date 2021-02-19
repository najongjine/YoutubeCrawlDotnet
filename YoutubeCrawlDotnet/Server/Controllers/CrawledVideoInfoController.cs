using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using YoutubeCrawlDotnet.Server.Data;
using YoutubeCrawlDotnet.Shared.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YoutubeCrawlDotnet.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CrawledVideoInfoController : ControllerBase
  {
    private readonly ApplicationDbContext dbContext;
    public CrawledVideoInfoController(ApplicationDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    // GET: api/<ValuesController>
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
      var crawledVideo = dbContext.Crawled.FirstOrDefault(x => x.VideoId == videoId);
      VideoDTO videoDTO = VideoDTO.CrawledToVideoDTO(crawledVideo);
      return new ResponseDTO<VideoDTO>
      {
        success = true,
        data = videoDTO
      };
    }


  }
}
