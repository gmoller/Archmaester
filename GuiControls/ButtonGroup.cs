using System.Collections.Generic;
using Common;
using Input;
using Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GuiControls
{
    public enum ButtonGroupDirection
    {
        Horizontal,
        Vertical
    }

    public class ButtonGroup
    {
        private readonly Dictionary<string, Button> _buttons;

        private ButtonGroup(IFont font, Vector2 position, Size buttonSize, string[] texts, ITexture2D[] textures, ContentManager content, ButtonGroupDirection direction)
        {
            _buttons = new Dictionary<string, Button>();

            foreach (string item in texts)
            {
                var button = Button.Create(font, position, buttonSize, item, textures, content);
                _buttons.Add(item, button);

                if (direction == ButtonGroupDirection.Horizontal)
                {
                    position.X += buttonSize.Width;
                }
                else
                {
                    position.Y += buttonSize.Height;
                }
            }
        }

        public float Alpha { get; set; }
        public Button this[string name] => _buttons[name];

        public static ButtonGroup Create(IFont font, Vector2 position, Size buttonSize, string[] texts, ITexture2D[] textures, ContentManager content, ButtonGroupDirection direction)
        {
            var control = new ButtonGroup(font, position, buttonSize, texts, textures, content, direction);

            return control;
        }

        public void Update(InputState input, GameTime gameTime)
        {
            foreach (Button button in _buttons.Values)
            {
                button.Update(input, gameTime);
                button.Alpha = Alpha;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Button button in _buttons.Values)
            {
                spriteBatch.Draw(button);
            }
        }
    }
}