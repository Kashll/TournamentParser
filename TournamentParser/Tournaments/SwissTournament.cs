using System.Collections.Generic;
using TournamentParser.Tournaments.SectionSplitters;

namespace TournamentParser.Tournaments
{
    public class SwissTournament : Tournament
    {
        public SwissTournament(IReadOnlyCollection<Player> players)
            : base(players, new SwissSectionSplitter())
        {
        }
    }
}