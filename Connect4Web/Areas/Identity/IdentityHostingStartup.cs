using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Connect4Web.Data;
using Connect4Web.Models;

[assembly: HostingStartup(typeof(Connect4Web.Areas.Identity.IdentityHostingStartup))]
namespace Connect4Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            // builder.ConfigureServices((context, services) => {
            //     services.AddDefaultIdentity<User>()
            //         .AddEntityFrameworkStores<ApplicationContext>();
            // });
        }
    }
}