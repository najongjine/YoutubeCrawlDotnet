using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using YoutubeCrawlDotnet.Client.Crawl;
using YoutubeCrawlDotnet.Client.Youtube;
using YoutubeCrawlDotnet.Shared.DTOs;

namespace YoutubeCrawlDotnet.Client
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      builder.RootComponents.Add<App>("#app");

      builder.Services.AddHttpClient("YoutubeCrawlDotnet.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
          .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

      // Supply HttpClient instances that include access tokens when making requests to the server project
      builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("YoutubeCrawlDotnet.ServerAPI"));

      
      ConfigureServices(builder.Services);
      await builder.Build().RunAsync();
    }
    private static void ConfigureServices(IServiceCollection services)
    {
      services.AddScoped<IYoutubeAPI, YoutubeAPI>();
      services.AddScoped<ICrawlVideo, CrawlVideo>();
      services.AddApiAuthorization();
    }
  }
}
