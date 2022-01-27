using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace QuadParser
{
    class Program
    {
        static void Main(string[] args)
        {
            const string inputFileName = "participants.txt";
            const string outputFileBaseName = "Participants.txt";

            string currentDirectory = Directory.GetCurrentDirectory();
            string inputFilePath = Path.Combine(currentDirectory, inputFileName);

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("Couldn't find participants.txt in current directory.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }

            string[] fileLines = File.ReadAllLines(inputFilePath);
            List<Player> players = SplitIntoPlayers(fileLines);
            List<Quad> quads = SplitIntoQuads(players);

            int quadNumber = 1;
            foreach (Quad quad in quads)
            {
                string quadFileName = $"Q{quadNumber}{outputFileBaseName}";
                string quadFilePath = Path.Combine(currentDirectory, quadFileName);

                string[] quadLines = quad.Players.SelectMany(x => x.SourceBlockLines.Concat(new List<string> { "" })).ToArray();
                File.WriteAllLines(quadFilePath, quadLines);

                quadNumber++;
            }
        }

        private static List<Quad> SplitIntoQuads(IReadOnlyList<Player> players)
        {
            List<Quad> quads = new List<Quad>();

            if (players == null || players.Count == 0)
            {
                return quads;
            }

            List<Player> playersOrderedByRating = players.OrderByDescending(x => x.Rating).ToList();
            if (playersOrderedByRating.Count <= 6)
            {
                quads.Add(new Quad(playersOrderedByRating));
                return quads;
            }

            int index = 0;
            while (index < playersOrderedByRating.Count - 8)
            {
                quads.Add(new Quad(playersOrderedByRating.Skip(index).Take(4).ToList()));
                index += 4;
            }

            int remainder = playersOrderedByRating.Count % 4;
            switch (remainder)
            {
                case 0:
                    quads.Add(new Quad(playersOrderedByRating.Skip(index).Take(4).ToList()));
                    quads.Add(new Quad(playersOrderedByRating.Skip(index + 4).Take(4).ToList()));
                    break;
                case 1:
                    // last quad should be a five person quad
                    quads.Add(new Quad(playersOrderedByRating.Skip(index).Take(5).ToList()));
                    break;
                case 2:
                    // last quad should be a 6 person quad
                    quads.Add(new Quad(playersOrderedByRating.Skip(index).Take(6).ToList()));
                    break;
                case 3:
                    // last quad should be a 3 person quad
                    quads.Add(new Quad(playersOrderedByRating.Skip(index).Take(4).ToList()));
                    quads.Add(new Quad(playersOrderedByRating.Skip(index + 4).Take(3).ToList()));
                    break;
            }

            return quads;
        }

        private static List<Player> SplitIntoPlayers(IReadOnlyList<string> fileLines)
        {
            List<Player> players = new List<Player>();
            List<string> blockLines = new List<string>();
            for (int i = 0; i < fileLines.Count; i++)
            {
                if (fileLines[i].ToLowerInvariant().StartsWith("name"))
                {
                    if (blockLines.Count > 0)
                    {
                        players.Add(new Player(blockLines.ToArray()));
                        blockLines.Clear();
                    }
                }

                if (fileLines[i].Trim() != string.Empty)
                {
                    blockLines.Add(fileLines[i]);
                }
            }

            if (blockLines.Count > 0)
            {
                players.Add(new Player(blockLines.ToArray()));
                blockLines.Clear();
            }

            return players;
        }
    }
}
