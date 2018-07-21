﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Textures
{
    public class Texture2DWrapper : ITexture2D
    {
        private readonly ContentManager _content;
        private readonly string _textureName;

        public Texture2DWrapper(string textureName, ContentManager content)
        {
            _content = content;
            _textureName = textureName;
        }

        public int Width
        {
            get
            {
                var texture = _content.Load<Texture2D>(_textureName);
                return texture.Width;
            }
        }

        public int Height
        {
            get
            {
                var texture = _content.Load<Texture2D>(_textureName);
                return texture.Height;
            }
        }

        public void Draw(Vector2 position, Color color, SpriteBatch spriteBatch)
        {
            var texture = _content.Load<Texture2D>(_textureName);
            spriteBatch.Draw(texture, position, color);
        }

        public void Draw(Rectangle destinationRectangle, Color color, SpriteBatch spriteBatch)
        {
            var texture = _content.Load<Texture2D>(_textureName);
            spriteBatch.Draw(texture, destinationRectangle, color);
        }
    }
}