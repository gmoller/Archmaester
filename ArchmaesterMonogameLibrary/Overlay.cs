using Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Textures;

namespace ArchmaesterMonogameLibrary
{
    public class Overlay
    {
        private readonly Dictionary<byte[], int> _test;
        private Dictionary<byte[], int> _missing;

        public Overlay()
        {
            _test = new Dictionary<byte[], int>(new MyEqualityComparer());
            _missing = new Dictionary<byte[], int>(new MyEqualityComparer());

            _test.Add(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 11 }, 99); // not tested

            _test.Add(new byte[] { 0, 11, 11, 11, 11, 11, 11, 11, 11 }, 7);
            _test.Add(new byte[] { 11, 0, 11, 11, 11, 11, 11, 11, 11 }, 1);
            _test.Add(new byte[] { 11, 11, 0, 11, 11, 11, 11, 11, 11 }, 0);
            _test.Add(new byte[] { 11, 11, 11, 0, 11, 11, 11, 11, 11 }, 11);
            _test.Add(new byte[] { 11, 11, 11, 11, 0, 11, 11, 11, 11 }, 17);
            _test.Add(new byte[] { 11, 11, 11, 11, 11, 0, 11, 11, 11 }, 14);
            _test.Add(new byte[] { 11, 11, 11, 11, 11, 11, 0, 11, 11 }, 21);
            _test.Add(new byte[] { 11, 11, 11, 11, 11, 11, 11, 0, 11 }, 4);

            _test.Add(new byte[] { 0, 0, 11, 11, 11, 11, 11, 11, 11 }, 7);
            _test.Add(new byte[] { 11, 0, 0, 11, 11, 11, 11, 11, 11 }, 0);
            _test.Add(new byte[] { 11, 11, 0, 0, 11, 11, 11, 11, 11 }, 0);
            _test.Add(new byte[] { 11, 11, 11, 0, 0, 11, 11, 11, 11 }, 17);
            _test.Add(new byte[] { 11, 11, 11, 11, 0, 0, 11, 11, 11 }, 17);
            _test.Add(new byte[] { 11, 11, 11, 11, 11, 0, 0, 11, 11 }, 21);
            _test.Add(new byte[] { 11, 11, 11, 11, 11, 11, 0, 0, 11 }, 21);
            _test.Add(new byte[] { 0, 11, 11, 11, 11, 11, 11, 0, 11 }, 7);

            _test.Add(new byte[] { 0, 0, 0, 11, 11, 11, 11, 11, 11 }, 3);
            _test.Add(new byte[] { 11, 0, 0, 0, 11, 11, 11, 11, 11 }, 0);
            _test.Add(new byte[] { 11, 11, 0, 0, 0, 11, 11, 11, 11 }, 12);
            _test.Add(new byte[] { 11, 11, 11, 0, 0, 0, 11, 11, 11 }, 17);
            _test.Add(new byte[] { 11, 11, 11, 11, 0, 0, 0, 11, 11 }, 15);
            _test.Add(new byte[] { 11, 11, 11, 11, 11, 0, 0, 0, 11 }, 21);
            _test.Add(new byte[] { 0, 11, 11, 11, 11, 11, 0, 0, 11 }, 6);
            _test.Add(new byte[] { 0, 0, 11, 11, 11, 11, 11, 0, 11 }, 7);

            _test.Add(new byte[] { 0, 0, 0, 0, 11, 11, 11, 11, 11 }, 3);
            _test.Add(new byte[] { 11, 0, 0, 0, 0, 11, 11, 11, 11 }, 12);
            _test.Add(new byte[] { 11, 11, 0, 0, 0, 0, 11, 11, 11 }, 12);
            _test.Add(new byte[] { 11, 11, 11, 0, 0, 0, 0, 11, 11 }, 15);
            _test.Add(new byte[] { 11, 11, 11, 11, 0, 0, 0, 0, 11 }, 15);
            _test.Add(new byte[] { 0, 11, 11, 11, 11, 0, 0, 0, 11 }, 6);
            _test.Add(new byte[] { 0, 0, 11, 11, 11, 11, 0, 0, 11 }, 6);
            _test.Add(new byte[] { 0, 0, 0, 11, 11, 11, 11, 0, 11 }, 3);

