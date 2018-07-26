using Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BitmapFonts
{
    public class SpriteFontWrapper : IFont
    {
        private readonly SpriteFont _spriteFont;

        public SpriteFontWrapper(string fontName, ContentManager content)
        {
            _spriteFont = content.Load<SpriteFont>(fontName);
        }

        public int LineSpacing => _spriteFont.LineSpacing;

        public void Draw(string message, Vector2 pos, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layerDepth, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_spriteFont, message, pos, color, rotation, origin, scale, spriteEffects, layerDepth);
        }

        public Vector2 MeasureString(string text, float scale)
        {
            return _spriteFont.MeasureString(text) * scale;
        }
    }
}