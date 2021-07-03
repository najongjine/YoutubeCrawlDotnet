using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoutubeCrawlDotnet.Server.SignalR
{
  public class YoutubeDlHub :Hub
  {
    public override Task OnConnectedAsync()
    {
      Console.WriteLine($"{Context.ConnectionId} connected");
      return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception e)
    {
      Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
      await base.OnDisconnectedAsync(e);
    }

    public async Task sendDlProgress( float progress)
    {
      await Clients.All.SendAsync("sendDlProgress", progress);
    }
  }
}
