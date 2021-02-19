using Microsoft.AspNetCore.Mvc;
using System.Linq;
using YoutubeCrawlDotnet.Server.Data;
using YoutubeCrawlDotnet.Shared.Config;

namespace YoutubeCrawlDotnet.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StreamingController : ControllerBase
  {
    private readonly ApplicationDbContext dbContext;
    public StreamingController(ApplicationDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    [HttpGet("{videoId}")]
    public FileResult getFileByVideoId(string videoId = null)
    {
      if (string.IsNullOrWhiteSpace(videoId))
      {
        return PhysicalFile($"{Config.PhysicalFilePath}/novideo.webm", "application/octet-stream", enableRangeProcessing: true);
      }
      var crawledVideo = dbContext.Crawled.FirstOrDefault(x => x.VideoId == videoId);
      return PhysicalFile($"{Config.PhysicalFilePath}/{crawledVideo.FullPath}", "application/octet-stream", enableRangeProcessing: true);
    }
    [HttpGet("private/{id}")]
    public FileResult getFileByVideoId(long id= 0)
    {
      if (id<1)
      {
        return PhysicalFile($"{Config.PhysicalFilePath}/novideo.webm", "application/octet-stream", enableRangeProcessing: true);
      }
      var crawledVideo = dbContext.Crawled.FirstOrDefault(x => x.Id == id);
      return PhysicalFile($"{Config.PhysicalFilePath}/{crawledVideo.FullPath}", "application/octet-stream", enableRangeProcessing: true);
    }
    [HttpGet("othervideo/{id}")]
    public FileResult getOtherVideoFileByVideoId(long id = 0)
    {
      if (id < 1)
      {
        return PhysicalFile($"{Config.PhysicalFilePath}/novideo.webm", "application/octet-stream", enableRangeProcessing: true);
      }
      var crawledVideo = dbContext.OtherCrawledVideos.FirstOrDefault(x => x.Id == id);
      return PhysicalFile($"{Config.OtherVideoPhysicalFilePath}/{crawledVideo.FullPath}", "application/octet-stream", enableRangeProcessing: true);
    }
    [HttpGet("othervideoimg/{id}")]
    public FileResult getOtherVideoImgFileByVideoId(long id = 0)
    {
      if (id < 1)
      {
        return PhysicalFile($"{Config.PhysicalFilePath}/noimage.png", "application/octet-stream", enableRangeProcessing: true);
      }
      var crawledVideo = dbContext.OtherCrawledVideos.FirstOrDefault(x => x.Id == id);
      return PhysicalFile($"{Config.OtherVideoPhysicalFilePath}/{crawledVideo.Thumbnail}", "img/*", enableRangeProcessing: true);
    }
  }
}