            _test.Add(new byte[] { 0, 0, 0, 0, 0, 11, 11, 11, 11 }, 0); // wrong image - don't have
            _test.Add(new byte[] { 11, 0, 0, 0, 0, 0, 11, 11, 11 }, 12);
            _test.Add(new byte[] { 11, 11, 0, 0, 0, 0, 0, 11, 11 }, 0); // wrong image - don't have
            _test.Add(new byte[] { 11, 11, 11, 0, 0, 0, 0, 0, 11 }, 15);
            _test.Add(new byte[] { 0, 11, 11, 11, 0, 0, 0, 0, 11 }, 0); // wrong image - don't have
            _test.Add(new byte[] { 0, 0, 11, 11, 11, 0, 0, 0, 11 }, 6);
            _test.Add(new byte[] { 0, 0, 0, 11, 11, 11, 0, 0, 11 }, 0); // wrong image - don't have
            _test.Add(new byte[] { 0, 0, 0, 0, 11, 11, 11, 0, 11 }, 3);

            //////////////////////////////

            _test.Add(new byte[] { 11, 11, 11, 0, 11, 0, 11, 11, 11 }, 0); // wrong image - don't have
            _test.Add(new byte[] { 0, 11, 11, 11, 0, 0, 0, 11, 11 }, 15);
            _test.Add(new byte[] { 11, 0, 11, 0, 0, 11, 11, 11, 11 }, 0); // wrong image - don't have
            _test.Add(new byte[] { 0, 0, 0, 0, 0, 0, 11, 11, 11 }, 0); // wrong image - don't have
            _test.Add(new byte[] { 11, 0, 11, 11, 11, 11, 11, 0, 11 }, 0); // wrong image - don't have
            _test.Add(new byte[] { 11, 11, 11, 11, 11, 0, 11, 0, 11 }, 0); // wrong image - don't have
            _test.Add(new byte[] { 11, 0, 11, 0, 11, 11, 11, 11, 11 }, 0); // wrong image - don't have
            _test.Add(new byte[] { 0, 0, 0, 0, 11, 0, 0, 0, 11 }, 0); // wrong image - don't have
            _test.Add(new byte[] { 11, 0, 0, 0, 11, 11, 0, 0, 11 }, 0); // wrong image - don't have
            _test.Add(new byte[] { 0, 11, 11, 11, 0, 11, 11, 11, 11 }, 0); // wrong image - don't have
            _test.Add(new byte[] { 11, 0, 0, 11, 11, 11, 11, 0, 11 }, 0); // wrong image - don't have
            _test.Add(new byte[] { 0, 0, 11, 0, 0, 11, 11, 11, 11 }, 0); // wrong image - don't have
            _test.Add(new byte[] { 0, 0, 11, 0, 0, 0, 11, 0, 11 }, 0); // wrong image - don't have
            _test.Add(new byte[] { 0, 0, 0, 0, 0, 0, 11, 0, 11 }, 0); // wrong image - don't have
            _test.Add(new byte[] { 11, 11, 11, 11, 11, 11, 11, 11, 11 }, -1);

