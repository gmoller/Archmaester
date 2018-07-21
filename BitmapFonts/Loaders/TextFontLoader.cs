using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace BitmapFonts.Loaders
{
    public class TextFontLoader : IFontLoader
    {
        public FontFile ReadFile(string filename)
        {
            IEnumerable<string> lines = File.ReadLines(filename);

            FontFile fontFile = new FontFile
            {
                Pages = new List<FontPage>(),
                Chars = new List<FontChar>(),
                Kernings = new List<FontKerning>()
            };

            foreach (string line in lines)
            {
                Handler handler;
                if (line.StartsWith("info"))
                {
                    handler = new InfoHandler();
                }
                else if (line.StartsWith("common"))
                {
                    handler = new CommonHandler();
                }
                else if (line.StartsWith("page"))
                {
                    handler = new PageHandler();
                }
                else if (line.StartsWith("chars"))
                {
                    handler = new CharsHandler();
                }
                else if (line.StartsWith("char"))
                {
                    handler = new CharHandler();
                }
                else
                {
                    throw new Exception($"Cannot handle line:  {line}");
                }

                fontFile = handler.Handle(line, fontFile);
            }

            return fontFile;
        }
    }

    public abstract class Handler
    {
        public abstract FontFile Handle(string line, FontFile fontFile);

        protected Dictionary<string, string> CreateDictionary(string line, string lineType)
        {
            string cleanedString = Regex.Replace(line, @"\s+", " ");

            cleanedString = cleanedString.Remove(0, lineType.Length + 1);
            string[] words = cleanedString.Split(' ');
            var dict = new Dictionary<string, string>();
            foreach (string word in words)
            {
                string[] keyValue = word.Split('=');
                dict.Add(keyValue[0].ToLower(), keyValue[1].Trim('"'));
            }

            return dict;
        }
    }

    public class InfoHandler : Handler
    {
        public override FontFile Handle(string line, FontFile fontFile)
        {
            Dictionary<string, string> dict = CreateDictionary(line, "info");

            var fontInfo = new FontInfo
            {
                Face = dict["face"],
                Size = Convert.ToInt32(dict["size"]),
                Bold = Convert.ToInt32(dict["bold"]),
                Italic = Convert.ToInt32(dict["italic"]),
                CharSet = dict["charset"],
                Unicode = Convert.ToInt32(dict["unicode"]),
                StretchHeight = Convert.ToInt32(dict["stretchh"]),
                Smooth = Convert.ToInt32(dict["smooth"]),
                SuperSampling = Convert.ToInt32(dict["aa"]),
                Padding = dict["padding"],
                Spacing = dict["spacing"],
                OutLine = Convert.ToInt32(dict["outline"])
            };

            fontFile.Info = fontInfo;

            return fontFile;
        }
    }

    public class CommonHandler : Handler
    {
        public override FontFile Handle(string line, FontFile fontFile)
        {
            Dictionary<string, string> dict = CreateDictionary(line, "common");

            var fontCommon = new FontCommon
            {
                LineHeight = Convert.ToInt32(dict["lineheight"]),
                Base = Convert.ToInt32(dict["base"]),
                ScaleW = Convert.ToInt32(dict["scalew"]),
                ScaleH = Convert.ToInt32(dict["scaleh"]),
                Pages = Convert.ToInt32(dict["pages"]),
                Packed = Convert.ToInt32(dict["packed"]),
                AlphaChannel = Convert.ToInt32(dict["alphachnl"]),
                RedChannel = Convert.ToInt32(dict["redchnl"]),
                GreenChannel = Convert.ToInt32(dict["greenchnl"]),
                BlueChannel = Convert.ToInt32(dict["bluechnl"])
            };

            fontFile.Common = fontCommon;

            return fontFile;
        }
    }

    public class PageHandler : Handler
    {
        public override FontFile Handle(string line, FontFile fontFile)
        {
            Dictionary<string, string> dict = CreateDictionary(line, "page");

            var fontPage = new FontPage
            {
                ID = Convert.ToInt32(dict["id"]),
                File = dict["file"]
            };

            fontFile.Pages.Add(fontPage);

            return fontFile;
        }
    }

    public class CharsHandler : Handler
    {
        public override FontFile Handle(string line, FontFile fontFile)
        {
            return fontFile;
        }
    }

    public class CharHandler : Handler
    {
        public override FontFile Handle(string line, FontFile fontFile)
        {
            Dictionary<string, string> dict = CreateDictionary(line, "char");

            var fontChar = new FontChar
            {
                ID = Convert.ToInt32(dict["id"]),
                X = Convert.ToInt32(dict["x"]),
                Y = Convert.ToInt32(dict["y"]),
                Width = Convert.ToInt32(dict["width"]),
                Height = Convert.ToInt32(dict["height"]),
                XOffset = Convert.ToInt32(dict["xoffset"]),
                YOffset = Convert.ToInt32(dict["yoffset"]),
                XAdvance = Convert.ToInt32(dict["xadvance"]),
                Page = Convert.ToInt32(dict["page"]),
                Channel = Convert.ToInt32(dict["chnl"])
            };

            fontFile.Chars.Add(fontChar);

            return fontFile;
        }
    }
}