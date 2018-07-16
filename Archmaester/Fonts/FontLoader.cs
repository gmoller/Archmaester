using System.IO;
using System.Xml.Serialization;

namespace Archmaester.Fonts
{
    public class FontLoader
    {
        public static FontFile Load(string filename)
        {
            var deserializer = new XmlSerializer(typeof(FontFile));
            var textReader = new StreamReader(filename);
            var file = (FontFile)deserializer.Deserialize(textReader);
            textReader.Close();

            return file;
        }
    }
}