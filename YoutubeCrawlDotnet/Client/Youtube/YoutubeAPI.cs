using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using YoutubeCrawlDotnet.Shared.Config;
using YoutubeCrawlDotnet.Shared.DTOs;

namespace YoutubeCrawlDotnet.Client.Youtube
{
  public class YoutubeAPI : IYoutubeAPI
  {
    private HttpClient client;
    public YoutubeAPI( HttpClient client)
    {
      this.client = new HttpClient();
    }
    

    public async Task<ResponseDTO<List<YouTubeVideoDTO>>> SearchYoutubeByKeyword(string searchKeyword)
    {
      List<YouTubeVideoDTO> youtubeList = new List<YouTubeVideoDTO>();
      string url = $"{Config.YoutubeAPIBaseURL}&key={Config.YoutubeApiKey}&part=snippet&maxResults=1000&q={HttpUtility.UrlEncode(searchKeyword)}";
      HttpResponseMessage response = await client.GetAsync(url);
      if (!response.IsSuccessStatusCode)
      {
        return new ResponseDTO<List<YouTubeVideoDTO>>
        {
          data = youtubeList,
          success = false,
          responseMessage = response.Content.ReadAsStringAsync().Result
        };

      }
      
      string data = await response.Content.ReadAsStringAsync();
      var youtubeJObject = JObject.Parse(data);
      var items = youtubeJObject.SelectTokens("items").Children();
      //var data2= JsonConvert.DeserializeObject<object>(data);
      foreach (var item in items)
      {
        string videoId = item.SelectToken("id").SelectToken("videoId")!=null? item.SelectToken("id").SelectToken("videoId").ToString():null;
        string publishedAt = item.SelectToken("snippet").SelectToken("publishedAt")!=null? item.SelectToken("snippet").SelectToken("publishedAt").ToString():null;
        string description = item.SelectToken("snippet").SelectToken("description").ToString()!=null? item.SelectToken("snippet").SelectToken("description").ToString():null;
        string channelId = item.SelectToken("snippet").SelectToken("channelId")!=null? item.SelectToken("snippet").SelectToken("channelId").ToString():null;
        string title = item.SelectToken("snippet").SelectToken("title").ToString()!=null? item.SelectToken("snippet").SelectToken("title").ToString():null;
        string thumbnailDefault = item.SelectToken("snippet").SelectToken("thumbnails").SelectToken("default").SelectToken("url")!=null? item.SelectToken("snippet").SelectToken("thumbnails").SelectToken("default").SelectToken("url").ToString():null;
        string thumbnailMedium = item.SelectToken("snippet").SelectToken("thumbnails").SelectToken("medium").SelectToken("url")!=null? item.SelectToken("snippet").SelectToken("thumbnails").SelectToken("medium").SelectToken("url").ToString():null;
        string thumbnailHigh = item.SelectToken("snippet").SelectToken("thumbnails").SelectToken("high").SelectToken("url")!=null? item.SelectToken("snippet").SelectToken("thumbnails").SelectToken("high").SelectToken("url").ToString():null;
        string thumbnail = thumbnailDefault ?? thumbnailMedium ?? thumbnailHigh;
        if(videoId!=null && title!= null)
        {
          YouTubeVideoDTO youtubeDTO = new YouTubeVideoDTO
          {
            ChannelId = channelId,
            Description = description,
            PublishedAt = publishedAt,
            Thumbnail = thumbnail,
            Title = title,
            VideoId = videoId
          };
          youtubeList.Add(youtubeDTO);
        }
        
      }
      ResponseDTO<List<YouTubeVideoDTO>> responseDTO = new ResponseDTO<List<YouTubeVideoDTO>> {
        data=youtubeList,
        itemsPerPage=1000,
        success=true,
        totalData=youtubeList.Count
      };
      return responseDTO;
    }

  }
}
