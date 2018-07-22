using System;
using System.Collections.Generic;
using System.Threading;
using Common;
using GameState;
using GuiControls;
using Input;
using Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Textures;

namespace ArchmaesterMonogameLibrary.GameStates
{
    public class MainMenuState : IGameState
    {
        private readonly Label _title;
        private readonly ButtonGroup _menuButtonGroup;

        private readonly ButtonGroup _testButtonGroup1;
        private readonly ButtonGroup _testButtonGroup2;
        private readonly ButtonGroup _testButtonGroup3;
        private readonly ButtonGroup _testButtonGroup4;
        private readonly ButtonGroup _testButtonGroup5;
        private readonly ButtonGroup _testButtonGroup6;
        private readonly ButtonGroup _testButtonGroup7;
        private readonly ButtonGroup _testButtonGroup8;

        public string Name => "MainMenu";
        public Game Game { get; }

        public MainMenuState(Game game)
        {
            Game = game;

            var titleFont = AssetsRepository.Instance.GetFont("MenuSpriteFont");
            var titlePostion = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f, 80.0f);
            _title = Label.Create(titleFont, titlePostion, "Archmaester", Color.Red, 1.25f);

            IFont menuItemFont = AssetsRepository.Instance.GetFont("MenuSpriteFont");

            var all = GetTextures("lite_translucent", "lite_opaque", "medium_translucent", "menu", "opaque", "selection", "selection2", "strong_opaque", "strong_translucent", "thick_opaque", "thin_opaque", "thin_translucent", "translucent54", "translucent65");
            ITexture2D[] textures = all["lite_opaque"];

            float x = Game.GraphicsDevice.Viewport.Width / 2.0f;
            float y = Game.GraphicsDevice.Viewport.Height - 350.0f;

            _menuButtonGroup = ButtonGroup.Create(menuItemFont, new Vector2(x, y), new Size(200, 70), new[] { "Continue", "Load Game", "New Game", "Hall Of Fame", "Quit" }, textures, game.Content, ButtonGroupDirection.Vertical);
            _menuButtonGroup["Continue"].Click += continueButton_Click;
            _menuButtonGroup["Quit"].Click += quitButton_Click;

            _testButtonGroup1 = ButtonGroup.Create(menuItemFont, new Vector2(1000, 100), new Size(100, 80), new[] { "1", "2", "3", "4", "5" }, all["lite_opaque"], game.Content, ButtonGroupDirection.Horizontal);
            _testButtonGroup2 = ButtonGroup.Create(menuItemFont, new Vector2(1000, 200), new Size(100, 80), new[] { "1", "2", "3", "4", "5" }, all["menu"], game.Content, ButtonGroupDirection.Horizontal);
            _testButtonGroup3 = ButtonGroup.Create(menuItemFont, new Vector2(1000, 300), new Size(100, 80), new[] { "1", "2", "3", "4", "5" }, all["opaque"], game.Content, ButtonGroupDirection.Horizontal);
            _testButtonGroup4 = ButtonGroup.Create(menuItemFont, new Vector2(1000, 400), new Size(100, 80), new[] { "1", "2", "3", "4", "5" }, all["selection"], game.Content, ButtonGroupDirection.Horizontal);
            _testButtonGroup5 = ButtonGroup.Create(menuItemFont, new Vector2(1000, 500), new Size(100, 80), new[] { "1", "2", "3", "4", "5" }, all["selection2"], game.Content, ButtonGroupDirection.Horizontal);
            _testButtonGroup6 = ButtonGroup.Create(menuItemFont, new Vector2(1000, 600), new Size(100, 80), new[] { "1", "2", "3", "4", "5" }, all["strong_opaque"], game.Content, ButtonGroupDirection.Horizontal);
            _testButtonGroup7 = ButtonGroup.Create(menuItemFont, new Vector2(1000, 700), new Size(100, 80), new[] { "1", "2", "3", "4", "5" }, all["thick_opaque"], game.Content, ButtonGroupDirection.Horizontal);
            _testButtonGroup8 = ButtonGroup.Create(menuItemFont, new Vector2(1000, 800), new Size(100, 80), new[] { "1", "2", "3", "4", "5" }, all["thin_opaque"], game.Content, ButtonGroupDirection.Horizontal);
        }

        private Dictionary<string, ITexture2D[]> GetTextures(params string[] s)
        {
            Dictionary<string, ITexture2D[]> dict = new Dictionary<string, ITexture2D[]>();
            foreach (string item in s)
            {
                ITexture2D[] t = GetTextures(item);
                dict.Add(item, t);
            }

            return dict;
        }

        private ITexture2D[] GetTextures(string s)
        {
            ITexture2D buttonTextureTopLeft = AssetsRepository.Instance.GetTexture($"{s}-border-topleft");
            ITexture2D buttonTextureTop = AssetsRepository.Instance.GetTexture($"{s}-border-top");
            ITexture2D buttonTextureTopRight = AssetsRepository.Instance.GetTexture($"{s}-border-topright");
            ITexture2D buttonTextureLeft = AssetsRepository.Instance.GetTexture($"{s}-border-left");
            ITexture2D buttonTextureRight = AssetsRepository.Instance.GetTexture($"{s}-border-right");
            ITexture2D buttonTextureBackground = AssetsRepository.Instance.GetTexture($"{s}-background");
            ITexture2D buttonTextureBottomLeft = AssetsRepository.Instance.GetTexture($"{s}-border-botleft");
            ITexture2D buttonTextureBottom = AssetsRepository.Instance.GetTexture($"{s}-border-bottom");
            ITexture2D buttonTextureBottomRight = AssetsRepository.Instance.GetTexture($"{s}-border-botright");
            ITexture2D[] textures = { buttonTextureTopLeft, buttonTextureTop, buttonTextureTopRight, buttonTextureLeft, buttonTextureBackground, buttonTextureRight, buttonTextureBottomLeft, buttonTextureBottom, buttonTextureBottomRight };

            return textures;
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            StateManager.Instance.SignalStateChange("Overland");
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            Thread.Sleep(250); // sleep so click sound can be heard by user
            StateManager.Instance.SignalStateChange("Exit");
        }

        public void Update(InputState input, GameTime gameTime)
        {
            _menuButtonGroup.Update(input, gameTime);
            _testButtonGroup1.Update(input, gameTime);
            _testButtonGroup2.Update(input, gameTime);
            _testButtonGroup3.Update(input, gameTime);
            _testButtonGroup4.Update(input, gameTime);
            _testButtonGroup5.Update(input, gameTime);
            _testButtonGroup6.Update(input, gameTime);
            _testButtonGroup7.Update(input, gameTime);
            _testButtonGroup8.Update(input, gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.Instance.SpriteBatch;
            Viewport viewport = Game.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            ITexture2D backgroundTexture = AssetsRepository.Instance.GetTexture("background2");

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, fullscreen, Color.White);
            spriteBatch.Draw(_title);
            spriteBatch.Draw(_menuButtonGroup);
            spriteBatch.Draw(_testButtonGroup1);
            spriteBatch.Draw(_testButtonGroup2);
            spriteBatch.Draw(_testButtonGroup3);
            spriteBatch.Draw(_testButtonGroup4);
            spriteBatch.Draw(_testButtonGroup5);
            spriteBatch.Draw(_testButtonGroup6);
            spriteBatch.Draw(_testButtonGroup7);
            spriteBatch.Draw(_testButtonGroup8);
            spriteBatch.End();
        }
    }
}