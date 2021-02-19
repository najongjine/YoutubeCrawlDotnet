using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeCrawlDotnet.Shared.Entities
{
  public class Crawled
  {
    public long Id { get; set; }

    [Required]
    public string VideoId { get; set; }

    public long Order { get; set; } = 1;
    [Required]
    public string Title { get; set; }

    public string Description { get; set; }

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
  }
}
