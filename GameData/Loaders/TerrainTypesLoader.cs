using System.Collections.Generic;

namespace GameData.Loaders
{
    public static class TerrainTypesLoader
    {
        //public static List<TerrainType> GetTerrainTypes()
        //{
        //    var terrainTypes = new List<TerrainType>();

        //    IEnumerable<string> lines = File.ReadLines("TerrainTypes.txt");

        //    foreach (string line in lines)
        //    {
        //        if (line.StartsWith("--")) continue;
        //        string[] splitLine = line.Split(',');
        //        int id = splitLine[0].ToInt32();
        //        string name = splitLine[1];
        //        int movementCost = splitLine[2].ToInt32();
        //        float foodOutput = splitLine[3].ToFloat();
        //        float productionPercentage = splitLine[4].ToFloat();
        //        TerrainType terrainType = TerrainType.Create(id, name, movementCost, foodOutput, productionPercentage);
        //        terrainTypes.Add(terrainType);
        //    }

        //    return terrainTypes;
        //}

        public static List<TerrainType> GetTerrainTypes()
        {
            var terrainTypes = new List<TerrainType>
            {
                TerrainType.Create(0, "Grassland", 1, 1.5f, 0.0f),
                TerrainType.Create(1, "Forest", 2, 0.5f, 3.0f),
                TerrainType.Create(2, "Desert", 1, 0.0f, 3.0f),
                TerrainType.Create(3, "Swamp", 3, 0.0f, 0.0f),
                TerrainType.Create(4, "River", 2, 2.0f, 0.0f),
                TerrainType.Create(5, "River Mouth", 2, 2.0f, 2.0f),
                TerrainType.Create(6, "Hill", 3, 0.5f, 3.0f),
                TerrainType.Create(7, "Mountain", 4, 0.0f, 5.0f),
                TerrainType.Create(8, "Volcano", 4, 0.0f, 0.0f),
                TerrainType.Create(9, "Tundra", 2, 0.0f, 0.0f),
                TerrainType.Create(10, "Shore", -1, 0.5f, 0.0f),
                TerrainType.Create(11, "Ocean", -1, 0.0f, 0.0f),
                TerrainType.Create(12, "SorceryNode", 1, 2.0f, 0.0f),
                TerrainType.Create(13, "ChaosNode", 4, 0.0f, 5.0f),
                TerrainType.Create(14, "NatureNode", 2, 2.5f, 3.0f)
            };

            return terrainTypes;
        }
    }
}