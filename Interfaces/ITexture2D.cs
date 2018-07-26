using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Interfaces
{
    public interface ITexture2D
    {
        int Width { get; }
        int Height { get; }
        Frame[] Frames { get; }
        void Draw(Vector2 position, Color color, float scale, SpriteBatch spriteBatch);
        void Draw(Vector2 position, Rectangle sourceRectangle, Color color, float scale, SpriteBatch spriteBatch);
        void Draw(Rectangle destinationRectangle, Color color, SpriteBatch spriteBatch);
        void Draw(Rectangle destinationRectangle, Rectangle sourceRectangle, Color color, SpriteBatch spriteBatch);
    }
}