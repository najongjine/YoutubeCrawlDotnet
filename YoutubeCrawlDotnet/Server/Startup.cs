using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using YoutubeCrawlDotnet.Server.Data;
using YoutubeCrawlDotnet.Server.Helper;
using YoutubeCrawlDotnet.Server.Helpers;
using YoutubeCrawlDotnet.Server.Manager;
using YoutubeCrawlDotnet.Server.Models;

namespace YoutubeCrawlDotnet.Server
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(
              Configuration.GetConnectionString("DefaultConnection")));
      services.AddAutoMapper(typeof(Startup));
      services.AddDatabaseDeveloperPageExceptionFilter();

      services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
          .AddEntityFrameworkStores<ApplicationDbContext>();

      /*
      services.AddIdentityServer()
          .AddApiAuthorization<ApplicationUser, ApplicationDbContext>().AddProfileService<IdentityProfileService>();
      */
      services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options => {
      options.IdentityResources["openid"].UserClaims.Add("name");
      options.ApiResources.Single().UserClaims.Add("name");
      options.IdentityResources["openid"].UserClaims.Add("role");
      options.ApiResources.Single().UserClaims.Add("role");
    });

      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");
      services.Configure<IdentityOptions>(options =>
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);

      services.AddAuthentication()
          .AddIdentityServerJwt();
      services.AddScoped<CleanString>();
      services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
      services.AddRazorPages();
      services.AddScoped<IYoutubeDownloadManager, YoutubeDownloadManager>();
      services.AddScoped<IPublicVideoManager, PublicVideoManager>();
      services.AddScoped<IPrivateVideoManager, PrivateVideoManager>();
      services.AddScoped<IOtherVideoManager, OtherVideoManager>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseWebAssemblyDebugging();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseBlazorFrameworkFiles();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseIdentityServer();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapRazorPages();
        endpoints.MapControllers();
        endpoints.MapFallbackToFile("index.html");
      });
    }
  }
}
