using System.Collections.Generic;
using System.IO;
using GeneralUtilities;

namespace GameData.Loaders
{
    public static class UnitTypesLoader
    {
        public static List<UnitType> GetUnitTypes(MovementTypes movementTypes)
        {
            var unitTypes = new List<UnitType>();

            IEnumerable<string> lines = File.ReadLines("UnitTypes.txt");

            foreach (string line in lines)
            {
                if (line.StartsWith("--")) continue;
                string[] splitLine = line.Split(',');
                int id = splitLine[0].ToInt32();
                string name = splitLine[1];
                int moves = splitLine[2].ToInt32();
                string movementType = splitLine[3];
                UnitType unitType = UnitType.Create(id, name, moves, movementTypes[movementType]);
                unitTypes.Add(unitType);
            }

            return unitTypes;
        }
    }
}