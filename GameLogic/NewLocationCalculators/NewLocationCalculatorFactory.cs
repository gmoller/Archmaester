using System.Collections.Generic;
using GameMap;

namespace GameLogic.NewLocationCalculators
{
    public static class NewLocationCalculatorFactory
    {
        private static readonly Dictionary<CompassDirection, INewLocationCalculator> Calculators;

        static NewLocationCalculatorFactory()
        {
            Calculators = new Dictionary<CompassDirection, INewLocationCalculator>
            {
                {CompassDirection.North, new NorthCalculator()},
                {CompassDirection.NorthEast, new NorthEastCalculator()},
                {CompassDirection.East, new EastCalculator()},
                {CompassDirection.SouthEast, new SouthEastCalculator()},
                {CompassDirection.South, new SouthCalculator()},
                {CompassDirection.SouthWest, new SouthWestCalculator()},
                {CompassDirection.West, new WestCalculator()},
                {CompassDirection.NorthWest, new NorthWestCalculator()}
            };
        }

        public static INewLocationCalculator GetNewLocationCalculator(CompassDirection direction)
        {
            return Calculators[direction];
        }
    }
}