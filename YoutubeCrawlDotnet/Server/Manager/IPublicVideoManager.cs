using System.Collections.Generic;
using System.Threading.Tasks;
using YoutubeCrawlDotnet.Shared.DTOs;

namespace YoutubeCrawlDotnet.Server.Manager
{
  public interface IPublicVideoManager
  {
    Task<List<VideoDTO>> getAllPublicVideosAsync(int page, int max);

    Task<long> changePublicVideoOrderAsync(long currentOrder, long wantToChangeToThisOrder);
    Task<long> getPublicVideosTotalCount();
  }
}
