using System;
using BitmapFonts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Primitives2D;
using Textures;
using Input;

namespace GuiControls
{
    public class ButtonControl
    {
        private readonly ITexture2D _textureTopLeft;
        private readonly ITexture2D _textureTop;
        private readonly ITexture2D _textureTopRight;
        private readonly ITexture2D _textureLeft;
        private readonly ITexture2D _textureBackground;
        private readonly ITexture2D _textureRight;
        private readonly ITexture2D _textureBottomLeft;
        private readonly ITexture2D _textureBottom;
        private readonly ITexture2D _textureBottomRight;

        private readonly IFont _font;
        private readonly Vector2 _center;
        private readonly Rectangle _area;
        private readonly string _text;
        private readonly Color _textColor = Color.Yellow;

        public event EventHandler Click;

        public int Width => _area.Width;
        public int Height => _area.Height;

        public ButtonControl(IFont font, Vector2 center, int width, int height, string text, ITexture2D textureTopLeft, ITexture2D textureTop, ITexture2D textureTopRight, ITexture2D textureLeft, ITexture2D textureBackground, ITexture2D textureRight, ITexture2D textureBottomLeft, ITexture2D textureBottom, ITexture2D textureBottomRight)
        {
            _font = font;

            _textureTopLeft = textureTopLeft;
            _textureTop = textureTop;
            _textureTopRight = textureTopRight;
            _textureLeft = textureLeft;
            _textureBackground = textureBackground;
            _textureRight = textureRight;
            _textureBottomLeft = textureBottomLeft;
            _textureBottom = textureBottom;
            _textureBottomRight = textureBottomRight;

            _center = center;
            _area = new Rectangle((int)center.X - width/2, (int)center.Y - height/2, width, height);
            _text = text;
        }

        public ButtonControl(IFont font, Vector2 center, int width, int height, string text, ITexture2D[] textures)
        {
            _font = font;

            _textureTopLeft = textures[0];
            _textureTop = textures[1];
            _textureTopRight = textures[2];
            _textureLeft = textures[3];
            _textureBackground = textures[4];
            _textureRight = textures[5];
            _textureBottomLeft = textures[6];
            _textureBottom = textures[7];
            _textureBottomRight = textures[8];

            _center = center;
            _area = new Rectangle((int)center.X - width / 2, (int)center.Y - height / 2, width, height);
            _text = text;
        }

        public void Update(InputState input, GameTime gameTime)
        {
            // check for input (mouse or keyboard) and if clicked - need to have access to InputState
            if (input.IsLeftMouseButtonPressedInAnArea(_area))
            {
                OnClick(new EventArgs());
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _textureTopLeft.Draw(new Vector2(_area.Left, _area.Top), Color.White, spriteBatch);
            _textureTop.Draw(new Rectangle(_area.Left + _textureTopLeft.Width, _area.Top, _area.Width - _textureTopLeft.Width - _textureTopRight.Width, _textureTop.Height), Color.White, spriteBatch);
            _textureTopRight.Draw(new Vector2(_area.Right - _textureTopRight.Width, _area.Top), Color.White, spriteBatch);

            _textureLeft.Draw(new Rectangle(_area.Left, _area.Top + _textureTopLeft.Height, _textureTopLeft.Width, _area.Height - _textureTopLeft.Height - _textureBottomLeft.Height), Color.White, spriteBatch);
            _textureBackground.Draw(new Rectangle(_area.Left + _textureLeft.Width, _area.Top + _textureTop.Height, _area.Width - _textureLeft.Width - _textureRight.Width, _area.Height - _textureTop.Height - _textureBottom.Height), Color.White, spriteBatch);
            _textureRight.Draw(new Rectangle(_area.Right - _textureRight.Width, _area.Top + _textureTopRight.Height, _textureTopRight.Width, _area.Height - _textureTopRight.Height - _textureBottomRight.Height), Color.White, spriteBatch);

            _textureBottomLeft.Draw(new Vector2(_area.Left, _area.Bottom - _textureBottomLeft.Height), Color.White, spriteBatch);
            _textureBottom.Draw(new Rectangle(_area.Left + _textureBottomLeft.Width, _area.Bottom - _textureBottom.Height, _area.Width - _textureBottomLeft.Width - _textureBottomRight.Width, _textureBottom.Height), Color.White, spriteBatch);
            _textureBottomRight.Draw(new Vector2(_area.Right - _textureBottomRight.Width, _area.Bottom - _textureBottomRight.Height), Color.White, spriteBatch);

            //_texture.Draw(_area, Color.White, spriteBatch);
            //spriteBatch.DrawRectangle(_area, Color.Red);

            Vector2 size = _font.MeasureString(_text, 1.0f);
            spriteBatch.DrawString(_font, _text, _center, _textColor, 0.0f, size / 2.0f, 1.0f, SpriteEffects.None, 0.0f);
        }

        protected virtual void OnClick(EventArgs e)
        {
            Click?.Invoke(this, e);
        }
    }
}