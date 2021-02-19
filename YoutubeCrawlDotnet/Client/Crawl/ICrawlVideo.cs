using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeCrawlDotnet.Shared.DTOs;

namespace YoutubeCrawlDotnet.Client.Crawl
{
  public interface ICrawlVideo
  {
    Task<ResponseDTO<string>> CrawlYoutube(string videoId = null);
  }
}
