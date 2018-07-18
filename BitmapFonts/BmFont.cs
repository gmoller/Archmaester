using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BitmapFonts
{
    public class BmFont : IFont
    {
        private readonly FontFile _fontFile;
        private readonly Dictionary<char, FontChar> _characterMap;
        private readonly FontRenderer _fontRenderer;

        public int LineSpacing => _fontFile.Common.LineHeight;

        public BmFont(string fontTexture, string png, ContentManager content)
        {
            string fontFilePath = Path.Combine(content.RootDirectory, fontTexture);
            FontFile fontFile = FontLoader.Load(fontFilePath);
            _fontFile = fontFile;
            Texture2D fontTexture2D = content.Load<Texture2D>(png);
            _fontRenderer = new FontRenderer(fontFile, fontTexture2D);

            _characterMap = new Dictionary<char, FontChar>();

            foreach (FontChar fontCharacter in fontFile.Chars)
            {
                var c = (char)fontCharacter.ID;
                _characterMap.Add(c, fontCharacter);
            }
        }

        public void Draw(string message, Vector2 pos, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layerDepth, SpriteBatch spriteBatch)
        {
            _fontRenderer.DrawText(spriteBatch, (int)pos.X, (int)pos.Y, message, color, rotation, origin, scale, spriteEffects, layerDepth);
        }

        public Vector2 MeasureString(string text, float scale)
        {
            if (string.IsNullOrEmpty(text))
                return Vector2.Zero;

            var stringRectangle = GetStringRectangle(text, scale);
            return new Vector2(stringRectangle.Width, stringRectangle.Height);
        }

        private Rectangle GetStringRectangle(string text, float scale)
        {
            int dx = 0;
            int dy = 0;
            int maxX = 0;
            foreach (char c in text)
            {
                if (c == '\n')
                {
                    if (dx > maxX) maxX = dx;
                    dx = 0;
                    dy += _fontFile.Common.LineHeight;
                }
                else
                {
                    FontChar fc;
                    if (_characterMap.TryGetValue(c, out fc))
                    {
                        dx += (int) (fc.XAdvance * scale);
                    }
                }
            }

            if (dx > maxX) maxX = dx;

            var rectangle = new Rectangle(0, 0, maxX, dy + _fontFile.Common.LineHeight);

            return rectangle;
        }
    }
}