using Connect4Web.Data;
using Connect4Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4Web.Hubs
{
    [Authorize]
    public class UserHub : Hub
    {
        private readonly Data.AppDbContext _context;
        private readonly UserManager<User> _userManager;

        static private bool clearConnectionIds = true;

        public UserHub(Data.AppDbContext context,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

            if (clearConnectionIds)
            {
                clearConnectionIds = false;
                _context.Users.ForEachAsync(u => u.ConnectionId = string.Empty).Wait();
            }
        }

        public override async Task OnConnectedAsync()
        {
            var user = await _userManager.GetUserAsync(Context.User);
            if (!string.IsNullOrWhiteSpace(user.ConnectionId))
            {
                await Clients.Client(user.ConnectionId).SendAsync("DoDisconnect", "Eine weitere Sitzung unter diesem Benutzer wurde geöffnet.");
            }

            user.ConnectionId = Context.ConnectionId;
            await _userManager.UpdateAsync(user);

            await Clients.Others.SendAsync("OnUserConnected", user.UserName);
            var users = await _context.Users
                .Where(u => u.ConnectionId != string.Empty)
                .Where(u => u.ConnectionId != Context.ConnectionId)
                .OrderBy(u => u.UserName)
                .Select(u => u.UserName)
                .ToListAsync();
            await Clients.Caller.SendAsync("OnConnected", users);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            if (user.ConnectionId == Context.ConnectionId)
            {
                user.ConnectionId = string.Empty;
                await _userManager.UpdateAsync(user);
            }

            await Clients.Others.SendAsync("OnUserDisconnected", user.UserName);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
