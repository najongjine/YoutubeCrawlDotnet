using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoutubeCrawlDotnet.Shared.Config
{
  public static class Config
  {
    public static readonly string PhysicalFilePath = "H:/crawled";
    public static readonly string OtherVideoPhysicalFilePath = "H:/OtherCrawled";
    public static readonly string YoutubeApiKey = "YOUR API KEY";
    public static readonly String YoutubeAPIBaseURL = "https://www.googleapis.com/youtube/v3/search?part=snippet&maxResults=1000";
    
    public static readonly string BaseURL = "https://localhost:44328";

  }
}
