using GeneralUtilities;

namespace GameLogic.NewLocationCalculators
{
    public interface INewLocationCalculator
    {
        Point2 Calculate(Point2 location);
    }
}