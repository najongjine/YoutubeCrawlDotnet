using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeCrawlDotnet.Shared.DTOs;

namespace YoutubeCrawlDotnet.Server.Manager
{
  public interface IPrivateVideoManager
  {
    Task<List<VideoDTO>> getAllPrivateVideosAsync(int page = 1, int max = 5000);
    Task<int> getPrivateVideosTotalCount();
  }
}
