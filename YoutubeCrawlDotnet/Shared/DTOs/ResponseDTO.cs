using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeCrawlDotnet.Shared.DTOs
{
  public class ResponseDTO<T>
  {
    /*
    public ResponseDTO(bool success, T data, string responseMessage, int totalData)
    {
      this.success = success;
      this.data = data;
      this.responseMessage = responseMessage;
      this.totalData = totalData;
    }
    */
    public bool success { get; set; } = true;

    public int code { get; set; } = 0;
    public T data { get; set; }
    public string responseMessage { get; set; } = null;

    public long totalData { get; set; } = 1;
    public int itemsPerPage { get; set; } = 50;
    public int currentPage { get; set; } = 1;

    
  }

}
