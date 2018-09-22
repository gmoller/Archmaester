using GameLogic.NewLocationCalculators;
using GameLogic.Processors;
using GameMap;

namespace GameLogic.Actions
{
    public class MoveAction : IAct
    {
        public Unit Execute(Unit unit, object parameters)
        {
            var compassDirection = (CompassDirection)parameters;
            INewLocationCalculator newLocationCalculator = NewLocationCalculatorFactory.GetNewLocationCalculator(compassDirection);
            ProcessResponse response = Globals.Instance.GameWorld.MovementProcessor.Process(new ProcessRequest(unit.Location, unit.MovementPoints), newLocationCalculator);

            Unit ret = Unit.Create(unit.UnitType, response.NewLocation, response.NewMovementPoints);

            return ret;
        }
    }
}