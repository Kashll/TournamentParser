using System.Collections.Generic;
using System.IO;
using TournamentParser.Tournaments.SectionSplitters;

namespace TournamentParser.Tournaments
{
    public abstract class Tournament
    {
        protected const string OutputFileBaseName = "Participants.txt";

        protected Tournament(IReadOnlyCollection<Player> players, ISectionSplitter sectionSplitter)
        {
            Players = players;
            Sections = sectionSplitter.SplitIntoSections(players);
        }

        public IReadOnlyCollection<Player> Players { get; }

        public IReadOnlyCollection<Section> Sections { get; }

        public virtual void WriteSectionsToTextFiles()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            int sectionNumber = 1;
            foreach (Section section in Sections)
            {
                string sectionFileName = $"S{sectionNumber}{OutputFileBaseName}";
                string sectionFilePath = Path.Combine(currentDirectory, sectionFileName);

                string[] sectionFileLines = section.GetSectionFileLines();
                File.WriteAllLines(sectionFilePath, sectionFileLines);

                sectionNumber++;
            }
        }
    }
}