using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Input;
using Interfaces;
using Textures;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using GeneralUtilities;

namespace GuiControls
{
    public class Button : Control
    {
        private readonly ContentManager _content;

        private readonly ITexture2D _textureAtlas;

        private readonly Label _label;

        private ControlState _controlState;
        private float _clickedCountdown;

        public event EventHandler Click;

        private Button(IFont font, VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment, Vector2 position, Size size, string text, Color textColor, float scale, ITexture2D textureAtlas, ContentManager content) :
            base(verticalAlignment, horizontalAlignment, position)
        {
            _content = content;

            _textureAtlas = textureAtlas;
            Size = size;
            Scale = scale;

            _label = Label.Create(font, VerticalAlignment.Middle, HorizontalAlignment.Center, new Vector2(Area.X + Area.Width / 2.0f, Area.Y + Area.Height / 2.0f), text, textColor, scale);
        }

        public static Button Create(IFont font, VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment, Vector2 center, Size size, string text, Color textColor, float scale, ITexture2D textureAtlas, ContentManager content)
        {
            var control = new Button(font, verticalAlignment, horizontalAlignment, center, size, text, textColor, scale, textureAtlas, content);

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
            Color color = Color.White * Alpha;
            Rectangle topLeftRectangle = _textureAtlas.Frames[8].Rectangle;
            Rectangle topRectangle = _textureAtlas.Frames[7].Rectangle;
            Rectangle topRightRectangle = _textureAtlas.Frames[0].Rectangle;
            Rectangle leftRectangle = _textureAtlas.Frames[5].Rectangle;
            Rectangle backgroundRectangle = _textureAtlas.Frames[1].Rectangle;
            Rectangle rightRectangle = _textureAtlas.Frames[6].Rectangle;
            Rectangle bottomLeftRectangle = _textureAtlas.Frames[2].Rectangle;
            Rectangle bottomRectangle = _textureAtlas.Frames[4].Rectangle;
            Rectangle bottomRightRectangle = _textureAtlas.Frames[3].Rectangle;

            spriteBatch.Draw(_textureAtlas, new Vector2(Area.Left, Area.Top), topLeftRectangle, color, 1.0f);
            spriteBatch.Draw(_textureAtlas, new Rectangle(Area.Left + topLeftRectangle.Width, Area.Top, Area.Width - topLeftRectangle.Width - topRightRectangle.Width, topRectangle.Height), topRectangle, color);
            spriteBatch.Draw(_textureAtlas, new Vector2(Area.Right - topRightRectangle.Width, Area.Top), topRightRectangle, color, 1.0f);

            spriteBatch.Draw(_textureAtlas, new Rectangle(Area.Left, Area.Top + topLeftRectangle.Height, topLeftRectangle.Width, Area.Height - topLeftRectangle.Height - bottomLeftRectangle.Height), leftRectangle, color);
            spriteBatch.Draw(_textureAtlas, new Rectangle(Area.Left + leftRectangle.Width, Area.Top + topRectangle.Height, Area.Width - leftRectangle.Width - rightRectangle.Width, Area.Height - topRectangle.Height - bottomRectangle.Height), backgroundRectangle, color);
            spriteBatch.Draw(_textureAtlas, new Rectangle(Area.Right - rightRectangle.Width, Area.Top + topRightRectangle.Height, topRightRectangle.Width, Area.Height - topRightRectangle.Height - bottomRightRectangle.Height), rightRectangle, color);

            spriteBatch.Draw(_textureAtlas, new Vector2(Area.Left, Area.Bottom - bottomLeftRectangle.Height), bottomLeftRectangle, color, 1.0f);
            spriteBatch.Draw(_textureAtlas, new Rectangle(Area.Left + bottomLeftRectangle.Width, Area.Bottom - bottomRectangle.Height, Area.Width - bottomLeftRectangle.Width - bottomRightRectangle.Width, bottomRectangle.Height), bottomRectangle, color);
            spriteBatch.Draw(_textureAtlas, new Vector2(Area.Right - bottomRightRectangle.Width, Area.Bottom - bottomRightRectangle.Height), bottomRightRectangle, color, 1.0f);

            spriteBatch.Draw(_label);
        }

        private void OnClick(EventArgs e)
        {
            //ISoundEffect buttonClickSound = AssetsRepository.Instance.GetSound("ButtonClick");
            //buttonClickSound.Play();
            string sound = AssetsRepository.Instance.GetSound("ButtonClick");
            SoundEffect effect = _content.Load<SoundEffect>(sound);
            effect.Play();

            _clickedCountdown = 0.1f; // in seconds
            Size.Width -= 5;
            Size.Height -= 5;
            _controlState = ControlState.Clicked;
            Click?.Invoke(this, e);
        }

        private void OnClickComplete()
        {
            _clickedCountdown = 0.0f;
            Size.Width += 5;
            Size.Height += 5;
            _controlState = ControlState.None;
        }
    }
}