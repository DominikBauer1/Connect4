using Connect4.Data;
using Connect4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Hubs
{
    [Authorize]
    public class UserHub : Hub<IUserClient>
    {
        private readonly AppDbContext _context;

        public UserHub(AppDbContext context)
        {
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == Context.UserIdentifier);
            if (user.ConnectionId != null && user.ConnectionId != string.Empty)
            {
                await Clients.Client(user.ConnectionId).OrderDisconnect();
            }

            user.ConnectionId = Context.ConnectionId;
            await _context.SaveChangesAsync();

            var users = await _context.Users
                .Where(u => u.ConnectionId != string.Empty && u.ConnectionId != Context.ConnectionId)
                .ToListAsync();

            await Clients.All.ReceiveActiveUsers(users);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == Context.UserIdentifier);
            if (user.ConnectionId == Context.ConnectionId)
            {
                user.ConnectionId = string.Empty;
                await _context.SaveChangesAsync();
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
