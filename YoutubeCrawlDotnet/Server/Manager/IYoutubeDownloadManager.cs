using System.Threading.Tasks;
using YoutubeCrawlDotnet.Shared.Entities;

namespace YoutubeCrawlDotnet.Server.Manager
{
  public interface IYoutubeDownloadManager
  {
    Task<Crawled> AddCrawledAsync(Crawled crawled);
    Task<PrivateCrawled> AddPrivateCrawledAsync(PrivateCrawled privateCrawled);
    Task<PublicCrawled> AddPublicCrawledAsync(PublicCrawled publicCrawled);
    Crawled getCrawledByYoutubeVideoId(string youtubeVideoId);
    Task<long> getCrawledMaxOrderAsync();
    Task<long> getPrivateCrawledMaxOrderAsync();
    Task<long> getPublicCrawledMaxOrderAsync();
    public void InsertToPublic();
  }
}
