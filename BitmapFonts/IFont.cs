using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BitmapFonts
{
    public interface IFont
    {
        int LineSpacing { get; }
        Vector2 MeasureString(string text, float scale);
        void Draw(string message, Vector2 pos, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layerDepth, SpriteBatch spriteBatch);
    }
}