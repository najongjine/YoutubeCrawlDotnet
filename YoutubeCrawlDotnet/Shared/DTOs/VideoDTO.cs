using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeCrawlDotnet.Shared.Entities;

namespace YoutubeCrawlDotnet.Shared.DTOs
{
  public class VideoDTO
  {
    public long Id { get; set; } = 0;
    public long publicVideoId { get; set; } = 0;
    public long privateVideoId { get; set; } = 0;

    [Required]
    public string VideoId { get; set; } = null;

    public long Order { get; set; } = 1;
    [Required]
    public string Title { get; set; } = null;

    public string Description { get; set; } = null;

    public string ThumbnailDefault { get; set; }
    public string ThumbnailMedium { get; set; }
    public string ThumbnailHigh { get; set; }

    public string ChannelTitle { get; set; }

    public string PublishedAt { get; set; }

    public string ChannelId { get; set; }
    public string Path { get; set; }
    public string FileName { get; set; }
    public string FullPath { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public static VideoDTO CrawledToVideoDTO(Crawled x)
    {
      VideoDTO videoDTO = new VideoDTO();
      videoDTO.Id = x.Id;
      videoDTO.ChannelId = x.ChannelId;
      videoDTO.ChannelTitle = x.ChannelTitle;
        videoDTO.CreatedAt = x.CreatedAt;
      videoDTO.Description = x.Description;
      videoDTO.FileName = x.FileName;
      videoDTO.FullPath = x.FullPath;
      videoDTO.Order = x.Order;
      videoDTO.Path = x.Path;
      videoDTO.PublishedAt = x.PublishedAt;
      videoDTO.ThumbnailDefault = x.ThumbnailDefault;
      videoDTO.ThumbnailHigh = x.ThumbnailHigh;
      videoDTO.ThumbnailMedium = x.ThumbnailMedium;
      videoDTO.Title = x.Title;
      videoDTO.UpdatedAt = x.UpdatedAt;
      videoDTO.VideoId = x.VideoId;
      return videoDTO;
    }
    public static VideoDTO CrawledToVideoDTO(PublicCrawled x)
    {
      VideoDTO videoDTO = new VideoDTO();
      videoDTO.Id = x.Crawled.Id;
      videoDTO.publicVideoId = x.Id;
      videoDTO.ChannelId = x.Crawled.ChannelId;
      videoDTO.ChannelTitle = x.Crawled.ChannelTitle;
      videoDTO.CreatedAt = x.Crawled.CreatedAt;
      videoDTO.Description = x.Crawled.Description;
      videoDTO.FileName = x.Crawled.FileName;
      videoDTO.FullPath = x.Crawled.FullPath;
      videoDTO.Order = x.Order;
      videoDTO.Path = x.Crawled.Path;
      videoDTO.PublishedAt = x.Crawled.PublishedAt;
      videoDTO.ThumbnailDefault = x.Crawled.ThumbnailDefault;
      videoDTO.ThumbnailHigh = x.Crawled.ThumbnailHigh;
      videoDTO.ThumbnailMedium = x.Crawled.ThumbnailMedium;
      videoDTO.Title = x.Crawled.Title;
      videoDTO.UpdatedAt = x.Crawled.UpdatedAt;
      videoDTO.VideoId = x.Crawled.VideoId;
      return videoDTO;
    }
    public static VideoDTO CrawledToVideoDTO(PrivateCrawled x)
    {
      VideoDTO videoDTO = new VideoDTO();
      videoDTO.Id = x.Crawled.Id;
      videoDTO.privateVideoId = x.Id;
      videoDTO.ChannelId = x.Crawled.ChannelId;
      videoDTO.ChannelTitle = x.Crawled.ChannelTitle;
      videoDTO.CreatedAt = x.Crawled.CreatedAt;
      videoDTO.Description = x.Crawled.Description;
      videoDTO.FileName = x.Crawled.FileName;
      videoDTO.FullPath = x.Crawled.FullPath;
      videoDTO.Order = x.Order;
      videoDTO.Path = x.Crawled.Path;
      videoDTO.PublishedAt = x.Crawled.PublishedAt;
      videoDTO.ThumbnailDefault = x.Crawled.ThumbnailDefault;
      videoDTO.ThumbnailHigh = x.Crawled.ThumbnailHigh;
      videoDTO.ThumbnailMedium = x.Crawled.ThumbnailMedium;
      videoDTO.Title = x.Crawled.Title;
      videoDTO.UpdatedAt = x.Crawled.UpdatedAt;
      videoDTO.VideoId = x.Crawled.VideoId;
      return videoDTO;
    }
  }
}
