using System.Collections.Generic;

namespace QuadParser
{
    public class Quad
    {
        public Quad(IReadOnlyCollection<Player> players)
        {
            Players = players;
        }

        public IReadOnlyCollection<Player> Players { get; }
    }
}