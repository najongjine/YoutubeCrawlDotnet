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
    public static readonly string YoutubeApiKey = "aAIzaSyAPB2pk6mIb0Ag1H5qxymTyTHtkZtRIvg8a";
    public static readonly String YoutubeAPIBaseURL = "https://www.googleapis.com/youtube/v3/search?part=snippet&maxResults=1000";
  }
}
