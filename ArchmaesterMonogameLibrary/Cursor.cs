using Common;
using GameState;
using Interfaces;
using Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArchmaesterMonogameLibrary
{
    public class Cursor
    {
        private Vector2 _cursorPos;

        public void Update(GameTime gameTime)
        {
            _cursorPos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }

        public void Draw(GameTime gameTime)
        {
            ITexture2D cursorTexture = AssetsRepository.Instance.GetTexture("cursor");

            SpriteBatch spriteBatch = StateManager.Instance.SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.Draw(cursorTexture, _cursorPos, Color.White, 1.0f);
            spriteBatch.End();
        }
    }
}