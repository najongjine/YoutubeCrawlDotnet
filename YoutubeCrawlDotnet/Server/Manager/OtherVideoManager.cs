using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xabe.FFmpeg;
using YoutubeCrawlDotnet.Server.Data;
using YoutubeCrawlDotnet.Shared.Config;
using YoutubeCrawlDotnet.Shared.DTOs;
using YoutubeCrawlDotnet.Shared.DTOs.OtherVideoPage;
using YoutubeCrawlDotnet.Shared.Entities;
using YoutubeDLSharp;

namespace YoutubeCrawlDotnet.Server.Manager
{
  public class OtherVideoManager : IOtherVideoManager
  {
    private readonly ApplicationDbContext dbContext;
    public OtherVideoManager(ApplicationDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public async Task<ResponseDTO<List<OtherVideoDTO>>> GetAllOtherVideosAsync(int page,int max,string type)
    {
      var queryableOtherCrawled = dbContext.OtherCrawledVideos.AsQueryable();
      if (type == "All"|| type == "all"|| type == "ALL")
      {
        queryableOtherCrawled = queryableOtherCrawled.OrderByDescending(x => x.order);
      }else if (type == "Anime"|| type == "anime"|| type == "ANIME")
      {
        queryableOtherCrawled = queryableOtherCrawled.Where(x=>x.Type=="Anime"|| x.Type == "anime"|| x.Type == "ANIME").OrderByDescending(x => x.order);
      }
      else if (type == "Real"|| type == "real"|| type == "REAL")
      {
        queryableOtherCrawled = queryableOtherCrawled.Where(x => x.Type == "Real"|| x.Type == "real"|| x.Type == "REAL").OrderByDescending(x => x.order);
      }
      
      queryableOtherCrawled = queryableOtherCrawled.Skip((page - 1) * max).Take(max);
      var OtherVideoListRaw = queryableOtherCrawled.ToList();
      List<OtherVideoDTO> otherVideosList = OtherVideoListRaw.Select(x => OtherVideoDTO.OtherCrawledVideosToOtherVideoDTO(x)).ToList();
      return new ResponseDTO<List<OtherVideoDTO>> { success = true, data = otherVideosList };
    }

    public async Task<ResponseDTO<OtherVideoDTO>> GetOtherVideoByIdAsync(long id)
    {
      if (id < 1)
      {
        return new ResponseDTO<OtherVideoDTO> { success = false, data = null, responseMessage = "invalid id" };
      }
      OtherCrawledVideos otherVideo = await dbContext.OtherCrawledVideos.FirstOrDefaultAsync(x => x.Id == id);
      if (otherVideo == null)
      {
        return new ResponseDTO<OtherVideoDTO> { success = false, data = null, responseMessage = "invalid id" };
      }
      return new ResponseDTO<OtherVideoDTO> { success = true, data = OtherVideoDTO.OtherCrawledVideosToOtherVideoDTO(otherVideo) };
    }

    public async Task<ResponseDTO<long>> CrawlVideosByUrlAsync(CrawlOtherVideoDTO otherVideoDTO)
    {
      var exVideo=dbContext.OtherCrawledVideos.FirstOrDefault(x => x.VideoUrl == otherVideoDTO.VideoUrl);
      if (exVideo != null)
      {
        return new ResponseDTO<long>
        {
          success = false,
          data = -1,
          responseMessage = "alrdy Crawled This video"
        };
      }
      string url = otherVideoDTO.VideoUrl;
      if (!System.IO.Directory.Exists($"{Config.OtherVideoPhysicalFilePath}/{otherVideoDTO.SiteName}_{otherVideoDTO.Author}"))
      {
        System.IO.Directory.CreateDirectory($"{Config.OtherVideoPhysicalFilePath}/{otherVideoDTO.SiteName}_{otherVideoDTO.Author}");
      }
      var ytdl = new YoutubeDL();
      // set the path of the youtube-dl and FFmpeg if they're not in PATH or current directory
      ytdl.YoutubeDLPath = $"H:/MyProjects/youtube-dl.exe";
      ytdl.FFmpegPath = $"C:/ffmpeg/bin/ffmpeg.exe";
      // optional: set a different download folder
      ytdl.OutputFolder = $"{Config.OtherVideoPhysicalFilePath}/{otherVideoDTO.SiteName}_{otherVideoDTO.Author}";
      ytdl.RestrictFilenames = true;
      // download a video
      var data = await ytdl.RunVideoDataFetch(url: url);

      string title = data.Data.Title;
      string publishedAt = data.Data.UploadDate.ToString();
      string author = data.Data.Uploader;
      string description = data.Data.Description;
      string channelId = data.Data.ChannelID;
      string thumbnailDefault = data.Data.Thumbnail;
      double duration = data.Data.Duration ?? 0;
      
      var res = await ytdl.RunVideoDownload(url: url);
      // the path of the downloaded file
      string path = res.Data;
      var splitedPath = path.Split("\\");
      string folder = $"{otherVideoDTO.SiteName}_{otherVideoDTO.Author}";
      string fileName = splitedPath[splitedPath.Length - 1];
      string fullPath = $"{otherVideoDTO.SiteName}_{otherVideoDTO.Author}/{fileName}";

      if (duration < 1)
      {
        var mediaInfo = await FFmpeg.GetMediaInfo($"{Config.OtherVideoPhysicalFilePath}/{fullPath}");
        duration = mediaInfo.VideoStreams.First().Duration.TotalSeconds;
      }
      string thumbnailOutputPath = $"{Config.OtherVideoPhysicalFilePath}/{fullPath}_thumbnail.png";
      string thumbnailPath = $"{fullPath}_thumbnail.png";
      IConversion conversion = await FFmpeg.Conversions.FromSnippet.Snapshot($"{Config.OtherVideoPhysicalFilePath}/{fullPath}"
        , thumbnailOutputPath, TimeSpan.FromSeconds(duration/2));
      IConversionResult result = await conversion.Start();

      OtherCrawledVideos otherCrawledVideos = new OtherCrawledVideos
      {
        Author = otherVideoDTO.Author,
        FileName = fileName,
        FullPath = fullPath,
        Path = folder,
        Type=otherVideoDTO.Type,
        SiteName = otherVideoDTO.SiteName,
        TagString = otherVideoDTO.TagString,
        Title = title??fileName,
        VideoUrl = otherVideoDTO.VideoUrl,
        Thumbnail= thumbnailPath,
        order = dbContext.OtherCrawledVideos.DefaultIfEmpty().Max(x => x == null ? 0 : x.order) + 1
      };

      await dbContext.OtherCrawledVideos.AddAsync(otherCrawledVideos);
      await dbContext.SaveChangesAsync();
      return new ResponseDTO<long> { success=true,data=otherCrawledVideos.Id};
    }

  }
}
