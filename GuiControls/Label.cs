using System;
using BitmapFonts;
using GeneralUtilities;
using Input;
using Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Primitives2D;

namespace GuiControls
{
    public class Label
    {
        private readonly IFont _font;
        private readonly VerticalAlignment _verticalAlignment;
        private readonly HorizontalAlignment _horizontalAlignment;
        private readonly Vector2 _position;
        private Size _size;
        private string _text;
        private readonly Color _textColor;

        public Rectangle Area => DetermineArea(_verticalAlignment, _horizontalAlignment, _position, ScaledWidth, ScaledHeight);
        public int ScaledWidth => (int)(_size.Width * Scale);
        public int ScaledHeight => (int)(_size.Height * Scale);
        public float Scale { get; set; }
        public float Alpha { get; set; }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                AutoSize(_text);
            }
        }

        private Label(IFont font, VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment, Vector2 position, string text, Color textColor, float scale)
        {
            _font = font;
            _verticalAlignment = verticalAlignment;
            _horizontalAlignment = horizontalAlignment;
            _position = position;
            _text = text;
            _textColor = textColor;
            Scale = scale;

            AutoSize(text);
        }

        private void AutoSize(string text)
        {
            Vector2 v = _font.MeasureString(text, 1.0f); // using scale of 1.0f
            _size = new Size((int)v.X, (int)v.Y);
        }

        public static Label Create(IFont font, VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment, Vector2 position, string text, Color textColor, float scale)
        {
            var control = new Label(font, verticalAlignment, horizontalAlignment, position, text, textColor, scale);

            return control;
        }

        public void Update(InputState input, GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 position = DetermineTopLeftPosition(_verticalAlignment, _horizontalAlignment, _position, ScaledWidth, ScaledHeight);
            spriteBatch.DrawString(_font, _text, position, _textColor * Alpha, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0.0f);

            //spriteBatch.DrawRectangle(Area, Color.Red);
        }

        private Vector2 DetermineTopLeftPosition(VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment, Vector2 position, int scaledWidth, int scaledHeight)
        {
            Vector2 offset;

            if (verticalAlignment == VerticalAlignment.Top && horizontalAlignment == HorizontalAlignment.Left)
            {
                offset = new Vector2(0.0f, 0.0f);
            }
            else if (verticalAlignment == VerticalAlignment.Top && horizontalAlignment == HorizontalAlignment.Center)
            {
                offset = new Vector2(scaledWidth / 2.0f, 0.0f);
            }
            else if (verticalAlignment == VerticalAlignment.Top && horizontalAlignment == HorizontalAlignment.Right)
            {
                offset = new Vector2(scaledWidth, 0.0f);
            }
            else if (verticalAlignment == VerticalAlignment.Middle && horizontalAlignment == HorizontalAlignment.Left)
            {
                offset = new Vector2(0.0f, scaledHeight / 2.0f);
            }
            else if (verticalAlignment == VerticalAlignment.Middle && horizontalAlignment == HorizontalAlignment.Center)
            {
                offset = new Vector2(scaledWidth / 2.0f, scaledHeight / 2.0f);
            }
            else if (verticalAlignment == VerticalAlignment.Middle && horizontalAlignment == HorizontalAlignment.Right)
            {
                offset = new Vector2(scaledWidth, scaledHeight / 2.0f);
            }
            else if (verticalAlignment == VerticalAlignment.Bottom && horizontalAlignment == HorizontalAlignment.Left)
            {
                offset = new Vector2(0.0f, scaledHeight);
            }
            else if (verticalAlignment == VerticalAlignment.Bottom && horizontalAlignment == HorizontalAlignment.Center)
            {
                offset = new Vector2(scaledWidth / 2.0f, scaledHeight);
            }
            else if (verticalAlignment == VerticalAlignment.Bottom && horizontalAlignment == HorizontalAlignment.Right)
            {
                offset = new Vector2(scaledWidth, scaledHeight);
            }
            else
            {
                throw new NotImplementedException($"{verticalAlignment}{horizontalAlignment} alignment has not been implemented yet.");
            }

            Vector2 topLeftPosition = position - offset;

            return topLeftPosition;
        }

        private Rectangle DetermineArea(VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment, Vector2 position, int scaledWidth, int scaledHeight)
        {
            Vector2 topLeftPosition = DetermineTopLeftPosition(verticalAlignment, horizontalAlignment, position, scaledWidth, scaledHeight);

            Rectangle area = new Rectangle((int)topLeftPosition.X, (int)topLeftPosition.Y, scaledWidth, scaledHeight);

            return area;
        }
    }
}