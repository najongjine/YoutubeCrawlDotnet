using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeCrawlDotnet.Shared.DTOs
{
  public class YouTubeVideoDTO
  {
    public string VideoId { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Thumbnail { get; set; } = "";
    public string Picture { get; set; } = "";
    public string PublishedAt { get; set; } = "";
    public string ChannelId { get; set; } = "";
    
  }
}
