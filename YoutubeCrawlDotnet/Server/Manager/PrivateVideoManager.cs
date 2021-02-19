using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeCrawlDotnet.Server.Data;
using YoutubeCrawlDotnet.Shared.DTOs;

namespace YoutubeCrawlDotnet.Server.Manager
{
  public class PrivateVideoManager : IPrivateVideoManager
  {
    private readonly ApplicationDbContext dbContext;
    public PrivateVideoManager(ApplicationDbContext dbContext)
    {
      this.dbContext = dbContext;
    }
    public async Task<List<VideoDTO>> getAllPrivateVideosAsync(int page = 1, int max = 5000)
    {
      var queryablePrivateCrawled = dbContext.PrivateCrawled.Include(x => x.Crawled).AsQueryable();
      queryablePrivateCrawled = queryablePrivateCrawled.OrderBy(x => x.Order);
      queryablePrivateCrawled = queryablePrivateCrawled.Skip((page - 1) * max).Take(max);
      var PrivateVideoListRaw = queryablePrivateCrawled.ToList();

      List<VideoDTO> privateVideoDTO = PrivateVideoListRaw.Select(x => new VideoDTO
      {
        Id = x.Crawled.Id,
        privateVideoId=x.Id,
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
      return privateVideoDTO;
    }
    public async Task<int> getPrivateVideosTotalCount()
    {
      int totalCount = await dbContext.PrivateCrawled.CountAsync();
      return totalCount;
    }

  }
}
