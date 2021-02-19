using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeCrawlDotnet.Shared.Entities
{
  public class PublicCrawled
  {
    public long Id { get; set; }
    [Required]
    public long CrawledId { get; set; }
    public long Order { get; set; } = 1;

    public Crawled Crawled { get; set; }
  }
}
