using GeneralUtilities;

namespace GameLogic.NewLocationCalculators
{
    public class SouthCalculator : INewLocationCalculator
    {
        public Point2 Calculate(Point2 location)
        {
            return Point2.Create(location.X, location.Y + 1);
        }
    }
}