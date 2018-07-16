using System.Collections.Generic;
using System.IO;
using GeneralUtilities;

namespace GameData.Loaders
{
    public static class RaceTypesLoader
    {
        public static List<RaceType> GetRaceTypes()
        {
            var raceTypes = new List<RaceType>();

            IEnumerable<string> lines = File.ReadLines("RaceTypes.txt");

            foreach (string line in lines)
            {
                if (line.StartsWith("--")) continue;
                string[] splitLine = line.Split(',');
                int id = splitLine[0].ToInt32();
                string name = splitLine[1];
                float farmingRate = splitLine[2].ToFloat();
                int growthRateModifier = splitLine[3].ToInt32();
                float workerProductionRate = splitLine[4].ToFloat();
                float farmerProductionRate = splitLine[5].ToFloat();
                RaceType raceType = RaceType.Create(id, name, farmingRate, growthRateModifier, workerProductionRate, farmerProductionRate);
                raceTypes.Add(raceType);
            }

            return raceTypes;
        }
    }
}