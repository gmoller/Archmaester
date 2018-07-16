using System.Diagnostics;

namespace GeneralUtilities
{
    /// <summary>
    /// Holds two integer values. Typically used for storing cartesian points.
    /// This struct is designed to be immutable.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public struct Point2
    {
        private readonly bool _isNull;

        public static readonly Point2 Empty = new Point2();
        public static readonly Point2 Null = new Point2(true);

        public int X { get; }
        public int Y { get; }

        private Point2(int x, int y)
        {
            X = x;
            Y = y;
            _isNull = false;
        }

        private Point2(bool isNull)
        {
            X = 0;
            Y = 0;
            _isNull = isNull;
        }

        public static Point2 Create(int x, int y)
        {
            return new Point2(x, y);
        }

        public bool IsNull()
        {
            return _isNull;
        }

        public static bool operator == (Point2 left, Point2 right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator != (Point2 left, Point2 right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point2)) return false;
            var comp = (Point2)obj;
            return comp.X == X && comp.Y == Y;
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public override string ToString()
        {
            return DebuggerDisplay;
        }

        private string DebuggerDisplay => $"{{X={X},Y={Y}}}";
    }
}