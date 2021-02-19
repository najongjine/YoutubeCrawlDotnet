using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeCrawlDotnet.Shared.DTOs;
using YoutubeCrawlDotnet.Shared.DTOs.OtherVideoPage;

namespace YoutubeCrawlDotnet.Server.Manager
{
  public interface IOtherVideoManager
  {
    public Task<ResponseDTO<long>> CrawlVideosByUrlAsync(CrawlOtherVideoDTO otherVideoDTO);
    Task<ResponseDTO<OtherVideoDTO>> GetOtherVideoByIdAsync(long id);
    Task<ResponseDTO<List<OtherVideoDTO>>> GetAllOtherVideosAsync(int page, int max,string type);
  }
}
