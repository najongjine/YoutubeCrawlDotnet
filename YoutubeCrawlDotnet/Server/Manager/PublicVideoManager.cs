using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeCrawlDotnet.Server.Data;
using YoutubeCrawlDotnet.Shared.DTOs;

namespace YoutubeCrawlDotnet.Server.Manager
{
  public class PublicVideoManager : IPublicVideoManager
  {
    private readonly ApplicationDbContext dbContext;
    public PublicVideoManager(ApplicationDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public async Task<List<VideoDTO>> getAllPublicVideosAsync(int page = 1, int max = 5000)
    {
      var queryablePublicCrawled = dbContext.PublicCrawled.Include(x => x.Crawled).AsQueryable();
      queryablePublicCrawled = queryablePublicCrawled.OrderBy(x => x.Order);
      queryablePublicCrawled = queryablePublicCrawled.Skip((page - 1) * max).Take(max);
      var publicVideoListRaw = queryablePublicCrawled.ToList();

      List<VideoDTO> publicVideoDTO = publicVideoListRaw.Select(x => new VideoDTO
      {
        Id = x.Crawled.Id,
        publicVideoId=x.Id,
        ChannelId = x.Crawled.ChannelId,
        ChannelTitle = x.Crawled.ChannelTitle,
        CreatedAt = x.Crawled.CreatedAt,
        Description = x.Crawled.Description,
        FileName = x.Crawled.FileName,
        FullPath = x.Crawled.FullPath,
        Order = x.Order,
        Path = x.Crawled.Path,
        PublishedAt = x.Crawled.PublishedAt,
        ThumbnailDefault = x.Crawled.ThumbnailDefault,
        ThumbnailHigh = x.Crawled.ThumbnailHigh,
        ThumbnailMedium = x.Crawled.ThumbnailMedium,
        Title = x.Crawled.Title,
        UpdatedAt = x.Crawled.UpdatedAt,
        VideoId = x.Crawled.VideoId,
      }).ToList();
      return publicVideoDTO;
    }
    public async Task<long> getPublicVideosTotalCount()
    {
      int totalCount = await dbContext.PublicCrawled.CountAsync();
      return totalCount;
    }

    public async Task<long> changePublicVideoOrderAsync(long currentOrder=0,long wantToChangeToThisOrder=0)
    {
      if (wantToChangeToThisOrder < 1 || currentOrder < 1)
      {
        return -1;
      }
      var targetPublicVideo = await dbContext.PublicCrawled.FirstOrDefaultAsync(x => x.Order == currentOrder);
      var exOrder=await dbContext.PublicCrawled.FirstOrDefaultAsync(x => x.Order == wantToChangeToThisOrder);
      if (exOrder == null)
      {
        targetPublicVideo.Order = wantToChangeToThisOrder;
        await dbContext.SaveChangesAsync();
      }
      var changeOrderList= dbContext.PublicCrawled.Where(x => x.Order >= wantToChangeToThisOrder);
      await changeOrderList.ForEachAsync(x => x.Order += 1);
      targetPublicVideo.Order = wantToChangeToThisOrder;
      await dbContext.SaveChangesAsync();
      return wantToChangeToThisOrder;
    }
  }
}
