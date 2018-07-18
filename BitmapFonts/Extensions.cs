using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BitmapFonts
{
    public static class SpriteBatchExtensions
    {
        public static void DrawString(this SpriteBatch spriteBatch, IFont font, string text, Vector2 position)
        {
            font.Draw(text, position, Color.White, 1.0f, spriteBatch);
        }

        public static void DrawString(this SpriteBatch spriteBatch, IFont font, string text, Vector2 position, Color color)
        {
            font.Draw(text, position, color, 1.0f, spriteBatch);
        }

        public static void DrawString(this SpriteBatch spriteBatch, IFont font, string text, Vector2 position, float scale)
        {
            font.Draw(text, position, Color.White, scale, spriteBatch);
        }

        public static void DrawString(this SpriteBatch spriteBatch, IFont font, string text, Vector2 position, Color color, float scale)
        {
            font.Draw(text, position, color, scale, spriteBatch);
        }
    }
}