using System.Collections.Generic;
using TournamentParser.Tournaments.SectionSplitters;

namespace TournamentParser.Tournaments
{
    public class Quad : Tournament
    {
        public Quad(IReadOnlyCollection<Player> players)
            : base(players, new QuadSectionSplitter())
        {
        }
    }
}