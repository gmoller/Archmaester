using BitmapFonts;
using GeneralUtilities;
using Input;
using Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Primitives2D;

namespace GuiControls
{
    public class Label : Control
    {
        private readonly IFont _font;
        private string _text;
        private readonly Color _textColor;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                AutoSize(_text);
            }
        }

        private Label(IFont font, VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment, Vector2 position, string text, Color textColor, float scale) :
            base(verticalAlignment, horizontalAlignment, position)
        {
            _font = font;
            _text = text;
            _textColor = textColor;
            Scale = scale;

            AutoSize(text);
        }

        private void AutoSize(string text)
        {
            Vector2 v = _font.MeasureString(text, 1.0f); // using scale of 1.0f
            Size = new Size((int)v.X, (int)v.Y);
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
            Vector2 position = DetermineTopLeftPosition(VerticalAlignment, HorizontalAlignment, Position, ScaledWidth, ScaledHeight);
            spriteBatch.DrawString(_font, _text, position, _textColor * Alpha, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0.0f);

            //spriteBatch.DrawRectangle(Area, Color.Red);
        }
    }
}