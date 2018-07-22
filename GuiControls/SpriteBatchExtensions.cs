using Microsoft.Xna.Framework.Graphics;

namespace GuiControls
{
    public static class SpriteBatchExtensions
    {
        public static void Draw(this SpriteBatch spriteBatch, Label control)
        {
            control.Draw(spriteBatch);
        }

        public static void Draw(this SpriteBatch spriteBatch, Button control)
        {
            control.Draw(spriteBatch);
        }

        public static void Draw(this SpriteBatch spriteBatch, ButtonGroup control)
        {
            control.Draw(spriteBatch);
        }
    }
}