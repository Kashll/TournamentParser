using System.Collections.Generic;
using System.Linq;

namespace TournamentParser.Tournaments.SectionSplitters
{
    public class SwissSectionSplitter : ISectionSplitter
    {
        public List<Section> SplitIntoSections(IReadOnlyCollection<Player> players)
        {
            const int ratingThreshold = 900;

            List<Section> sections = new List<Section>();

            if (players == null || players.Count == 0)
            {
                return sections;
            }

            List<Player> playersOrderedByRating = players.OrderByDescending(x => x.Rating).ToList();

            List<Player> upperSectionPlayers = playersOrderedByRating.Where(x => x.Rating >= ratingThreshold).ToList();
            List<Player> lowerSectionPlayers = playersOrderedByRating.Where(x => x.Rating < ratingThreshold).ToList();

            Section upperSection = new Section(upperSectionPlayers);
            Section lowerSection = new Section(lowerSectionPlayers);

            sections.Add(upperSection);
            sections.Add(lowerSection);

            return sections;
        }
    }
}