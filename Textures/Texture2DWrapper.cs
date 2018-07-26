using Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Textures
{
    public class Texture2DWrapper : ITexture2D
    {
        private readonly Texture2D _texture;

        public Texture2DWrapper(string textureName, Frame[] frames, ContentManager content)
        {
            _texture = content.Load<Texture2D>(textureName);
            Frames = frames ?? new[] { new Frame { Id = 1, Name = "Default", Rectangle = new Rectangle(0, 0, _texture.Width, _texture.Height) } };
        }

        public int Width => _texture.Width;

        public int Height => _texture.Height;

        public Frame[] Frames { get; }

        public void Draw(Vector2 position, Color color, float scale, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, position, null, color, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f);
        }

        public void Draw(Vector2 position, Rectangle sourceRectangle, Color color, float scale, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, position, sourceRectangle, color, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f);
        }

        public void Draw(Rectangle destinationRectangle, Color color, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, destinationRectangle, color);
        }

        public void Draw(Rectangle destinationRectangle, Rectangle sourceRectangle, Color color, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, color);
        }
    }
}