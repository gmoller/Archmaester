using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BitmapFonts
{
    public class SpriteFontWrapper : IFont
    {
        private readonly SpriteFont _spriteFont;

        public SpriteFontWrapper(SpriteFont spriteFont)
        {
            _spriteFont = spriteFont;
        }

        public void Draw(string message, Vector2 pos, Color color, float scale, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_spriteFont, message, pos, color, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f);
        }

        public Vector2 MeasureString(string text)
        {
            return _spriteFont.MeasureString(text);
        }
    }
}