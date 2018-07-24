using Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Textures
{
    public static class SpriteBatchExtensions
    {
        public static void Draw(this SpriteBatch spriteBatch, ITexture2D texture, Vector2 position, Color color, float scale)
        {
            texture.Draw(position, color, scale, spriteBatch);
        }

        public static void Draw(this SpriteBatch spriteBatch, ITexture2D texture, Rectangle destinationRectangle, Color color)
        {
            texture.Draw(destinationRectangle, color, spriteBatch);
        }

        public static void Draw(this SpriteBatch spriteBatch, ITexture2D texture, Rectangle destinationRectangle, Rectangle sourceRectangle, Color color)
        {
            texture.Draw(destinationRectangle, sourceRectangle, color, spriteBatch);
        }
    }
}