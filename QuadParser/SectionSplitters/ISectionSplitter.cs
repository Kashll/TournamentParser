using System.Collections.Generic;

namespace QuadParser.SectionSplitters
{
    public interface ISectionSplitter
    {
        List<Section> SplitIntoSections(IReadOnlyCollection<Player> players);
    }
}