using System;
using BitmapFonts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Textures;
using Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace GuiControls
{
    public class ButtonControl
    {
        private readonly ContentManager _content;

        private readonly ITexture2D _textureTopLeft;
        private readonly ITexture2D _textureTop;
        private readonly ITexture2D _textureTopRight;
        private readonly ITexture2D _textureLeft;
        private readonly ITexture2D _textureBackground;
        private readonly ITexture2D _textureRight;
        private readonly ITexture2D _textureBottomLeft;
        private readonly ITexture2D _textureBottom;
        private readonly ITexture2D _textureBottomRight;

        private readonly LabelControl _label;

        private readonly Vector2 _center;
        private Vector2 _size;

        private ControlState _controlState;
        private float _clickedCountdown;

        public event EventHandler Click;

        public Rectangle Area => new Rectangle((int)(_center.X - _size.X / 2.0f), (int)(_center.Y - _size.Y / 2.0f), (int)_size.X, (int)_size.Y);
        public int Width => (int)_size.X;
        public int Height => (int)_size.Y;

        private ButtonControl(IFont font, Vector2 center, int width, int height, string text, ITexture2D[] textures, ContentManager content)
        {
            _content = content;

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
            _size = new Vector2(width, height);

            _label = LabelControl.Create(font, center, text, Color.Yellow, 1.0f);
        }

        public static ButtonControl Create(IFont font, Vector2 center, int width, int height, string text, ITexture2D[] textures, ContentManager content)
        {
            var control = new ButtonControl(font, center, width, height, text, textures, content);

            return control;
        }

        public void Update(InputState input, GameTime gameTime)
        {
            if (_clickedCountdown > 0.0f)
            {
                _clickedCountdown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_clickedCountdown <= 0.0f)
                {
                    OnClickComplete();
                }
            }
            else
            {
                _controlState = input.IsMouseInArea(Area) ? ControlState.MouseOver : ControlState.None;

                if (input.IsLeftMouseButtonPressedInAnArea(Area))
                {
                    OnClick(new EventArgs());
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float scale = _controlState == ControlState.MouseOver ? 1.1f : 1.0f;

            _textureTopLeft.Draw(new Vector2(Area.Left, Area.Top), Color.White, 1.0f, spriteBatch);
            _textureTop.Draw(new Rectangle(Area.Left + _textureTopLeft.Width, Area.Top, Area.Width - _textureTopLeft.Width - _textureTopRight.Width, _textureTop.Height), Color.White, spriteBatch);
            _textureTopRight.Draw(new Vector2(Area.Right - _textureTopRight.Width, Area.Top), Color.White, 1.0f, spriteBatch);

            _textureLeft.Draw(new Rectangle(Area.Left, Area.Top + _textureTopLeft.Height, _textureTopLeft.Width, Area.Height - _textureTopLeft.Height - _textureBottomLeft.Height), Color.White, spriteBatch);
            _textureBackground.Draw(new Rectangle(Area.Left + _textureLeft.Width, Area.Top + _textureTop.Height, Area.Width - _textureLeft.Width - _textureRight.Width, Area.Height - _textureTop.Height - _textureBottom.Height), Color.White, spriteBatch);
            _textureRight.Draw(new Rectangle(Area.Right - _textureRight.Width, Area.Top + _textureTopRight.Height, _textureTopRight.Width, Area.Height - _textureTopRight.Height - _textureBottomRight.Height), Color.White, spriteBatch);

            _textureBottomLeft.Draw(new Vector2(Area.Left, Area.Bottom - _textureBottomLeft.Height), Color.White, 1.0f, spriteBatch);
            _textureBottom.Draw(new Rectangle(Area.Left + _textureBottomLeft.Width, Area.Bottom - _textureBottom.Height, Area.Width - _textureBottomLeft.Width - _textureBottomRight.Width, _textureBottom.Height), Color.White, spriteBatch);
            _textureBottomRight.Draw(new Vector2(Area.Right - _textureBottomRight.Width, Area.Bottom - _textureBottomRight.Height), Color.White, 1.0f, spriteBatch);

            //_texture.Draw(Area, Color.White, spriteBatch);

            _label.Scale = scale;
            _label.Draw(spriteBatch);
        }

        private void OnClick(EventArgs e)
        {
            SoundEffect effect = _content.Load<SoundEffect>("Sounds\\Button_click");
            effect.Play();

            _clickedCountdown = 0.25f; // in seconds
            _size.X -= 5;
            _size.Y -= 5;
            _controlState = ControlState.Clicked;
            Click?.Invoke(this, e);
        }

        private void OnClickComplete()
        {
            _clickedCountdown = 0.0f;
            _size.X += 5;
            _size.Y += 5;
            _controlState = ControlState.None;
        }
    }
}