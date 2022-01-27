using System.Linq;

namespace QuadParser
{
    public class Player
    {
        public Player(string[] blockLines)
        {
            SourceBlockLines = blockLines.Where(x => x.Trim() != "").ToArray();

            NameLine = blockLines.SingleOrDefault(x => x.ToLowerInvariant().StartsWith("name"));
            IdNumberLine = blockLines.SingleOrDefault(x => x.ToLowerInvariant().StartsWith("id"));
            TeamLine = blockLines.SingleOrDefault(x => x.ToLowerInvariant().StartsWith("team"));
            AgeLine = blockLines.SingleOrDefault(x => x.ToLowerInvariant().StartsWith("age"));

            Rating = ParseRating(blockLines.SingleOrDefault(x => x.ToLowerInvariant().StartsWith("rating")));
        }

        public string[] SourceBlockLines { get; }

        public string NameLine { get; }

        public int Rating { get; }

        public string IdNumberLine { get; }

        public string TeamLine { get; }

        public string AgeLine { get; }

        private static int ParseRating(string inputLine)
        {
            if (string.IsNullOrEmpty(inputLine))
            {
                return 0;
            }

            string ratingValue = new string(inputLine.SkipWhile(x => x != '=').Skip(1).ToArray()).Trim();
            if (ratingValue.StartsWith("unr"))
            {
                return 0;
            }

            if (!int.TryParse(ratingValue, out int rating))
            {
                return 0;
            }

            return rating;
        }
    }
}