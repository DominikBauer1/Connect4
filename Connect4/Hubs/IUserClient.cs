using Connect4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Hubs
{
    public interface IUserClient
    {
        Task OrderDisconnect();
        Task ReceiveActiveUsers(List<User> users);
    }
}
