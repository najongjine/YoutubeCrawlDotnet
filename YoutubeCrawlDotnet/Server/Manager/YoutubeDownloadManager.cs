using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeCrawlDotnet.Server.Data;
using YoutubeCrawlDotnet.Shared.Entities;

namespace YoutubeCrawlDotnet.Server.Manager
{
  public class YoutubeDownloadManager : IYoutubeDownloadManager
  {
    private readonly ApplicationDbContext dbContext;
    public YoutubeDownloadManager(ApplicationDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public Crawled getCrawledByYoutubeVideoId(string youtubeVideoId)
    {
      var crawledVideo = dbContext.Crawled.FirstOrDefault(x => x.VideoId == youtubeVideoId);
      return crawledVideo;
    }

    public async Task<long> getCrawledMaxOrderAsync()
    {
      long maxOrder = dbContext.Crawled.DefaultIfEmpty().Max(x => x == null ? 0 : x.Order);
      return maxOrder;
    }
    public async Task<long> getPublicCrawledMaxOrderAsync()
    {
      long maxOrder = dbContext.PublicCrawled.DefaultIfEmpty().Max(x => x == null ? 0 : x.Order);
      return maxOrder;
    }
    public async Task<long> getPrivateCrawledMaxOrderAsync()
    {
      long maxOrder = dbContext.PrivateCrawled.DefaultIfEmpty().Max(x => x == null ? 0 : x.Order);
      return maxOrder;
    }
    public async Task<Crawled> AddCrawledAsync(Crawled crawled)
    {
      await dbContext.Crawled.AddAsync(crawled);
      await dbContext.SaveChangesAsync();
      return crawled;
    }

    public async Task<PublicCrawled> AddPublicCrawledAsync(PublicCrawled publicCrawled)
    {
      await dbContext.AddAsync<PublicCrawled>(publicCrawled);
      await dbContext.SaveChangesAsync();
      return publicCrawled;
    }

    public async Task<PrivateCrawled> AddPrivateCrawledAsync(PrivateCrawled privateCrawled)
    {
      await dbContext.AddAsync<PrivateCrawled>(privateCrawled);
      await dbContext.SaveChangesAsync();
      return privateCrawled;
    }

    public void InsertToPublic()
    {
      List<Crawled> crawledList = dbContext.Crawled.ToList();
      foreach (var item in crawledList)
      {

        dbContext.PublicCrawled.Add(new PublicCrawled
        {
          CrawledId = item.Id,
          Order = (dbContext.PublicCrawled.DefaultIfEmpty().Max(x => x == null ? 0 : x.Order)) + 1,
        });
        dbContext.SaveChanges();
      }
    }
  }
}
