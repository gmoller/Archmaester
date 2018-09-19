using System.Collections.Generic;
using GeneralUtilities;

namespace GameData.Loaders
{
    public static class BuildingTypesLoader
    {
        //public static BuildingTypes GetBuildingTypes()
        //{
        //    var buildingTypes = new List<BuildingType>();

        //    IEnumerable<string> lines = File.ReadLines("BuildingTypes.txt");

        //    foreach (string line in lines)
        //    {
        //        if (line.StartsWith("--")) continue;
        //        string[] splitLine = line.Split(',');
        //        int id = splitLine[0].ToInt32();
        //        string name = splitLine[1];
        //        float constructionCost = splitLine[2].ToFloat();
        //        float upkeepGold = splitLine[3].ToFloat();
        //        float upkeepMana = splitLine[4].ToFloat();
        //        float foodProduced = splitLine[5].ToFloat();
        //        float growthRateIncrease = splitLine[6].ToFloat();
        //        List<int> dependentBuildings = GetDependentBuildings(splitLine[7]);
        //        List<int> races = GetRaces(splitLine[8]);

        //        BuildingType buildingType = BuildingType.Create(id, name, constructionCost, 0.0f, upkeepGold, upkeepMana, foodProduced, growthRateIncrease, dependentBuildings, races);
        //        buildingTypes.Add(buildingType);
        //    }

        //    return BuildingTypes.Create(buildingTypes);
        //}

        public static BuildingTypes GetBuildingTypes()
        {
            var buildingTypes = new List<BuildingType>
            {
                BuildingType.Create(0, "Barracks", 30.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, GetDependentBuildings("none"), GetRaces("all")),
                BuildingType.Create(1, "Smithy", 40.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, GetDependentBuildings("none"), GetRaces("all")),
                BuildingType.Create(2, "Builders Hall", 60.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, GetDependentBuildings("none"), GetRaces("all")),
                BuildingType.Create(3, "Armory", 80.0f, 2.0f, 0.0f, 0.0f, 0.0f, 0.0f, GetDependentBuildings("1"), GetRaces("all")),
                BuildingType.Create(4, "Fighters Guild", 200.0f, 0.0f, 3.0f, 0.0f, 0.0f, 0.0f, GetDependentBuildings("3"), GetRaces("all")),
                BuildingType.Create(5, "Armorers Guild", 350.0f, 0.0f, 4.0f, 0.0f, 0.0f, 0.0f, GetDependentBuildings("4"), GetRaces("allexcept:5;6"))
            };

            return BuildingTypes.Create(buildingTypes);
        }

        private static List<int> GetDependentBuildings(string s)
        {
            List<int> dependentBuildings = new List<int>();
            string[] split = s.Split(';');
            foreach (string item in split)
            {
                if (item != "none")
                {
                    dependentBuildings.Add(item.ToInt32());
                }
            }

            return dependentBuildings;
        }

        private static List<int> GetRaces(string s)
        {
            List<int> races = new List<int>();
            string[] split = s.Split(':');

            if (split[0] == "all")
            {
                races.Add(0);
                races.Add(1);
                races.Add(2);
            }

            return races;
        }
    }
}