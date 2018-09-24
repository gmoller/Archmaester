using System;
using System.Threading;
using GameState;
using GeneralUtilities;
using GuiControls;
using Input;
using Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Textures;

namespace ArchmaesterMonogameLibrary.GameStates
{
    public class MainMenuState : GameState
    {
        private Label _title;
        private ButtonGroup _menuButtonGroup;

        //private ButtonGroup _testButtonGroup1;
        //private ButtonGroup _testButtonGroup2;
        //private ButtonGroup _testButtonGroup3;
        //private ButtonGroup _testButtonGroup4;
        //private ButtonGroup _testButtonGroup5;
        //private ButtonGroup _testButtonGroup6;
        //private ButtonGroup _testButtonGroup7;
        //private ButtonGroup _testButtonGroup8;

        public MainMenuState(Game game) : base("MainMenu", 0.2f, true, game)
        {
        }

        public override void Initialize()
        {
            var titleFont = AssetsRepository.Instance.GetFont("MenuSpriteFont");
            var titlePostion = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f, 80.0f);
            _title = Label.Create(titleFont, titlePostion, "Archmaester", Color.Red, 2.25f);

            IFont menuItemFont = AssetsRepository.Instance.GetFont("MenuSpriteFont");

            ITexture2D textureAtlas = AssetsRepository.Instance.GetTexture("lite_opaque");

            float x = Game.GraphicsDevice.Viewport.Width / 2.0f;
            float y = Game.GraphicsDevice.Viewport.Height - 350.0f;

            _menuButtonGroup = ButtonGroup.Create(menuItemFont, new Vector2(x, y), new Size(200, 70), new[] { "Continue", "Load Game", "New Game", "Hall Of Fame", "Quit" }, textureAtlas, Game.Content, ButtonGroupDirection.Vertical);
            _menuButtonGroup["Continue"].Click += continueButton_Click;
            _menuButtonGroup["Load Game"].Click += loadGameButton_Click;
            _menuButtonGroup["New Game"].Click += newGameButton_Click;
            _menuButtonGroup["Hall Of Fame"].Click += hallOfFameButton_Click;
            _menuButtonGroup["Quit"].Click += quitButton_Click;

            //_testButtonGroup1 = ButtonGroup.Create(menuItemFont, new Vector2(1000, 100), new Size(100, 80), new[] { "1", "2", "3", "4", "5" }, all["lite_opaque"], Game.Content, ButtonGroupDirection.Horizontal);
            //_testButtonGroup2 = ButtonGroup.Create(menuItemFont, new Vector2(1000, 200), new Size(100, 80), new[] { "1", "2", "3", "4", "5" }, all["menu"], Game.Content, ButtonGroupDirection.Horizontal);
            //_testButtonGroup3 = ButtonGroup.Create(menuItemFont, new Vector2(1000, 300), new Size(100, 80), new[] { "1", "2", "3", "4", "5" }, all["opaque"], Game.Content, ButtonGroupDirection.Horizontal);
            //_testButtonGroup4 = ButtonGroup.Create(menuItemFont, new Vector2(1000, 400), new Size(100, 80), new[] { "1", "2", "3", "4", "5" }, all["selection"], Game.Content, ButtonGroupDirection.Horizontal);
            //_testButtonGroup5 = ButtonGroup.Create(menuItemFont, new Vector2(1000, 500), new Size(100, 80), new[] { "1", "2", "3", "4", "5" }, all["selection2"], Game.Content, ButtonGroupDirection.Horizontal);
            //_testButtonGroup6 = ButtonGroup.Create(menuItemFont, new Vector2(1000, 600), new Size(100, 80), new[] { "1", "2", "3", "4", "5" }, all["strong_opaque"], Game.Content, ButtonGroupDirection.Horizontal);
            //_testButtonGroup7 = ButtonGroup.Create(menuItemFont, new Vector2(1000, 700), new Size(100, 80), new[] { "1", "2", "3", "4", "5" }, all["thick_opaque"], Game.Content, ButtonGroupDirection.Horizontal);
            //_testButtonGroup8 = ButtonGroup.Create(menuItemFont, new Vector2(1000, 800), new Size(100, 80), new[] { "1", "2", "3", "4", "5" }, all["thin_opaque"], Game.Content, ButtonGroupDirection.Horizontal);
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            StateManager.Instance.SignalStateChange("Overland");
        }

        private void loadGameButton_Click(object sender, EventArgs e)
        {
            StateManager.Instance.SignalStateChange("LoadGame");
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            StateManager.Instance.SignalStateChange("NewGame");
        }

        private void hallOfFameButton_Click(object sender, EventArgs e)
        {
            StateManager.Instance.SignalStateChange("HallOfFame");
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            Thread.Sleep(250); // sleep so click sound can be heard by user
            StateManager.Instance.SignalStateChange("Exit");
        }

        public override void Update(InputState input, GameTime gameTime)
        {
            _menuButtonGroup.Update(input, gameTime);
            //_testButtonGroup1.Update(input, gameTime);
            //_testButtonGroup2.Update(input, gameTime);
            //_testButtonGroup3.Update(input, gameTime);
            //_testButtonGroup4.Update(input, gameTime);
            //_testButtonGroup5.Update(input, gameTime);
            //_testButtonGroup6.Update(input, gameTime);
            //_testButtonGroup7.Update(input, gameTime);
            //_testButtonGroup8.Update(input, gameTime);

            _title.Alpha = TransitionPosition;
            _menuButtonGroup.Alpha = TransitionPosition;
            //_testButtonGroup1.Alpha = TransitionPosition;
            //_testButtonGroup2.Alpha = TransitionPosition;
            //_testButtonGroup3.Alpha = TransitionPosition;
            //_testButtonGroup4.Alpha = TransitionPosition;
            //_testButtonGroup5.Alpha = TransitionPosition;
            //_testButtonGroup6.Alpha = TransitionPosition;
            //_testButtonGroup7.Alpha = TransitionPosition;
            //_testButtonGroup8.Alpha = TransitionPosition;
        }

        public override void Draw(GameTime gameTime)
        {
            Viewport viewport = Game.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            ITexture2D backgroundTexture = AssetsRepository.Instance.GetTexture("background2");

            SpriteBatch.Begin();
            SpriteBatch.Draw(backgroundTexture, fullscreen, Color.White * TransitionPosition);
            SpriteBatch.Draw(_title);
            SpriteBatch.Draw(_menuButtonGroup);
            //SpriteBatch.Draw(_testButtonGroup1);
            //SpriteBatch.Draw(_testButtonGroup2);
            //SpriteBatch.Draw(_testButtonGroup3);
            //SpriteBatch.Draw(_testButtonGroup4);
            //SpriteBatch.Draw(_testButtonGroup5);
            //SpriteBatch.Draw(_testButtonGroup6);
            //SpriteBatch.Draw(_testButtonGroup7);
            //SpriteBatch.Draw(_testButtonGroup8);
            SpriteBatch.End();
        }
    }
}