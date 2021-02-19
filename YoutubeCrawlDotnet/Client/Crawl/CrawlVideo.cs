using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using YoutubeCrawlDotnet.Shared.Config;
using YoutubeCrawlDotnet.Shared.DTOs;

namespace YoutubeCrawlDotnet.Client.Crawl
{
  public class CrawlVideo:ICrawlVideo
  {
    public string url = "api/YoutubeDownload";
    private readonly HttpClient client;
    public CrawlVideo(HttpClient client)
    {
      this.client = new HttpClient();
    }

    public async Task<ResponseDTO<string>> CrawlYoutube(string videoId = "")
    {
      url = $"{url}/{videoId}";
      if (string.IsNullOrWhiteSpace(videoId))
      {
        return new ResponseDTO<string>
        {
          success = false,
          data = null,
          responseMessage = "no video id"
        };
      }
      var response = await client.GetFromJsonAsync<ResponseDTO<string>>(url);
      return response;
      
      

    }
  }
}
