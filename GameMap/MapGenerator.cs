using System;

namespace GameMap
{
    public class MapGenerator
    {
        public static int[,] Generate(int numberOfColumns, int numberOfRows)
        {
            // make some noise!
            float[,] noise = MakeNoise(numberOfColumns, numberOfRows);

            int[,] terrain = TurnNoiseIntoTerrain(noise);

            return terrain;
        }

        private static float[,] MakeNoise(int numberOfColumns, int numberOfRows)
        {
            var noise = new float[numberOfColumns, numberOfRows];
            for (float y = 0.0f; y < numberOfRows; ++y)
            {
                for (float x = 0.0f; x < numberOfColumns; ++x)
                {
                    float val = GetNoise(x, y, FastNoise.Interp.Linear);
                    noise[(int)x, (int)y] = val;
                }
            }

            return noise;
        }

        private static float GetNoise(float x, float y, FastNoise.Interp interp)
        {
            var fastNoise = new FastNoise();
            fastNoise.SetNoiseType(FastNoise.NoiseType.Value);
            fastNoise.SetInterp(interp);
            fastNoise.SetSeed(1336);
            fastNoise.SetFrequency(0.1f);
            float val = fastNoise.GetNoise(x, y);

            return val;
        }

        private static int[,] TurnNoiseIntoTerrain(float[,] noise)
        {
            int numberOfColumns = noise.GetLength(0);
            int numberOfRows = noise.GetLength(1);
            var terrain = new int[numberOfColumns, numberOfRows];

            for (int y = 0; y < numberOfRows; ++y)
            {
                for (int x = 0; x < numberOfColumns; ++x)
                {
                    float val = noise[x, y];
                    int terrainTypeId = DetermineTerrainTypeId(val);
                    terrain[x, y] = terrainTypeId;
                }
            }

            return terrain;
        }

        private static int DetermineTerrainTypeId(float val)
        {
            int terrainTypeId = -1;

            if (IsOcean(val))
            {
                terrainTypeId = 11; // ocean
            }
            else if (IsGrassland(val))
            {
                terrainTypeId = 0; // grassland
            }
            else if (IsForest(val))
            {
                terrainTypeId = 1; // forest
            }
            else if (IsHill(val))
            {
                terrainTypeId = 6; // hill
            }
            else if (IsMountain(val))
            {
                terrainTypeId = 7; // mountain
            }

            if (terrainTypeId == -1)
            {
                throw new Exception($"That was unexpected! Val of {val} not supported.");
            }

            return terrainTypeId;
        }

        private static bool IsOcean(float val)
        {
            return val < -0.0f;
        }

        private static bool IsGrassland(float val)
        {
            return val >= 0.0f && val < 0.4f;
        }

        private static bool IsForest(float val)
        {
            return val >= 0.4f && val < 0.6f;
        }

        private static bool IsHill(float val)
        {
            return val >= 0.6f && val < 0.7f;
        }

        private static bool IsMountain(float val)
        {
            return val >= 0.7f && val <= 1.0f;
        }
    }
}