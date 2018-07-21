using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Textures
{
    public interface ITexture2D
    {
        int Width { get; }
        int Height { get; }
        void Draw(Vector2 position, Color color, float scale, SpriteBatch spriteBatch);
        void Draw(Rectangle destinationRectangle, Color color, SpriteBatch spriteBatch);
    }
}