using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BitmapFonts
{
    public class SpriteFontWrapper : IFont
    {
        private readonly ContentManager _content;
        private readonly string _fontName;

        public SpriteFontWrapper(string fontName, ContentManager content)
        {
            _content = content;
            _fontName = fontName;
        }

        public int LineSpacing
        {
            get
            {
                var spriteFont = _content.Load<SpriteFont>(_fontName);
                return spriteFont.LineSpacing;
            }
        }

        public void Draw(string message, Vector2 pos, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layerDepth, SpriteBatch spriteBatch)
        {
            var spriteFont = _content.Load<SpriteFont>(_fontName);

            spriteBatch.DrawString(spriteFont, message, pos, color, rotation, origin, scale, spriteEffects, layerDepth);
        }

        public Vector2 MeasureString(string text, float scale)
        {
            var spriteFont = _content.Load<SpriteFont>(_fontName);

            return spriteFont.MeasureString(text);
        }
    }
}