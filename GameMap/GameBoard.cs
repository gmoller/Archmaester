using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeneralUtilities;

namespace GameMap
{
    public enum CompassDirection
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest,
        None
    }

    /// <summary>
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class GameBoard
    {
        private readonly Random _random = new Random();

        private readonly Cell[,,] _cells; // layer-z, column-x, row-y
        private bool[,] _isVisible;

        internal int NumberOfLayers => _cells.GetLength(0);
        public int NumberOfColumns => _cells.GetLength(1);
        public int NumberOfRows => _cells.GetLength(2);

        private GameBoard(int numberOfLayers, int[,] terrain, bool allVisible)
        {
            int numberOfColumns = terrain.GetLength(0);
            int numberOfRows = terrain.GetLength(1);

            _cells = new Cell[numberOfLayers, numberOfColumns, numberOfRows];
            _isVisible = new bool[numberOfColumns, numberOfRows];

            for (int layer = 0; layer < numberOfLayers; ++layer)
            {
                for (int column = 0; column < numberOfColumns; ++column)
                {
                    for (int row = 0; row < numberOfRows; ++row)
                    {
                        int terrainTypeId = terrain[column, row];
                        _cells[layer, column, row] = Cell.Create(terrainTypeId);
                        _isVisible[column, row] = allVisible;
                    }
                }
            }
        }

        public static GameBoard Create(int numberOfLayers, int[,] terrain, bool allVisible)
        {
            return new GameBoard(numberOfLayers, terrain, allVisible);
        }

        public Cell GetCell(Point2 location)
        {
            if (!IsCellOnBoard(location))
            {
                return Cell.Null;
            }

            return _cells[0, location.X, location.Y];
        }

        private bool IsCellOnBoard(Point2 location)
        {
            if (location.X < 0 ||
                location.Y < 0 ||
                location.X > NumberOfColumns - 1 ||
                location.Y > NumberOfRows - 1)
            {
                return false;
            }

            return true;
        }

        public List<int> GetNeighboringTerrainTypeIds(Point2 cellLocation)
        {
            var cells = new List<int>
            {
                GetNeighboringCell(cellLocation, CompassDirection.North).TerrainTypeId,
                GetNeighboringCell(cellLocation, CompassDirection.NorthEast).TerrainTypeId,
                GetNeighboringCell(cellLocation, CompassDirection.East).TerrainTypeId,
                GetNeighboringCell(cellLocation, CompassDirection.SouthEast).TerrainTypeId,
                GetNeighboringCell(cellLocation, CompassDirection.South).TerrainTypeId,
                GetNeighboringCell(cellLocation, CompassDirection.SouthWest).TerrainTypeId,
                GetNeighboringCell(cellLocation, CompassDirection.West).TerrainTypeId,
                GetNeighboringCell(cellLocation, CompassDirection.NorthWest).TerrainTypeId
            };

            return cells;
        }

        private Cell GetNeighboringCell(Point2 cellLocation, CompassDirection direction)
        {
            Point2 p;
            switch (direction)
            {
                case CompassDirection.North:
                    p = Point2.Create(cellLocation.X, cellLocation.Y - 1);
                    break;
                case CompassDirection.NorthEast:
                    p = Point2.Create(cellLocation.X + 1, cellLocation.Y - 1);
                    break;
                case CompassDirection.East:
                    p = Point2.Create(cellLocation.X + 1, cellLocation.Y);
                    break;
                case CompassDirection.SouthEast:
                    p = Point2.Create(cellLocation.X + 1, cellLocation.Y + 1);
                    break;
                case CompassDirection.South:
                    p = Point2.Create(cellLocation.X, cellLocation.Y + 1);
                    break;
                case CompassDirection.SouthWest:
                    p = Point2.Create(cellLocation.X - 1, cellLocation.Y + 1);
                    break;
                case CompassDirection.West:
                    p = Point2.Create(cellLocation.X - 1, cellLocation.Y);
                    break;
                case CompassDirection.NorthWest:
                    p = Point2.Create(cellLocation.X - 1, cellLocation.Y - 1);
                    break;
                default:
                    p = Point2.Null;
                    break;
            }

            return GetCell(p);
        }

        public List<Point2> GetCellNeighbors(Point2 location)
        {
            var neighbors = new List<Point2>();
            Point2 a = Point2.Create(location.X, location.Y + 1); // north
            Point2 b = Point2.Create(location.X - 1, location.Y); // west
            Point2 c = Point2.Create(location.X, location.Y - 1); // south
            Point2 d = Point2.Create(location.X + 1, location.Y); // east

            var dic = new Dictionary<int, Point2[]>
            {
                {1, new[] {a, b, c, d}},
                {2, new[] {a, b, d, c}},
                {3, new[] {a, c, b, d}},
                {4, new[] {a, c, d, b}},
                {5, new[] {a, d, b, c}},
                {6, new[] {a, d, c, b}},
                {7, new[] {b, a, c, d}},
                {8, new[] {b, a, d, c}},
                {9, new[] {b, c, a, d}},
                {10, new[] {b, c, d, a}},
                {11, new[] {b, d, a, c}},
                {12, new[] {b, d, c, a}},
                {13, new[] {c, a, b, d}},
                {14, new[] {c, a, d, b}},
                {15, new[] {c, b, a, d}},
                {16, new[] {c, b, d, a}},
                {17, new[] {c, d, a, b}},
                {18, new[] {c, d, b, a}},
                {19, new[] {d, a, b, c}},
                {20, new[] {d, a, c, b}},
                {21, new[] {d, b, a, c}},
                {22, new[] {d, b, c, a}},
                {23, new[] {d, c, a, b}},
                {24, new[] {d, c, b, a}}
            };

            int i = _random.Next(1, 25); // get a number between 1 and 24
            Point2[] points = dic[i];
            AddCellsIfItsOnBoard(neighbors, points);

            return neighbors;
        }

        private void AddCellsIfItsOnBoard(List<Point2> neighbors, params Point2[] points)
        {
            foreach (Point2 item in points)
            {
                if (IsCellOnBoard(item))
                {
                    neighbors.Add(item);
                }
            }
        }

        public bool IsCellVisible(Point2 location)
        {
            return _isVisible[location.X, location.Y];
        }

        public void SetCellVisible(Point2 location)
        {
            _isVisible[location.X, location.Y] = true;
        }

        public bool AreAllCellsVisible()
        {
            for (int column = 0; column < NumberOfColumns; ++column)
            {
                for (int row = 0; row < NumberOfRows; ++row)
                {
                    if (!_isVisible[column, row])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void SetAllCellsInvisible()
        {
            _isVisible = new bool[NumberOfColumns, NumberOfRows];
        }

        private string DebuggerDisplay => $"{{NumberOfLayers={NumberOfLayers}, NumberOfColumns={NumberOfColumns}, NumberOfRows={NumberOfRows}}}";
    }
}