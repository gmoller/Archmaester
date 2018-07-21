using BitmapFonts;
using Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Primitives2D;

namespace GuiControls
{
    public class LabelControl
    {
        private readonly IFont _font;
        private readonly Vector2 _center;
        private readonly Vector2 _size;
        private readonly string _text;
        private readonly Color _textColor;
        private readonly float _scale;

        public Rectangle Area => new Rectangle((int)(_center.X - Width / 2.0f), (int)(_center.Y - Height / 2.0f), Width, Height);
        public int Width => (int)(_size.X * _scale);
        public int Height => (int)(_size.Y * _scale);

        private LabelControl(IFont font, Vector2 center, string text, Color textColor, float scale)
        {
            _font = font;
            _center = center;
            _text = text;
            _textColor = textColor;
            _scale = scale;

            // autosize
            _size = font.MeasureString(text, 1.0f);
        }

        public static LabelControl Create(IFont font, Vector2 center, string text, Color textColor, float scale)
        {
            var control = new LabelControl(font, center, text, textColor, scale);

            return control;
        }

        public void Update(InputState input, GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = _size / 2.0f;
            spriteBatch.DrawString(_font, _text, _center, _textColor, 0.0f, origin, _scale, SpriteEffects.None, 0.0f);

            //spriteBatch.DrawRectangle(Area, Color.Red);
        }
    }
}