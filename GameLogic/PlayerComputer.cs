using System.Collections.Generic;
using GeneralUtilities;

namespace GameLogic
{
    /// <summary>
    /// A computer player.
    /// </summary>
    public class PlayerComputer
    {
        private List<Unit> _units;

        public IEnumerable<Unit> Units => _units;

        public event UnitMovedEventHandler UnitMoved;

        public PlayerComputer()
        {
            _units = new List<Unit>();
        }

        public void AddUnit(int unitType, Point2 startLocation)
        {
            Unit unit = Unit.CreateNew(unitType, startLocation);
            _units.Add(unit);
        }

        public void DoTurn()
        {
            List<Unit> units = new List<Unit>(_units.Count);

            foreach (Unit item in _units)
            {
                // decide what to do
                int direction = Globals.Instance.GetRandomNumber(0, 7);

                Unit unit = item.DoAction("Move", direction);
                OnUnitMoved(new UnitMovedEventArgs(unit));
                units.Add(unit);
            }

            _units = units;
        }

        private void OnUnitMoved(UnitMovedEventArgs e)
        {
            //Interlocked.CompareExchange(ref TurnEnded, null, null)?.Invoke(this, e);
            UnitMoved?.Invoke(this, e);
        }

        public void EndTurn()
        {
            List<Unit> units = new List<Unit>(_units.Count);

            foreach (Unit item in _units)
            {
                Unit unit = item.StartNewTurn();
                units.Add(unit);
            }

            _units = units;
        }
    }
}