namespace Noise
{
    public class ValueNoise
    {
        public float GetNoise(float x, float y, Interp interp, int seed = 1337, float frequency = 0.01f)
        {
            x *= frequency;
            y *= frequency;

            return SingleValue(seed, x, y, interp);
        }

        public float GetNoise(float x, float y, float z, Interp interp, int seed = 1337, float frequency = 0.01f)
        {
            x *= frequency;
            y *= frequency;
            z *= frequency;

            return SingleValue(seed, x, y, z, interp);
        }

        private float SingleValue(int seed, float x, float y, Interp interp)
        {
            int x0 = Functions.FastFloor(x);
            int y0 = Functions.FastFloor(y);
            int x1 = x0 + 1;
            int y1 = y0 + 1;

            float xs, ys;
            switch (interp)
            {
                case Interp.Hermite:
                    xs = Functions.InterpHermiteFunc(x - x0);
                    ys = Functions.InterpHermiteFunc(y - y0);
                    break;
                case Interp.Quintic:
                    xs = Functions.InterpQuinticFunc(x - x0);
                    ys = Functions.InterpQuinticFunc(y - y0);
                    break;
                default: // Interp.Linear
                    xs = x - x0;
                    ys = y - y0;
                    break;
            }

            float xf0 = Functions.Lerp(Functions.ValCoord2D(seed, x0, y0), Functions.ValCoord2D(seed, x1, y0), xs);
            float xf1 = Functions.Lerp(Functions.ValCoord2D(seed, x0, y1), Functions.ValCoord2D(seed, x1, y1), xs);

            return Functions.Lerp(xf0, xf1, ys);
        }

        private float SingleValue(int seed, float x, float y, float z, Interp interp)
        {
            int x0 = Functions.FastFloor(x);
            int y0 = Functions.FastFloor(y);
            int z0 = Functions.FastFloor(z);
            int x1 = x0 + 1;
            int y1 = y0 + 1;
            int z1 = z0 + 1;

            float xs, ys, zs;
            switch (interp)
            {
                case Interp.Hermite:
                    xs = Functions.InterpHermiteFunc(x - x0);
                    ys = Functions.InterpHermiteFunc(y - y0);
                    zs = Functions.InterpHermiteFunc(z - z0);
                    break;
                case Interp.Quintic:
                    xs = Functions.InterpQuinticFunc(x - x0);
                    ys = Functions.InterpQuinticFunc(y - y0);
                    zs = Functions.InterpQuinticFunc(z - z0);
                    break;
                default: // Interp.Linear
                    xs = x - x0;
                    ys = y - y0;
                    zs = z - z0;
                    break;
            }

            float xf00 = Functions.Lerp(Functions.ValCoord3D(seed, x0, y0, z0), Functions.ValCoord3D(seed, x1, y0, z0), xs);
            float xf10 = Functions.Lerp(Functions.ValCoord3D(seed, x0, y1, z0), Functions.ValCoord3D(seed, x1, y1, z0), xs);
            float xf01 = Functions.Lerp(Functions.ValCoord3D(seed, x0, y0, z1), Functions.ValCoord3D(seed, x1, y0, z1), xs);
            float xf11 = Functions.Lerp(Functions.ValCoord3D(seed, x0, y1, z1), Functions.ValCoord3D(seed, x1, y1, z1), xs);

            float yf0 = Functions.Lerp(xf00, xf10, ys);
            float yf1 = Functions.Lerp(xf01, xf11, ys);

            return Functions.Lerp(yf0, yf1, zs);
        }
    }
}