using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
      var claimList = User.Claims.ToList();
      //매칭되는게 없으면 null
      var role = claimList.FirstOrDefault(x => x.Value.ToLower().Contains("something1".ToLower()) || x.Value.ToLower().Contains("something2"));
      foreach (var claim in claimList)
      {
        Console.WriteLine("## claim : " + claim.Value);//jwt 가 괜찬으면 role이 담겨져있다. 이상하면 null값
      }
      var userName = User.Identity.Name;
      Console.WriteLine("## userName: " + userName); //jwt 가 괜찬으면 Email 이 담겨져있다. 이상하면 null값
      var isAuthenticated = User.Identity.IsAuthenticated;
      Console.WriteLine("## IsAuthenticated : " + isAuthenticated); // jwt 가 괜찬으면 true, 이상하면 false
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
