using System;
using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Input;
using Interfaces;
using Textures;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace GuiControls
{
    public class Button
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

        private readonly Label _label;

        private readonly Vector2 _center;
        private Size _size;

        private ControlState _controlState;
        private float _clickedCountdown;

        public event EventHandler Click;

        public Rectangle Area => new Rectangle((int)(_center.X - _size.Width / 2.0f), (int)(_center.Y - _size.Height / 2.0f), (int)_size.Width, (int)_size.Height);
        public int Width => (int)_size.Width;
        public int Height => (int)_size.Height;
        public float Alpha { get; set; }

        private Button(IFont font, Vector2 center, Size size, string text, ITexture2D[] textures, ContentManager content)
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
            _size = size;

            _label = Label.Create(font, center, text, Color.Yellow, 1.0f);
        }

        public static Button Create(IFont font, Vector2 center, Size size, string text, ITexture2D[] textures, ContentManager content)
        {
            var control = new Button(font, center, size, text, textures, content);

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

            float scale = _controlState == ControlState.MouseOver ? 1.1f : 1.0f;
            _label.Scale = scale;
            _label.Alpha = Alpha;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureTopLeft, new Vector2(Area.Left, Area.Top), Color.White * Alpha, 1.0f);
            spriteBatch.Draw(_textureTop, new Rectangle(Area.Left + _textureTopLeft.Width, Area.Top, Area.Width - _textureTopLeft.Width - _textureTopRight.Width, _textureTop.Height), Color.White * Alpha);
            spriteBatch.Draw(_textureTopRight, new Vector2(Area.Right - _textureTopRight.Width, Area.Top), Color.White * Alpha, 1.0f);

            spriteBatch.Draw(_textureLeft, new Rectangle(Area.Left, Area.Top + _textureTopLeft.Height, _textureTopLeft.Width, Area.Height - _textureTopLeft.Height - _textureBottomLeft.Height), Color.White * Alpha);
            spriteBatch.Draw(_textureBackground, new Rectangle(Area.Left + _textureLeft.Width, Area.Top + _textureTop.Height, Area.Width - _textureLeft.Width - _textureRight.Width, Area.Height - _textureTop.Height - _textureBottom.Height), Color.White * Alpha);
            spriteBatch.Draw(_textureRight, new Rectangle(Area.Right - _textureRight.Width, Area.Top + _textureTopRight.Height, _textureTopRight.Width, Area.Height - _textureTopRight.Height - _textureBottomRight.Height), Color.White * Alpha);

            spriteBatch.Draw(_textureBottomLeft, new Vector2(Area.Left, Area.Bottom - _textureBottomLeft.Height), Color.White * Alpha, 1.0f);
            spriteBatch.Draw(_textureBottom, new Rectangle(Area.Left + _textureBottomLeft.Width, Area.Bottom - _textureBottom.Height, Area.Width - _textureBottomLeft.Width - _textureBottomRight.Width, _textureBottom.Height), Color.White * Alpha);
            spriteBatch.Draw(_textureBottomRight, new Vector2(Area.Right - _textureBottomRight.Width, Area.Bottom - _textureBottomRight.Height), Color.White * Alpha, 1.0f);

            spriteBatch.Draw(_label);
        }

        private void OnClick(EventArgs e)
        {
            //ISoundEffect buttonClickSound = AssetsRepository.Instance.GetSound("ButtonClick");
            //buttonClickSound.Play();
            string sound = AssetsRepository.Instance.GetSound("ButtonClick");
            SoundEffect effect = _content.Load<SoundEffect>(sound);
            effect.Play();

            _clickedCountdown = 0.25f; // in seconds
            _size.Width -= 5;
            _size.Height -= 5;
            _controlState = ControlState.Clicked;
            Click?.Invoke(this, e);
        }

        private void OnClickComplete()
        {
            _clickedCountdown = 0.0f;
            _size.Width += 5;
            _size.Height += 5;
            _controlState = ControlState.None;
        }
    }
}