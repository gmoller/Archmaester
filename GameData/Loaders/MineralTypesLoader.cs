using System.Collections.Generic;
using System.IO;
using GeneralUtilities;

namespace GameData.Loaders
{
    public static class MineralTypesLoader
    {
        public static List<MineralType> GetMineralTypes()
        {
            var mineralTypes = new List<MineralType>();

            IEnumerable<string> lines = File.ReadLines("MineralTypes.txt");

            foreach (string line in lines)
            {
                if (line.StartsWith("--")) continue;
                string[] splitLine = line.Split(',');
                int id = splitLine[0].ToInt32();
                string name = splitLine[1];
                float foodModifier = splitLine[2].ToFloat();
                MineralType mineralType = MineralType.Create(id, name, foodModifier);
                mineralTypes.Add(mineralType);
            }

            return mineralTypes;
        }
    }
}