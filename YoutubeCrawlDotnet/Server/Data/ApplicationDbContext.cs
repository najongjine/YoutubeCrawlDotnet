using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using YoutubeCrawlDotnet.Server.Models;
using YoutubeCrawlDotnet.Shared.Entities;

namespace YoutubeCrawlDotnet.Server.Data
{
  public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
  {
    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Crawled>().HasIndex(b => b.VideoId)
            .IsUnique();
      modelBuilder.Entity<OtherCrawledVideos>().HasIndex(x => x.VideoUrl).IsUnique();
      base.OnModelCreating(modelBuilder);
    }

    public DbSet<Crawled> Crawled { get; set; }
    public DbSet<PublicCrawled> PublicCrawled { get; set; }
    public DbSet<PrivateCrawled> PrivateCrawled { get; set; }
    public DbSet<OtherCrawledVideos> OtherCrawledVideos { get; set; }
  }
}
