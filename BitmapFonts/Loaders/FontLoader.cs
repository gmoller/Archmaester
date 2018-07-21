using System.IO;
using System.Linq;

namespace BitmapFonts.Loaders
{
    public class FontLoader
    {
        public static FontFile Load(string filename)
        {
            IFontLoader fontLoader = GetFontLoader(filename);
            FontFile fontFile = fontLoader.ReadFile(filename);

            return fontFile;
        }

        private static IFontLoader GetFontLoader(string filename)
        {
            string firstLine = File.ReadLines(filename).First();

            IFontLoader fontLoader;
            if (firstLine.StartsWith("<?xml"))
            {
                fontLoader = new XmlFontLoader();
            }
            else
            {
                fontLoader = new TextFontLoader();
            }

            return fontLoader;
        }
    }
}