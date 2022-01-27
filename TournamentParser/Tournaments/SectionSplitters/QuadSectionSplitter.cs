using System.Collections.Generic;
using System.Linq;

namespace TournamentParser.Tournaments.SectionSplitters
{
    public class QuadSectionSplitter : ISectionSplitter
    {
        public List<Section> SplitIntoSections(IReadOnlyCollection<Player> players)
        {
            List<Section> sections = new List<Section>();

            if (players == null || players.Count == 0)
            {
                return sections;
            }

            List<Player> playersOrderedByRating = players.OrderByDescending(x => x.Rating).ToList();
            if (playersOrderedByRating.Count <= 6)
            {
                sections.Add(new Section(playersOrderedByRating));
                return sections;
            }

            int index = 0;
            while (index < playersOrderedByRating.Count - 8)
            {
                sections.Add(new Section(playersOrderedByRating.Skip(index).Take(4).ToList()));
                index += 4;
            }

            int remainder = playersOrderedByRating.Count % 4;
            switch (remainder)
            {
                case 0:
                    sections.Add(new Section(playersOrderedByRating.Skip(index).Take(4).ToList()));
                    sections.Add(new Section(playersOrderedByRating.Skip(index + 4).Take(4).ToList()));
                    break;
                case 1:
                    // last quad should be a five person quad
                    sections.Add(new Section(playersOrderedByRating.Skip(index).Take(5).ToList()));
                    break;
                case 2:
                    // last quad should be a 6 person quad
                    sections.Add(new Section(playersOrderedByRating.Skip(index).Take(6).ToList()));
                    break;
                case 3:
                    // last quad should be a 3 person quad
                    sections.Add(new Section(playersOrderedByRating.Skip(index).Take(4).ToList()));
                    sections.Add(new Section(playersOrderedByRating.Skip(index + 4).Take(3).ToList()));
                    break;
            }

            return sections;
        }
    }
}