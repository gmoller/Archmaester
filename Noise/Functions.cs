using System.Runtime.CompilerServices;

namespace Noise
{
    internal static class Functions
    {
        private const int XPrime = 1619;
        private const int YPrime = 31337;
        private const int ZPrime = 6971;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int FastFloor(float f) { return (f >= 0 ? (int)f : (int)f - 1); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float InterpHermiteFunc(float t) { return t * t * (3 - 2 * t); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float InterpQuinticFunc(float t) { return t * t * t * (t * (t * 6 - 15) + 10); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float Lerp(float a, float b, float t) { return a + t * (b - a); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float ValCoord2D(int seed, int x, int y)
        {
            int n = seed;
            n ^= XPrime * x;
            n ^= YPrime * y;

            return n * n * n * 60493 / (float)2147483648.0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float ValCoord3D(int seed, int x, int y, int z)
        {
            int n = seed;
            n ^= XPrime * x;
            n ^= YPrime * y;
            n ^= ZPrime * z;

            return (n * n * n * 60493) / (float)2147483648.0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float GradCoord2D(int seed, int x, int y, float xd, float yd)
        {
            int hash = seed;
            hash ^= XPrime * x;
            hash ^= YPrime * y;

            hash = hash * hash * hash * 60493;
            hash = (hash >> 13) ^ hash;

            Float2 g = Grad_2D[hash & 7];

            return xd * g.X + yd * g.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float GradCoord3D(int seed, int x, int y, int z, float xd, float yd, float zd)
        {
            int hash = seed;
            hash ^= XPrime * x;
            hash ^= YPrime * y;
            hash ^= ZPrime * z;

            hash = hash * hash * hash * 60493;
            hash = (hash >> 13) ^ hash;

            Float3 g = Grad_3D[hash & 15];

            return xd * g.X + yd * g.Y + zd * g.Z;
        }

        private struct Float2
        {
            public float X { get; }
            public float Y { get; }

            public Float2(float x, float y)
            {
                X = x;
                Y = y;
            }
        }

        private struct Float3
        {
            public float X { get; }
            public float Y { get; }
            public float Z { get; }

            public Float3(float x, float y, float z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }

        private static readonly Float2[] Grad_2D = {
            new Float2(-1,-1), new Float2( 1,-1), new Float2(-1, 1), new Float2( 1, 1),
            new Float2( 0,-1), new Float2(-1, 0), new Float2( 0, 1), new Float2( 1, 0),
        };

        private static readonly Float3[] Grad_3D = {
            new Float3( 1, 1, 0), new Float3(-1, 1, 0), new Float3( 1,-1, 0), new Float3(-1,-1, 0),
            new Float3( 1, 0, 1), new Float3(-1, 0, 1), new Float3( 1, 0,-1), new Float3(-1, 0,-1),
            new Float3( 0, 1, 1), new Float3( 0,-1, 1), new Float3( 0, 1,-1), new Float3( 0,-1,-1),
            new Float3( 1, 1, 0), new Float3( 0,-1, 1), new Float3(-1, 1, 0), new Float3( 0,-1,-1),
        };
    }
}