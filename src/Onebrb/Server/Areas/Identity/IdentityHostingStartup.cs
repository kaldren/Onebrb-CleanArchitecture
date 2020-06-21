using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Onebrb.Server.Areas.Identity.IdentityHostingStartup))]
namespace Onebrb.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}