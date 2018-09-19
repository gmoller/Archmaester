using System.Collections.Generic;

namespace GameData.Loaders
{
    public static class MineralTypesLoader
    {
        //public static List<MineralType> GetMineralTypes()
        //{
        //    var mineralTypes = new List<MineralType>();

        //    IEnumerable<string> lines = File.ReadLines("MineralTypes.txt");

        //    foreach (string line in lines)
        //    {
        //        if (line.StartsWith("--")) continue;
        //        string[] splitLine = line.Split(',');
        //        int id = splitLine[0].ToInt32();
        //        string name = splitLine[1];
        //        float foodModifier = splitLine[2].ToFloat();
        //        MineralType mineralType = MineralType.Create(id, name, foodModifier);
        //        mineralTypes.Add(mineralType);
        //    }

        //    return mineralTypes;
        //}

        public static List<MineralType> GetMineralTypes()
        {
            var mineralTypes = new List<MineralType>
            {
                MineralType.Create(0, "Silver Ore", 0.0f),
                MineralType.Create(1, "Gold Ore", 0.0f),
                MineralType.Create(2, "Gems", 0.0f),
                MineralType.Create(3, "Iron Ore", 0.0f),
                MineralType.Create(4, "Coal", 0.0f),
                MineralType.Create(5, "Mithril Ore", 0.0f),
                MineralType.Create(6, "Adamantium Ore", 0.0f),
                MineralType.Create(7, "Quork Crystals", 0.0f),
                MineralType.Create(8, "Crysx Crystals", 0.0f),
                MineralType.Create(9, "Nightshade", 0.0f),
                MineralType.Create(10, "Wild Game", 2.0f)
            };


            return mineralTypes;
        }
    }
}