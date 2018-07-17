using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Archmaester
{
    public class BlankScroll
    {
        private SpriteBatch _spriteBatch;

        private Texture2D _texture;

        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _texture = content.Load<Texture2D>(@"Images\blankscroll2");
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, new Rectangle(800-200, 375-250, 400, 500), Color.White);
            _spriteBatch.End();
        }
    }
}