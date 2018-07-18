using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArchmaesterMonogameLibrary
{
    public class Cursor
    {
        private SpriteBatch _spriteBatch;

        private Texture2D _cursorTex;
        private Vector2 _cursorPos;


        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _cursorTex = content.Load<Texture2D>(@"Images\cursor");
        }

        public void Update(GameTime gameTime)
        {
            _cursorPos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_cursorTex, _cursorPos, Color.White);
            _spriteBatch.End();
        }
    }
}