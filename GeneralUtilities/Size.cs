using System;

namespace GeneralUtilities
{
    /// <summary>
    ///     A two dimensional size defined by two integral numbers, a width and a height.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A size is a subspace of two-dimensional space, the area of which is described in terms of a two-dimensional
    ///         coordinate system, given by a reference point and two coordinate axes.
    ///     </para>
    /// </remarks>
    public struct Size : IEquatable<Size>
    {
        /// <summary>
        ///     Returns a Size with Width and Height equal to 0.
        /// </summary>
        public static readonly Size Empty = new Size();

        /// <summary>
        ///     The horizontal component of this Size.
        /// </summary>
        public int Width;

        /// <summary>
        ///     The vertical component of this Size.
        /// </summary>
        public int Height;

        /// <summary>
        ///     Gets a value that indicates whether this Size is empty.
        /// </summary>
        public bool IsEmpty => Width == 0 && Height == 0;

        /// <summary>
        ///     Initializes a new instance of the Size structure from the specified dimensions.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        ///     Compares two Size structures. The result specifies
        ///     whether the values of the Width and Height
        ///     fields of the two Size structures are equal.
        /// </summary>
        /// <param name="first">The first size.</param>
        /// <param name="second">The second size.</param>
        /// <returns>
        ///     true if the Width and Height
        ///     fields of the two Size structures are equal; otherwise, false.
        /// </returns>
        public static bool operator ==(Size first, Size second)
        {
            return first.Equals(ref second);
        }

        /// <summary>
        ///     Indicates whether this Size is equal to another Size.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>
        ///     true if this Size is equal to the size parameter; otherwise,
        ///     false.
        /// </returns>
        public bool Equals(Size size)
        {
            return Equals(ref size);
        }

        /// <summary>
        ///     Indicates whether this Size is equal to another Size.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>
        ///     true if this Size is equal to the size; otherwise,
        ///     false.
        /// </returns>
        public bool Equals(ref Size size)
        {
            return Width == size.Width && Height == size.Height;
        }

        /// <summary>
        ///     Returns a value indicating whether this Size is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to make the comparison with.</param>
        /// <returns>
        ///     true if this Size is equal to obj />; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is Size)
                return Equals((Size)obj);
            return false;
        }

        /// <summary>
        ///     Compares two Size structures. The result specifies
        ///     whether the values of the Width or Height
        ///     fields of the two Size structures are unequal.
        /// </summary>
        /// <param name="first">The first size.</param>
        /// <param name="second">The second size.</param>
        /// <returns>
        ///     true if the Width or Height
        ///     fields of the two Size structures are unequal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(Size first, Size second)
        {
            return !(first == second);
        }

        /// <summary>
        ///     Calculates the Size representing the vector addition of two Size structures as if
        ///     they were Vector2 structures.
        /// </summary>
        /// <param name="first">The first size.</param>
        /// <param name="second">The second size.</param>
        /// <returns>
        ///     The Size representing the vector addition of two Size structures as if they
        ///     were Vector2 structures.
        /// </returns>
        public static Size operator +(Size first, Size second)
        {
            return Add(first, second);
        }

        /// <summary>
        ///     Calculates the Size representing the vector addition of two Size structures.
        /// </summary>
        /// <param name="first">The first size.</param>
        /// <param name="second">The second size.</param>
        /// <returns>
        ///     The Size representing the vector addition of two Size structures.
        /// </returns>
        public static Size Add(Size first, Size second)
        {
            Size size;
            size.Width = first.Width + second.Width;
            size.Height = first.Height + second.Height;
            return size;
        }

        /// <summary>
        /// Calculates the Size representing the vector subtraction of two Size structures.
        /// </summary>
        /// <param name="first">The first size.</param>
        /// <param name="second">The second size.</param>
        /// <returns>
        ///     The Size representing the vector subtraction of two Size structures.
        /// </returns>
        public static Size operator -(Size first, Size second)
        {
            return Subtract(first, second);
        }

        public static Size operator /(Size size, int value)
        {
            return new Size(size.Width / value, size.Height / value);
        }

        public static Size operator *(Size size, int value)
        {
            return new Size(size.Width * value, size.Height * value);
        }

        /// <summary>
        ///     Calculates the Size representing the vector subtraction of two Size structures.
        /// </summary>
        /// <param name="first">The first size.</param>
        /// <param name="second">The second size.</param>
        /// <returns>
        ///     The Size representing the vector subtraction of two Size structures.
        /// </returns>
        public static Size Subtract(Size first, Size second)
        {
            Size size;
            size.Width = first.Width - second.Width;
            size.Height = first.Height - second.Height;
            return size;
        }

        /// <summary>
        ///     Returns a hash code of this Size suitable for use in hashing algorithms and data
        ///     structures like a hash table.
        /// </summary>
        /// <returns>
        ///     A hash code of this Size.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                // ReSharper disable NonReadonlyMemberInGetHashCode
                return (Width.GetHashCode() * 397) ^ Height.GetHashCode();
                // ReSharper restore NonReadonlyMemberInGetHashCode
            }
        }

        /// <summary>
        ///     Returns a string that represents this Size.
        /// </summary>
        /// <returns>
        ///     A string that represents this Size.
        /// </returns>
        public override string ToString()
        {
            return $"Width: {Width}, Height: {Height}";
        }
    }
}