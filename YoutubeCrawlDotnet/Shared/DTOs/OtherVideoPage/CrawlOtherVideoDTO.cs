using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeCrawlDotnet.Shared.DTOs.OtherVideoPage
{
  public class CrawlOtherVideoDTO
  {
    public string VideoUrl { get; set; } = null;
    public string Author { get; set; } = "undefined";
    public string SiteName{get;set;}="undifined";
    public string TagString { get; set; } = "";

    public string Type { get; set; } = "anime";
  }
}
