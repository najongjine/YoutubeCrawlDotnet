using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeCrawlDotnet.Shared.DTOs;

namespace YoutubeCrawlDotnet.Client.Youtube
{
  public interface IYoutubeAPI
  {
    Task<ResponseDTO<List<YouTubeVideoDTO>>> SearchYoutubeByKeyword(string searchKeyword);
  }
}
