using System.Collections.Generic;
using System.Linq;

namespace TournamentParser
{
    public class Section
    {
        public Section(IReadOnlyCollection<Player> players)
        {
            Players = players;
        }

        public IReadOnlyCollection<Player> Players { get; }

        public string[] GetSectionFileLines()
        {
            return Players.ToList().SelectMany(x => x.SourceBlockLines.Concat(new List<string> { "" })).ToArray();
        }
    }
}