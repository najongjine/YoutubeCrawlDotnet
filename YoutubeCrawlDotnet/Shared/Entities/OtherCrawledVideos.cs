using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeCrawlDotnet.Shared.Entities
{
  public class OtherCrawledVideos
  {
    public long Id { get; set; }
    [Required]
    public string VideoUrl { get; set; }
    public long order { get; set; } = 0;

    [Required]
    public string Title { get; set; }

    public string Author { get; set; } = null;

    public string SiteName { get; set; } = null;

    public string TagString { get; set; } = null;

    public string Thumbnail { get; set; } = null;
    public string Type { get; set; } = "anime";

    public string Path { get; set; } = null;
    public string FileName { get; set; } = null;
    public string FullPath { get; set; } = null;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
  }
}
