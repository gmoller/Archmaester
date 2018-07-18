using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BitmapFonts
{
    public class FontRenderer
    {
        private readonly FontFile _fontFile;
        private readonly Dictionary<char, FontChar> _characterMap;
        private readonly Texture2D _texture;

        public FontRenderer(FontFile fontFile, Texture2D fontTexture)
        {
            _fontFile = fontFile;
            _texture = fontTexture;
            _characterMap = new Dictionary<char, FontChar>();

            foreach (FontChar fontCharacter in fontFile.Chars)
            {
                var c = (char)fontCharacter.ID;
                _characterMap.Add(c, fontCharacter);
            }
        }

        public void DrawText(SpriteBatch spriteBatch, int x, int y, string text, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layerDepth)
        {
            int dx = x;
            int dy = y;
            foreach (char c in text)
            {
                if (c == '\n')
                {
                    dx = x;
                    dy += _fontFile.Common.LineHeight;
                }
                else
                {
                    FontChar fc;
                    if (_characterMap.TryGetValue(c, out fc))
                    {
                        var sourceRectangle = new Rectangle(fc.X, fc.Y, fc.Width, fc.Height);
                        var position = new Vector2(dx + fc.XOffset, dy + fc.YOffset);

                        spriteBatch.Draw(_texture, position, sourceRectangle, color, rotation, origin, scale,
                            spriteEffects, layerDepth);

                        dx += (int) (fc.XAdvance * scale);
                    }
                }
            }
        }
    }
}