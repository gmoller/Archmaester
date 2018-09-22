using System.Collections.Generic;
using GameLogic.Processors;
using GameMap;
using GeneralUtilities;

namespace GameLogic
{
    /// <summary>
    /// Contains accessors to the GameBoard, list of TerrainTypes and list of UnitTypes.
    /// This class is immutable.
    /// </summary>
    public class GameWorld
    {
        private readonly Dictionary<Key, CompassDirection> _movementMapping = new Dictionary<Key, CompassDirection>
        {
            {Key.NumPad1, CompassDirection.SouthWest},
            {Key.NumPad2, CompassDirection.South},
            {Key.NumPad3, CompassDirection.SouthEast},
            {Key.NumPad4, CompassDirection.West},
            {Key.NumPad6, CompassDirection.East},
            {Key.NumPad7, CompassDirection.NorthWest},
            {Key.NumPad8, CompassDirection.North},
            {Key.NumPad9, CompassDirection.NorthEast}
        };

        private GameBoard _gameBoard;
        private PlayerHuman _humanPlayer;
        private PlayerComputer _computerPlayer;

        public MovementProcessor MovementProcessor { get; }
        public IEnumerable<Settlement> PlayerSettlements => _humanPlayer.Settlements;
        public IEnumerable<Unit> HumanPlayerUnits => _humanPlayer.Units;
        public IEnumerable<Unit> ComputerPlayerUnits => _computerPlayer.Units;
        public Unit SelectedUnit => _humanPlayer.SelectedUnit;
        public int NumberOfColumns => _gameBoard.NumberOfColumns;
        public int NumberOfRows => _gameBoard.NumberOfRows;

        private GameWorld()
        {
            MovementProcessor = new MovementProcessor();
        }

        public static GameWorld Create()
        {
            return new GameWorld();
        }

        public void Intialize(int numberOfColumns, int numberOfRows, PlayerHuman humanPlayer, PlayerComputer computerPlayer)
        {
            int[,] terrain = MapGenerator.Generate(numberOfColumns, numberOfRows);
            GameBoard testMap = GameBoard.Create(1, terrain, true);
            _gameBoard = testMap;

            _humanPlayer = humanPlayer;
            _computerPlayer = computerPlayer;
        }

        //public void KeyPressed(Key key)
        //{
        //    if (key == Key.Enter)
        //    {
        //        if (!_humanPlayer.SelectedUnit.Equals(Unit.Null))
        //        {
        //            _humanPlayer.EndTurn();
        //        }
        //        return;
        //    }

        //    CompassDirection direction = DetermineDirectionToMove(key);
        //    if (direction != CompassDirection.None)
        //    {
        //        _humanPlayer.MoveSelectedUnit(direction);
        //    }
        //}

        //private CompassDirection DetermineDirectionToMove(Key key)
        //{
        //    CompassDirection direction;
        //    _movementMapping.TryGetValue(key, out direction);

        //    return direction;
        //}

        public void StartTurnForHumanPlayer()
        {
            _humanPlayer.StartTurn();
        }

        public void EndTurnForHumanPlayer()
        {
            if (!_humanPlayer.SelectedUnit.Equals(Unit.Null))
            {
                _humanPlayer.EndTurn();
            }
        }

        public void DoTurnForComputerPlayer()
        {
            _computerPlayer.DoTurn();
        }

        public void EndTurnForComputerPlayer()
        {
            _computerPlayer.EndTurn();
        }

        internal Cell GetCell(Point2 location)
        {
            return _gameBoard.GetCell(location);
        }

        public int GetTerrainTypeIdOfCell(Point2 location)
        {
            return _gameBoard.GetCell(location).TerrainTypeId;
        }

        public List<int> GetNeighboringTerrainTypeIds(Point2 cellLocation)
        {
            return _gameBoard.GetNeighboringTerrainTypeIds(cellLocation);
        }

        public List<Point2> GetCellNeighbors(Point2 location)
        {
            return _gameBoard.GetCellNeighbors(location);
        }

        public bool IsCellVisible(Point2 location)
        {
            return _gameBoard.IsCellVisible(location);
        }

        internal void SetCellVisible(Point2 location)
        {
            if (location.X < 0 || location.X > _gameBoard.NumberOfColumns - 1) return;
            if (location.Y < 0 || location.Y > _gameBoard.NumberOfRows - 1) return;

            _gameBoard.SetCellVisible(location);
        }

        internal bool AreAllCellsVisible()
        {
            return _gameBoard.AreAllCellsVisible();
        }

        internal void SetAllCellsInvisible()
        {
            _gameBoard.SetAllCellsInvisible();
        }

        public bool IsPlayerSettlementOnCell(Point2 location)
        {
            foreach (Settlement item in Globals.Instance.GameWorld.PlayerSettlements)
            {
                if (item.Location == location)
                {
                    return true;
                }
            }

            return false;
        }

        public Settlement GetPlayerSettlementOnCell(Point2 location)
        {
            foreach (Settlement item in Globals.Instance.GameWorld.PlayerSettlements)
            {
                if (item.Location == location)
                {
                    return item;
                }
            }

            return null;
        }
    }
}