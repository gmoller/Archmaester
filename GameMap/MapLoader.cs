using System;
using System.IO;

namespace GameMap
{
    public class MapLoader
    {
        public static int[,] Load(string filename)
        {
            byte[] data = File.ReadAllBytes(filename);

            int[,] terrain = SetState(data);

            return terrain;
        }

        private static int[,] SetState(byte[] data)
        {
            ValidateVersion(data[0], data[1]);

            // number of layers, columns, rows
            byte numberOfLayers = data[2];
            int numberOfColumns = BitConverter.ToInt32(data, 3);
            int numberOfRows = BitConverter.ToInt32(data, 7);

            int[,] terrain = new int[numberOfColumns, numberOfRows];

            // cells
            int cursor = 11;
            int layer = 0;
            int column = 0;
            int row = 0;
            for (int i = cursor; i < data.Length - numberOfLayers; i += 2)
            {
                byte b2 = data[i + 1];

                if (b2 == 0) b2 = 1;
                else if (b2 == 7) b2 = 6;
                else if (b2 == 8) b2 = 7;
                else if (b2 == 9) b2 = 11;
                else if (b2 == 10) b2 = 0;
                terrain[column, row] = b2;

                row++;
                if (row > numberOfRows - 1)
                {
                    row = 0;
                    column++;
                    if (column > numberOfColumns - 1)
                    {
                        column = 0;
                        layer++;
                    }
                }
            }

            return terrain;
        }

        private static void ValidateVersion(byte firstByte, byte secondByte)
        {
            if (firstByte != 0x00 || secondByte != 0x01)
            {
                throw new Exception("Only version 1 supported!");
            }
        }
    }
}