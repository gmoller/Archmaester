using System;
using System.Collections.Generic;
using GameData;
using GameMap;
using GeneralUtilities;

namespace GameLogic
{
    public class PlayerHuman
    {
        private readonly List<Settlement> _settlements = new List<Settlement>();
        public IEnumerable<Settlement> Settlements => _settlements;

        private List<Unit> _units = new List<Unit>();
        private int _selectedUnitIndex = -1;

        public IEnumerable<Unit> Units => _units;
        public Unit SelectedUnit
        {
            get
            {
                if (_selectedUnitIndex == -1)
                {
                    return Unit.Null;
                }

                return _units[_selectedUnitIndex];
            }
        }

        public event UnitMovedEventHandler UnitMoved;
        public event EventHandler TurnEnded;

        public void MoveSelectedUnit(CompassDirection direction)
        {
            if (_selectedUnitIndex == -1) return;

            Unit unit = _units[_selectedUnitIndex].DoAction("Move", direction);
            OnUnitMoved(new UnitMovedEventArgs(unit));
            _units[_selectedUnitIndex] = unit;

            if (unit.MovementPoints <= 0)
            {
                _selectedUnitIndex++;
                if (_selectedUnitIndex > _units.Count - 1)
                {
                    _selectedUnitIndex = -1;
                }
            }
        }

        private void OnUnitMoved(UnitMovedEventArgs e)
        {
            //Interlocked.CompareExchange(ref TurnEnded, null, null)?.Invoke(this, e);
            UnitMoved?.Invoke(this, e);
        }

        public void AddSettlement(string name, Point2 location, RaceType raceType)
        {
            Settlement settlement = Settlement.CreateNew(name, raceType, location);
            _settlements.Add(settlement);
        }

        public void AddUnit(int unitType, Point2 startLocation)
        {
            Unit unit = Unit.CreateNew(unitType, startLocation);
            _units.Add(unit);
            _selectedUnitIndex = _units.Count - 1;
        }

        public void StartTurn()
        {
            List<Unit> units = new List<Unit>(_units.Count);

            foreach (Unit item in _units)
            {
                Unit unit = item.StartNewTurn();
                units.Add(unit);
                CellVisibilitySetter.SetCellVisibility(unit.Location, Globals.Instance.GameWorld);
            }

            _units = units;
            if (units.Count > 0)
            {
                _selectedUnitIndex = 0;
            }
        }

        public void EndTurn()
        {
            // each settlement must have its residents increased and build stuff
            foreach (Settlement item in _settlements)
            {
                item.EndTurn();
            }

            // raise event here to inform listeners that turn has been ended
            OnTurnEnded(EventArgs.Empty);
        }

        private void OnTurnEnded(EventArgs e)
        {
            //Interlocked.CompareExchange(ref TurnEnded, null, null)?.Invoke(this, e);
            TurnEnded?.Invoke(this, e);
        }

        //private Unit Explore(Unit item)
        //{
        //    Point2 newLocation = item.Explore();

        //    Cell cell = _gameWorld.GetCell(newLocation);
        //    TerrainType terrainType = Globals.Instance.TerrainTypes[cell.TerrainTypeId];
        //    int movementCost = terrainType.MovementCost;

        //    if (item.MovementPoints - movementCost >= 0)
        //    {
        //        Unit unit = Unit.Create(item.UnitType, newLocation, item.MovementPoints - movementCost, _gameWorld);
        //        return unit;
        //        //result = $"{item.ToString()} -> {unit.ToString()}";
        //    }
        //    else // can't move, not enough points
        //    {
        //        // TODO: fix bug where if not enough movement points, unit keeps to trying to move to same spot and gets stuck!
        //        return item;
        //        //result = "Nothing";
        //    }

        //    return item;
        //}
    }
}