using System;

namespace GameLogic
{
    public enum SettlementType
    {
        Outpost,
        Hamlet,
        Village,
        Town,
        City,
        Capital
    }

    public enum CitizenType
    {
        SubsistenceFarmer,
        AdditionalFarmer,
        Worker,
        Rebel
    }

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

    public enum Key
    {
        Up,
        Down,
        Left,
        Right,
        Enter,
        NumPad1,
        NumPad2,
        NumPad3,
        NumPad4,
        NumPad6,
        NumPad7,
        NumPad8,
        NumPad9
    }

    public delegate void UnitMovedEventHandler(object sender, UnitMovedEventArgs e);

    public class UnitMovedEventArgs : EventArgs
    {
        public Unit Unit { get; }

        public UnitMovedEventArgs(Unit unit)
        {
            Unit = unit;
        }
    }
}