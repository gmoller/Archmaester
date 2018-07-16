using System.Collections.Generic;
using GameMap;
using GeneralUtilities;

namespace GameLogic
{
    public static class TerrainHelper
    {
        public static int GetBaseFoodLevelFromTerrain(Point2 location)
        {
            float baseFoodLevelFromTerrain = 0.0f;

            List<Cell> cells = GetSettlementCells(location);
            foreach (Cell item in cells)
            {
                baseFoodLevelFromTerrain += Globals.Instance.TerrainTypes[item.TerrainTypeId].FoodOutput;
            }

            return (int)baseFoodLevelFromTerrain;
        }

        public static int GetBaseProductionPointsFromTerrain(Point2 location)
        {
            float baseProductionPointsFromTerrain = 0.0f;

            List<Cell> cells = GetSettlementCells(location);
            foreach (Cell item in cells)
            {
                baseProductionPointsFromTerrain += Globals.Instance.TerrainTypes[item.TerrainTypeId].ProductionPercentage;
            }

            return (int)baseProductionPointsFromTerrain;
        }

        public static int GetMineralFoodModifierFromTerrain(Point2 location)
        {
            float wildGameFromTerrain = 0.0f;

            List<Cell> cells = GetSettlementCells(location);
            foreach (Cell item in cells)
            {
                wildGameFromTerrain += Globals.Instance.MineralTypes[item.MineralTypeId].FoodModifier;
            }

            return (int)wildGameFromTerrain;
        }

        private static List<Cell> GetSettlementCells(Point2 location)
        {
            var cells = new List<Cell>
            {
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X - 1, location.Y - 2)),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X, location.Y - 2)),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X + 1, location.Y - 2)),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X - 2, location.Y - 1)),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X - 1, location.Y - 1)),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X, location.Y - 1)),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X + 1, location.Y - 1)),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X + 2, location.Y - 1)),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X - 2, location.Y)),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X - 1, location.Y)),
                Globals.Instance.GameWorld.GetCell(location),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X + 1, location.Y)),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X + 2, location.Y)),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X - 2, location.Y + 1)),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X - 1, location.Y + 1)),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X, location.Y + 1)),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X + 1, location.Y + 1)),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X + 2, location.Y + 1)),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X - 1, location.Y + 2)),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X, location.Y + 2)),
                Globals.Instance.GameWorld.GetCell(Point2.Create(location.X + 1, location.Y + 2))
            };

            return cells;
        }
    }
}