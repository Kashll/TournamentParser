using System.Collections.Generic;
using QuadParser.SectionSplitters;

namespace QuadParser.Tournaments
{
    public class Quad : Tournament
    {
        public Quad(IReadOnlyCollection<Player> players)
            : base(players, new QuadSectionSplitter())
        {
        }
    }
}