            // edge of map
            _test.Add(new byte[] { 255, 255, 11, 11, 11, 0, 0, 255, 11 }, 21);
            _test.Add(new byte[] { 255, 255, 11, 11, 11, 11, 11, 255, 11 }, -1);
            _test.Add(new byte[] { 0, 0, 11, 11, 11, 255, 255, 255, 11 }, 7);
            _test.Add(new byte[] { 11, 11, 11, 11, 11, 255, 255, 255, 11 }, -1);
            _test.Add(new byte[] { 11, 11, 11, 0, 0, 255, 255, 255, 11 }, 17);
            _test.Add(new byte[] { 11, 11, 11, 0, 11, 255, 255, 255, 11 }, 11);
            _test.Add(new byte[] { 11, 11, 0, 0, 0, 255, 255, 255, 11 }, 12);
            _test.Add(new byte[] { 0, 11, 11, 11, 11, 255, 255, 255, 11 }, 7);
            _test.Add(new byte[] { 0, 11, 11, 255, 255, 255, 0, 0, 11 }, 6);
            _test.Add(new byte[] { 11, 11, 11, 255, 255, 255, 11, 0, 11 }, 4);
            _test.Add(new byte[] { 11, 11, 11, 255, 255, 255, 11, 11, 11 }, -1);
            _test.Add(new byte[] { 11, 0, 0, 255, 255, 255, 11, 11, 11 }, 0);
            _test.Add(new byte[] { 11, 11, 11, 255, 255, 255, 0, 0, 11 }, 21);
            _test.Add(new byte[] { 11, 0, 11, 255, 255, 255, 11, 11, 11 }, 1);
            _test.Add(new byte[] { 0, 0, 0, 255, 255, 255, 11, 11, 11 }, 3);
            _test.Add(new byte[] { 11, 11, 0, 255, 255, 255, 11, 11, 11 }, 0);
            _test.Add(new byte[] { 11, 11, 11, 255, 255, 255, 0, 11, 11 }, 21);
            _test.Add(new byte[] { 11, 255, 255, 255, 11, 11, 11, 11, 11 }, -1);
            _test.Add(new byte[] { 11, 255, 255, 255, 255, 255, 11, 11, 11 }, -1);
            _test.Add(new byte[] { 0, 255, 255, 255, 11, 11, 11, 0, 11 }, 7);
            _test.Add(new byte[] { 11, 255, 255, 255, 0, 0, 11, 11, 11 }, 17);
            _test.Add(new byte[] { 11, 255, 255, 255, 11, 11, 11, 0, 11 }, 4);
            _test.Add(new byte[] { 0, 255, 255, 255, 11, 11, 0, 0, 11 }, 6);
            _test.Add(new byte[] { 11, 255, 255, 255, 0, 11, 11, 11, 11 }, 17);
            _test.Add(new byte[] { 0, 255, 255, 255, 11, 11, 11, 11, 11 }, 17);
            _test.Add(new byte[] { 255, 255, 0, 11, 11, 11, 11, 255, 11 }, 0);
            _test.Add(new byte[] { 255, 255 , 11, 11, 11, 11, 0, 255, 11 }, 21);
            _test.Add(new byte[] { 255, 255, 11, 0, 11, 11, 11, 255, 11 }, 11);
            _test.Add(new byte[] { 255, 255, 11, 0, 0, 11, 11, 255, 11 }, 17);
            _test.Add(new byte[] { 255, 255, 11, 0, 0, 0, 11, 255, 11 }, 17);
            _test.Add(new byte[] { 255, 255, 11, 11, 0, 0, 11, 255, 11 }, 17);
            _test.Add(new byte[] { 255, 255, 11, 11, 11, 0, 11, 255, 11 }, 14);
            _test.Add(new byte[] { 255, 255, 0, 0, 0, 0, 11, 255, 11 }, 12);
            _test.Add(new byte[] { 255, 255, 0, 0, 11, 11, 11, 255, 11 }, 0);
        }

        public void DrawFrame(int cellTerrainType, List<int> neighboringTerrainTypeIds, Rectangle rectangle, ITexture2D texture, SpriteBatch spriteBatch)
        {
            var search = new[] { (byte)neighboringTerrainTypeIds[0], (byte)neighboringTerrainTypeIds[1], (byte)neighboringTerrainTypeIds[2],
                                 (byte)neighboringTerrainTypeIds[3], (byte)neighboringTerrainTypeIds[4], (byte)neighboringTerrainTypeIds[5],
                                 (byte)neighboringTerrainTypeIds[6], (byte)neighboringTerrainTypeIds[7], (byte)cellTerrainType };

            if (_test.ContainsKey(search))
            {
                int frame = _test[search];
                if (frame != -1)
                {
                    spriteBatch.Draw(texture, rectangle, texture.Frames[frame].Rectangle, Color.White);
                }
            }
            else
            {
                //if (cellTerrainType == 11)
                {
                    //_missing[search] = 0;
                }
            }
        }
    }

    public class MyEqualityComparer : IEqualityComparer<byte[]>
    {
        public bool Equals(byte[] x, byte[] y)
        {
            if (x.Length != y.Length)
            {
                return false;
            }
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode(byte[] obj)
        {
            int result = 17;
            for (int i = 0; i < obj.Length; i++)
            {
                unchecked
                {
                    result = result * 23 + obj[i];
                }
            }

            return result;
        }
    }
}