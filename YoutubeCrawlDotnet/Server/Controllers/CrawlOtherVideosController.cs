using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xabe.FFmpeg;
using YoutubeCrawlDotnet.Server.Helper;
using YoutubeCrawlDotnet.Server.Manager;
using YoutubeCrawlDotnet.Shared.Config;
using YoutubeCrawlDotnet.Shared.DTOs;
using YoutubeCrawlDotnet.Shared.DTOs.OtherVideoPage;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YoutubeCrawlDotnet.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CrawlOtherVideosController : ControllerBase
  {
    private readonly CleanString CleanString;
    private readonly IOtherVideoManager otherVideoManager;

    public CrawlOtherVideosController(CleanString cleanString, IOtherVideoManager otherVideoManager)
    {
      CleanString = cleanString;
      this.otherVideoManager = otherVideoManager;
    }

    // GET: api/<ValuesController>
    [HttpGet]
    public async Task<ResponseDTO<List<OtherVideoDTO>>> Get([FromQuery] int page=1,[FromQuery] int max=50000,[FromQuery] string type="All")
    {
      var result = await otherVideoManager.GetAllOtherVideosAsync(page, max,type);
      return result;
    }

    // GET api/<ValuesController>/5
    [HttpGet("{id}")]
    public async Task<ResponseDTO<OtherVideoDTO>> Get(long id=0)
    {
      var result = await otherVideoManager.GetOtherVideoByIdAsync(id);
      return result;
    }

    // POST api/<ValuesController>
    [HttpPost]
    public async Task<ResponseDTO<long>> Post([FromBody] CrawlOtherVideoDTO otherVideoDTO)
    {
      var result=await otherVideoManager.CrawlVideosByUrlAsync(otherVideoDTO);
      return result;
      
    }

    // PUT api/<ValuesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ValuesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
