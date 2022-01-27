using System;
using System.Collections.Generic;
using System.IO;
using QuadParser.Tournaments;

namespace QuadParser
{
    class Program
    {
        const string inputFileName = "participants.txt";

        static void Main(string[] args)
        {
            string[] fileLines = ReadInputFile();
            List<Player> players = SplitIntoPlayers(fileLines);

            Tournament tournament = new Quad(players);
            tournament.WriteSectionsToTextFiles();
        }

        private static string[] ReadInputFile()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string inputFilePath = Path.Combine(currentDirectory, inputFileName);

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine($"Couldn't find {inputFileName} in current directory.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(0);
            }

            return File.ReadAllLines(inputFilePath);
        }

        private static List<Player> SplitIntoPlayers(IReadOnlyList<string> fileLines)
        {
            List<Player> players = new List<Player>();
            List<string> blockLines = new List<string>();
            foreach (var line in fileLines)
            {
                if (line.ToLowerInvariant().StartsWith("name"))
                {
                    if (blockLines.Count > 0)
                    {
                        players.Add(new Player(blockLines.ToArray()));
                        blockLines.Clear();
                    }
                }

                if (line.Trim() != string.Empty)
                {
                    blockLines.Add(line);
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
