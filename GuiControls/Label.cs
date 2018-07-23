using BitmapFonts;
using Common;
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
        private readonly Vector2 _center;
        private readonly Size _size;
        private readonly string _text;
        private readonly Color _textColor;

        public Rectangle Area => new Rectangle((int)(_center.X - Width / 2.0f), (int)(_center.Y - Height / 2.0f), Width, Height);
        public int Width => (int)(_size.Width * Scale);
        public int Height => (int)(_size.Height * Scale);
        public float Scale { get; set; }
        public float Alpha { get; set; }

        private Label(IFont font, Vector2 center, string text, Color textColor, float scale)
        {
            _font = font;
            _center = center;
            _text = text;
            _textColor = textColor;
            Scale = scale;

            // autosize
            Vector2 v = font.MeasureString(text, 1.0f);
            _size = new Size((int)v.X, (int)v.Y);
        }

        public static Label Create(IFont font, Vector2 center, string text, Color textColor, float scale)
        {
            var control = new Label(font, center, text, textColor, scale);

            return control;
        }

        public void Update(InputState input, GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Size size = _size / 2;
            var origin = new Vector2(size.Width, size.Height);
            spriteBatch.DrawString(_font, _text, _center, _textColor * Alpha, 0.0f, origin, Scale, SpriteEffects.None, 0.0f);

            //spriteBatch.DrawRectangle(Area, Color.Red);
        }
    }
}