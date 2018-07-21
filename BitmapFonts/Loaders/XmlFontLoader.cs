using System.IO;
using System.Xml.Serialization;

namespace BitmapFonts.Loaders
{
    public class XmlFontLoader : IFontLoader
    {
        public FontFile ReadFile(string filename)
        {
            var deserializer = new XmlSerializer(typeof(FontFile));
            var textReader = new StreamReader(filename);
            var file = (FontFile)deserializer.Deserialize(textReader);
            textReader.Close();

            return file;
        }
    }
}