using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BitmapFonts
{
    public static class SpriteBatchExtensions
    {
        public static void DrawString(this SpriteBatch spriteBatch, IFont font, string text, Vector2 position)
        {
            font.Draw(text, position, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f, spriteBatch);
        }

        public static void DrawString(this SpriteBatch spriteBatch, IFont font, string text, Vector2 position, Color color)
        {
            font.Draw(text, position, color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f, spriteBatch);
        }

        public static void DrawString(this SpriteBatch spriteBatch, IFont font, string text, Vector2 position, float scale)
        {
            font.Draw(text, position, Color.White, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f, spriteBatch);
        }

        public static void DrawString(this SpriteBatch spriteBatch, IFont font, string text, Vector2 position, Color color, float scale)
        {
            font.Draw(text, position, color, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f, spriteBatch);
        }

        public static void DrawString(this SpriteBatch spriteBatch, IFont font, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layerDepth)
        {
            font.Draw(text, position, color, rotation, origin, scale, spriteEffects, layerDepth, spriteBatch);
        }
    }
}