using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BitmapFonts
{
    public interface IFont
    {
        Vector2 MeasureString(string text);
        void Draw(string message, Vector2 pos, Color color, float scale, SpriteBatch spriteBatch);
    }
}