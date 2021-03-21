using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Transactions;
using YoutubeCrawlDotnet.Server.Helper;
using YoutubeCrawlDotnet.Server.Manager;
using YoutubeCrawlDotnet.Shared.Config;
using YoutubeCrawlDotnet.Shared.DTOs;
using YoutubeCrawlDotnet.Shared.Entities;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;

namespace YoutubeCrawlDotnet.Server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class YoutubeDownloadController : ControllerBase
  {
    private readonly CleanString CleanString;
    private readonly IYoutubeDownloadManager youtubeDownloadManager;
    public YoutubeDownloadController(CleanString CleanString, IYoutubeDownloadManager youtubeDownloadManager)
    {
      this.CleanString = CleanString;
      this.youtubeDownloadManager = youtubeDownloadManager;
    }

    [HttpGet("{youtubeUrlId}")]
    public async Task<ResponseDTO<string>> Get(string youtubeUrlId = "", [FromQuery] string inputStr = "undefined", [FromQuery] bool bMakeItPrivate = false)
    {
      var crawledVideo = youtubeDownloadManager.getCrawledByYoutubeVideoId(youtubeUrlId);
      if (crawledVideo != null)
      {
        return new ResponseDTO<string>
        {
          success = false,
          data = null,
          responseMessage = "alrdy Crawled this video"
        };
      }
      string downloadPath = Config.PhysicalFilePath;
      var youtube = new YoutubeClient();

      string youtubeLink = $"https://www.youtube.com/watch?v={youtubeUrlId}";
      var videoInfo = await youtube.Videos.GetAsync(youtubeLink);
      string publishedAt = videoInfo.UploadDate.ToString();
      string title = CleanString.RemoveEmojisSChars(videoInfo.Title);
      string author = CleanString.RemoveEmojisSChars(videoInfo.Author);
      string description = CleanString.RemoveEmojisSChars(videoInfo.Description);
      string channelId = videoInfo.ChannelId;
      string thumbnailDefault = videoInfo.Thumbnails.StandardResUrl;
      string thumbnailMedium = videoInfo.Thumbnails.MediumResUrl;
      string thumbnailHigh = videoInfo.Thumbnails.HighResUrl;
      var duration = videoInfo.Duration;

      var streamManifest = await youtube.Videos.Streams.GetManifestAsync(youtubeUrlId);

      var videoStreamInfo = streamManifest.GetVideoOnly().FirstOrDefault(s => s.VideoQualityLabel == "1080p60")
        ?? streamManifest.GetVideo().WithHighestVideoQuality();

      if (System.IO.File.Exists($"{Config.PhysicalFilePath}/{inputStr.Trim()}/{title}.{videoStreamInfo.Container}") ||
        System.IO.File.Exists($"{Config.PhysicalFilePath}/{inputStr.Trim()}/{title}.{videoStreamInfo.Container}..stream-1.tmp") ||
        System.IO.File.Exists($"{Config.PhysicalFilePath}/{inputStr.Trim()}/{title}.{videoStreamInfo.Container}..stream-2.tmp")
        )
      {
        return new ResponseDTO<string>
        {
          success = false,
          data = null,
          responseMessage = "alrdy downloading"
        };
      }

      var audioStreamInfo = streamManifest.GetAudio().WithHighestBitrate();

      var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };
      //var streamInfo = streamManifest.GetMuxed().WithHighestVideoQuality();
      if (streamInfos != null)
      {
        if (!System.IO.Directory.Exists($"{downloadPath}/{inputStr.Trim()}"))
        {
          System.IO.Directory.CreateDirectory($"{downloadPath}/{inputStr.Trim()}");
        }
        // Get the actual stream
        //var stream = await youtube.Videos.Streams.GetAsync(streamInfos);

        // Download the stream to file
        //await youtube.Videos.Streams.DownloadAsync(streamInfos, $"{downloadPath}/{title}.{streamInfo.Container}");
        string timeStr = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        string path = $"{inputStr.Trim()}";
        string fileName = $"{timeStr}_{title}.{videoStreamInfo.Container}";
        string fullPath = $"{path}/{fileName}";
        await youtube.Videos.DownloadAsync(streamInfos, new ConversionRequestBuilder($"{downloadPath}/{fullPath}").Build());

        try
        {
          Crawled crawled = new Crawled
          {
            ChannelId = channelId,
            CreatedAt = DateTime.UtcNow,
            Description = description,
            FileName = fileName,
            FullPath = fullPath,
            Path = path,
            PublishedAt = publishedAt,
            ThumbnailDefault = thumbnailDefault,
            ThumbnailHigh = thumbnailHigh,
            ThumbnailMedium = thumbnailMedium,
            Title = title,
            UpdatedAt = DateTime.UtcNow,
            VideoId = youtubeUrlId,
            Order = (await youtubeDownloadManager.getCrawledMaxOrderAsync()) + 1
          };
          using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
          {
            crawled = await youtubeDownloadManager.AddCrawledAsync(crawled);
            if (bMakeItPrivate)
            {
              await youtubeDownloadManager.AddPrivateCrawledAsync(new PrivateCrawled
              {
                CrawledId = crawled.Id,
                Order = await youtubeDownloadManager.getPrivateCrawledMaxOrderAsync() + 1
              });
            }
            else if (!bMakeItPrivate)
            {
              await youtubeDownloadManager.AddPublicCrawledAsync(new PublicCrawled
              {
                CrawledId = crawled.Id,
                Order = await youtubeDownloadManager.getPublicCrawledMaxOrderAsync() + 1
              });
            }

            scope.Complete();
          }
        }
        catch (Exception e)
        {
          Console.WriteLine($"!!! {e.Message}");
          Console.WriteLine($"!!! {e.InnerException}");
          return new ResponseDTO<string>
          {
            success = false,
            data = null,
            responseMessage = e.Message
          };
        }
      }
      return new ResponseDTO<string>
      {
        success = true,
        data = youtubeUrlId,
      };
    }

    [HttpGet("youtube")]
    public async Task youtubeAPI()
    {
      HttpClient client = new HttpClient();
      string url = $"{Config.YoutubeAPIBaseURL}&key={Config.YoutubeApiKey}&part=snippet&maxResults=1000&q=aaa";
      HttpResponseMessage response = await client.GetAsync(url);
      if (response.IsSuccessStatusCode)
      {
        string data = await response.Content.ReadAsStringAsync();
        var youtubeJObject = JObject.Parse(data);
        var items = youtubeJObject.SelectTokens("items").Children();
        //var data2= JsonConvert.DeserializeObject<object>(data);
        foreach (var item in items)
        {
          string videoId = item.SelectToken("id").SelectToken("videoId").ToString();
          string publishedAt = item.SelectToken("snippet").SelectToken("publishedAt").ToString();
          string description = item.SelectToken("snippet").SelectToken("description").ToString();
          string channelId = item.SelectToken("snippet").SelectToken("channelId").ToString();
          string title = item.SelectToken("snippet").SelectToken("title").ToString();
          string thumbnailDefault = item.SelectToken("snippet").SelectToken("thumbnails").SelectToken("default").SelectToken("url").ToString();
          string thumbnailMedium = item.SelectToken("snippet").SelectToken("thumbnails").SelectToken("medium").SelectToken("url").ToString();
          string thumbnailHigh = item.SelectToken("snippet").SelectToken("thumbnails").SelectToken("high").SelectToken("url").ToString();
          string thumbnail = thumbnailDefault ?? thumbnailMedium ?? thumbnailHigh;
          YouTubeVideoDTO youtubeDTO = new YouTubeVideoDTO
          {
            ChannelId = channelId,
            Description = description,
            PublishedAt = publishedAt,
            Thumbnail = thumbnail,
            Title = title,
            VideoId = videoId
          };
        }
      }
      else
      {
        throw new Exception(await response.Content.ReadAsStringAsync());
      }
    }


    [HttpGet("v2/{youtubeUrlId}")]
    public async Task<ResponseDTO<string>> SaveVideoToDisk(string youtubeUrlId = null, [FromQuery] string inputStr = "undefined", [FromQuery] bool bMakeItPrivate = false)
    {
      inputStr=CleanString.RemoveEmojisSChars(inputStr);
      string videoId = youtubeUrlId;
      string url = $"https://www.youtube.com/watch?v={videoId}";
      var crawledVideo = youtubeDownloadManager.getCrawledByYoutubeVideoId(videoId);
      if (crawledVideo != null)
      {
        return new ResponseDTO<string>
        {
          success = false,
          data = null,
          responseMessage = "alrdy Crawled this video"
        };
      }
      if (!System.IO.Directory.Exists($"{Config.PhysicalFilePath}/{inputStr}"))
      {
        System.IO.Directory.CreateDirectory($"{Config.PhysicalFilePath}/{inputStr}");
      }
      var ytdl = new YoutubeDL();
      // set the path of the youtube-dl and FFmpeg if they're not in PATH or current directory
      ytdl.YoutubeDLPath = $"H:/MyProjects/youtube-dl.exe";
      ytdl.FFmpegPath = $"C:/ffmpeg/bin/ffmpeg.exe";
      // optional: set a different download folder
      ytdl.OutputFolder = $"{Config.PhysicalFilePath}/{inputStr}";
      ytdl.RestrictFilenames = true;
      // download a video
      var data = await ytdl.RunVideoDataFetch(url:url);
      
      string title=data.Data.Title;
      string publishedAt = data.Data.UploadDate.ToString();
      string author = CleanString.RemoveEmojisSChars(data.Data.Uploader);
      string description = data.Data.Description;
      string channelId = data.Data.ChannelID;
      string thumbnailDefault = data.Data.Thumbnail;
      var res = await ytdl.RunVideoDownload(url:url);
      // the path of the downloaded file
      string path = res.Data;
      var splitedPath = path.Split("\\");
      string folder = $"{inputStr}";
      string fileName = splitedPath[splitedPath.Length - 1];
      string fullPath = $"{inputStr}/{fileName}";

      string timeStr = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

      string newFileName = timeStr + fileName;
      string newFullPath = $"{folder}/{newFileName}";

      System.IO.File.Move($"{Config.PhysicalFilePath}/{fullPath}", $"{Config.PhysicalFilePath}/{newFullPath}");

      try
      {
        Crawled crawled = new Crawled
        {
          ChannelId = channelId,
          CreatedAt = DateTime.UtcNow,
          Description = description,
          FileName = newFileName,
          FullPath = newFullPath,
          Path = folder,
          PublishedAt = publishedAt,
          ThumbnailDefault = thumbnailDefault,
          Title = title,
          UpdatedAt = DateTime.UtcNow,
          VideoId = videoId,
          Order = (await youtubeDownloadManager.getCrawledMaxOrderAsync()) + 1
        };
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
          crawled = await youtubeDownloadManager.AddCrawledAsync(crawled);
          if (bMakeItPrivate)
          {
            await youtubeDownloadManager.AddPrivateCrawledAsync(new PrivateCrawled
            {
              CrawledId = crawled.Id,
              Order = await youtubeDownloadManager.getPrivateCrawledMaxOrderAsync() + 1
            });
          }
          else if (!bMakeItPrivate)
          {
            await youtubeDownloadManager.AddPublicCrawledAsync(new PublicCrawled
            {
              CrawledId = crawled.Id,
              Order = await youtubeDownloadManager.getPublicCrawledMaxOrderAsync() + 1
            });
          }

          scope.Complete();
        }
      }
      catch (Exception e)
      {
        Console.WriteLine($"!!! {e.Message}");
        Console.WriteLine($"!!! {e.InnerException}");
        return new ResponseDTO<string>
        {
          success = false,
          data = null,
          responseMessage = e.Message
        };
      }


      return new ResponseDTO<string>
      {
        success = true,
        data = videoId,
      };
    }
    
    /*
    [HttpGet("inserttopublic")]
    public async Task InsertToPublic()
    {
      youtubeDownloadManager.InsertToPublic();
    }
    */

  }
}
