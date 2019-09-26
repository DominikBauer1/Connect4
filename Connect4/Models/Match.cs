using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public enum Player
    {
        None,
        Player1,
        Player2
    }

    public enum MatchState
    {
        InProgress,
        Ended
    }

    public class Match
    {
        public Guid Id { get; set; }

        public User Player1 { get; set; }
        public User Player2 { get; set; }

        public MatchState State { get; set; }
    }
}
