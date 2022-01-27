using System.Collections.Generic;

namespace TournamentParser.Tournaments.SectionSplitters
{
    public interface ISectionSplitter
    {
        List<Section> SplitIntoSections(IReadOnlyCollection<Player> players);
    }
}