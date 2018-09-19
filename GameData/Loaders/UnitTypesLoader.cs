using System.Collections.Generic;

namespace GameData.Loaders
{
    public static class UnitTypesLoader
    {
        //public static List<UnitType> GetUnitTypes(MovementTypes movementTypes)
        //{
        //    var unitTypes = new List<UnitType>();

        //    IEnumerable<string> lines = File.ReadLines("UnitTypes.txt");

        //    foreach (string line in lines)
        //    {
        //        if (line.StartsWith("--")) continue;
        //        string[] splitLine = line.Split(',');
        //        int id = splitLine[0].ToInt32();
        //        string name = splitLine[1];
        //        int moves = splitLine[2].ToInt32();
        //        string movementType = splitLine[3];
        //        UnitType unitType = UnitType.Create(id, name, moves, movementTypes[movementType]);
        //        unitTypes.Add(unitType);
        //    }

        //    return unitTypes;
        //}

        public static List<UnitType> GetUnitTypes(MovementTypes movementTypes)
        {
            var unitTypes = new List<UnitType>
            {
                UnitType.Create(0, "Spearmen", 1, movementTypes["Ground"]),
                UnitType.Create(1, "Swordsmen", 1, movementTypes["Ground"]),
                UnitType.Create(2, "Halberdiers", 1, movementTypes["Ground"]),
                UnitType.Create(3, "Pikemen", 1, movementTypes["Ground"]),
                UnitType.Create(4, "Cavalry", 2, movementTypes["Ground"]),
                UnitType.Create(5, "Bowmen", 1, movementTypes["Ground"]),
                UnitType.Create(6, "Shamans", 1, movementTypes["Ground"]),
                UnitType.Create(7, "Priests", 1, movementTypes["Ground"]),
                UnitType.Create(8, "Magicians", 1, movementTypes["Ground"]),
                UnitType.Create(9, "Engineers", 1, movementTypes["Ground"]),
                UnitType.Create(10, "Settlers", 1, movementTypes["Ground"])
            };

            return unitTypes;
        }
    }
}