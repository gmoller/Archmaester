﻿using GeneralUtilities;

namespace GameLogic.NewLocationCalculators
{
    public class EastCalculator : INewLocationCalculator
    {
        public Point2 Calculate(Point2 location)
        {
            return Point2.Create(location.X + 1, location.Y);
        }
    }
}