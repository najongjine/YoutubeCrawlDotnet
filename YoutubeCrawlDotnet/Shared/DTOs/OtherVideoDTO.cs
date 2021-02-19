using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeCrawlDotnet.Shared.Entities;

namespace YoutubeCrawlDotnet.Shared.DTOs
{
  public class OtherVideoDTO
  {
    public long Id { get; set; }
    public string VideoUrl { get; set; }
    public long order { get; set; } = 0;

    public string Title { get; set; } = null;

    public string Author { get; set; } = null;

    public string SiteName { get; set; } = null;

    public string TagString { get; set; } = null;

    public string Thumbnail { get; set; } = null;

    public string Type { get; set; } = "anime";

    public string Path { get; set; } = null;
    public string FileName { get; set; } = null;
    public string FullPath { get; set; } = null;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static OtherVideoDTO OtherCrawledVideosToOtherVideoDTO(OtherCrawledVideos otherCrawledVideos)
    {
      OtherVideoDTO otherVideoDTO = new OtherVideoDTO();
      otherVideoDTO.Author = otherCrawledVideos.Author;
      otherVideoDTO.CreatedAt = otherCrawledVideos.CreatedAt;
      otherVideoDTO.FileName = otherCrawledVideos.FileName;
      otherVideoDTO.FullPath = otherCrawledVideos.FullPath;
      otherVideoDTO.Id = otherCrawledVideos.Id;
      otherVideoDTO.UpdatedAt = otherCrawledVideos.UpdatedAt;
      otherVideoDTO.order = otherCrawledVideos.order;
      otherVideoDTO.Path = otherCrawledVideos.Path;
      otherVideoDTO.SiteName = otherCrawledVideos.SiteName;
      otherVideoDTO.TagString = otherCrawledVideos.TagString;
      otherVideoDTO.Thumbnail = otherCrawledVideos.Thumbnail;
      otherVideoDTO.Title = otherCrawledVideos.Title;
      otherVideoDTO.VideoUrl = otherCrawledVideos.VideoUrl;
      otherVideoDTO.Type = otherCrawledVideos.Type;

      return otherVideoDTO;
    }
  }

  
}
