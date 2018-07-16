using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Archmaester.Fonts
{
    public class FontRenderer
    {
        private readonly Dictionary<char, FontChar> _characterMap;
        private readonly Texture2D _texture;

        public FontRenderer(FontFile fontFile, Texture2D fontTexture)
        {
            _texture = fontTexture;
            _characterMap = new Dictionary<char, FontChar>();

            foreach (FontChar fontCharacter in fontFile.Chars)
            {
                var c = (char)fontCharacter.ID;
                _characterMap.Add(c, fontCharacter);
            }
        }

        public void DrawText(SpriteBatch spriteBatch, int x, int y, string text)
        {
            int dx = x;
            int dy = y;
            foreach (char c in text)
            {
                FontChar fc;
                if (_characterMap.TryGetValue(c, out fc))
                {
                    var sourceRectangle = new Rectangle(fc.X, fc.Y, fc.Width, fc.Height);
                    var position = new Vector2(dx + fc.XOffset, dy + fc.YOffset);

                    spriteBatch.Draw(_texture, position, sourceRectangle, Color.White);

                    //var destRectangle = new Rectangle((int)position.X, (int)position.Y, 40, 40);
                    //spriteBatch.Draw(_texture, destRectangle, sourceRectangle, Color.White);

                    dx += fc.XAdvance - 6;
                }
            }
        }
    }
}