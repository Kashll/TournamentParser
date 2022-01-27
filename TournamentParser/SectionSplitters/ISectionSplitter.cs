using System.Collections.Generic;

namespace TournamentParser.SectionSplitters
{
    public interface ISectionSplitter
    {
        List<Section> SplitIntoSections(IReadOnlyCollection<Player> players);
    }
}