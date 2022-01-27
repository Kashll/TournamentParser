using System.Collections.Generic;
using TournamentParser.Tournaments.SectionSplitters;

namespace TournamentParser.Tournaments
{
    public class QuadTournament : Tournament
    {
        public QuadTournament(IReadOnlyCollection<Player> players)
            : base(players, new QuadSectionSplitter())
        {
        }
    }
}