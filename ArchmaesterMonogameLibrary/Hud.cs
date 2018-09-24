﻿using GameLogic;
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
            var font = AssetsRepository.Instance.GetFont("TestFont");
            var labelCenterPostion = new Vector2(50.0f, _game.GraphicsDevice.Viewport.Height - 50.0f);
            _turnNumberLabel = Label.Create(font, labelCenterPostion, "Turn: ", Color.Yellow, 1.0f);
            _turnNumberLabel.Alpha = 1.0f;

            var buttonSize = new Size(220, 50);
            var buttonCenterPostion = new Vector2(_game.GraphicsDevice.Viewport.Width - buttonSize.Width/2.0f - 10, _game.GraphicsDevice.Viewport.Height - buttonSize.Height/2.0f - 10);
            ITexture2D textureAtlas = AssetsRepository.Instance.GetTexture("lite_opaque");
            _nextTurnButton = Button.Create(font, buttonCenterPostion, buttonSize, "Next Turn", textureAtlas, _game.Content);
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