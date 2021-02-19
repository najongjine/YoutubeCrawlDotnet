using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeCrawlDotnet.Shared.DTOs.PublicWatchPage
{
  public class ChangeOrderDTO
  {
    public long currentOrder { get; set; } = 0;
    public long wantToChangeToThisOrder { get; set; } = 0;
  }
}
