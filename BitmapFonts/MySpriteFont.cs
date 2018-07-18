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

        public int LineSpacing => _spriteFont.LineSpacing;

        public void Draw(string message, Vector2 pos, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layerDepth, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_spriteFont, message, pos, color, rotation, origin, scale, spriteEffects, layerDepth);
        }

        public Vector2 MeasureString(string text, float scale)
        {
            return _spriteFont.MeasureString(text);
        }
    }
}