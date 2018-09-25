using GameLogic;
using GuiControls;
using Input;
using Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using GeneralUtilities;
using Textures;

namespace ArchmaesterMonogameLibrary
{
    public class Hud
    {
        private readonly Game _game;
        private object _border;
        private Label _turnNumberLabel;
        private Button _nextTurnButton;

        public Hud(Game game)
        {
            _game = game;
        }

        public void Initialize()
        {
            var font = AssetsRepository.Instance.GetFont("MenuSpriteFont"); // TestFont

            var labelPostion = new Vector2(_game.GraphicsDevice.Viewport.Width - 5.0f, _game.GraphicsDevice.Viewport.Y + 10.0f);
            _turnNumberLabel = Label.Create(font, VerticalAlignment.Top, HorizontalAlignment.Right, labelPostion, "Turn: ", Color.Navy, 0.75f);
            _turnNumberLabel.Alpha = 1.0f;

            ITexture2D textureAtlas = AssetsRepository.Instance.GetTexture("lite_opaque");
            var buttonSize = new Size(220, 50);
            //var buttonPostion = new Vector2(_game.GraphicsDevice.Viewport.Width - buttonSize.Width/2.0f - 10, _game.GraphicsDevice.Viewport.Height - buttonSize.Height/2.0f - 10);
            var buttonPostion = new Vector2(_game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height);
            _nextTurnButton = Button.Create(font, VerticalAlignment.Bottom, HorizontalAlignment.Right, buttonPostion, buttonSize, "Next Turn", Color.Orange, 1.0f, textureAtlas, _game.Content);
            _nextTurnButton.Alpha = 1.0f;
            _nextTurnButton.Click += nextTurnButton_Click;
        }

        public void Update(InputState input, GameTime gameTime)
        {
            _turnNumberLabel.Text = $"Turn: {Globals.Instance.GameWorld.TurnNumber}";
            _turnNumberLabel.Update(input, gameTime);
            _nextTurnButton.Update(input, gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            _turnNumberLabel.Draw(spriteBatch);
            _nextTurnButton.Draw(spriteBatch);

            spriteBatch.End();
        }

        private void nextTurnButton_Click(object sender, EventArgs e)
        {
            Globals.Instance.GameWorld.HumanPlayerTurnEnded = true;
        }

        public bool MouseOver(InputState input)
        {
            return input.IsMouseInArea(_nextTurnButton.Area);
        }
    }
}