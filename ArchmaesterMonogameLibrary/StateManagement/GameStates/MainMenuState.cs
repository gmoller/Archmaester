using System;
using System.Collections.Generic;
using System.Threading;
using BitmapFonts;
using GuiControls;
using Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Textures;

namespace ArchmaesterMonogameLibrary.StateManagement.GameStates
{
    public class MainMenuState : IGameState
    {
        private readonly LabelControl _title;
        private readonly List<ButtonControl> _menuItems;

        public MainMenuState(ContentManager content)
        {
            var titleFont = AssetsRepository.Instance.GetFont("MenuSpriteFont");
            var titlePostion = new Vector2(StateManager.Instance.GraphicsDevice.Viewport.Width / 2.0f, 80.0f);
            _title = LabelControl.Create(titleFont, titlePostion, "Archmaester", Color.Red, 1.25f);

            IFont menuItemFont = AssetsRepository.Instance.GetFont("MenuSpriteFont");

            string s = "medium_translucent"; // "lite_translucent", "lite_opaque", "medium_translucent", "menu", "opaque", "selection", "selection2", "strong_opaque", "strong_translucent", "thick_opaque", "thin_opaque", "thin_translucent", "translucent54", "translucent65"
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

            float x = StateManager.Instance.GraphicsDevice.Viewport.Width / 2.0f;
            float y = StateManager.Instance.GraphicsDevice.Viewport.Height - 350.0f;

            var continueButton = ButtonControl.Create(menuItemFont, new Vector2(x, y), 200, 70, "Continue", textures, content);
            continueButton.Click += continueButton_Click;
            y += continueButton.Height;
            var loadGameButton = ButtonControl.Create(menuItemFont, new Vector2(x, y), 200, 70, "Load Game", textures, content);
            loadGameButton.Click += continueButton_Click;
            y += loadGameButton.Height;
            var newGameButton = ButtonControl.Create(menuItemFont, new Vector2(x, y), 200, 70, "New Game", textures, content);
            newGameButton.Click += continueButton_Click;
            y += newGameButton.Height;
            var hallOfFameButton = ButtonControl.Create(menuItemFont, new Vector2(x, y), 200, 70, "Hall Of Fame", textures, content);
            hallOfFameButton.Click += continueButton_Click;
            y += hallOfFameButton.Height;
            var quitButton = ButtonControl.Create(menuItemFont, new Vector2(x, y), 200, 70, "Quit", textures, content);
            quitButton.Click += quitButton_Click;

            _menuItems = new List<ButtonControl>
            {
                continueButton,
                loadGameButton,
                newGameButton,
                hallOfFameButton,
                quitButton
            };
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            Thread.Sleep(250); // sleep so click sound can be heard by user
            StateManager.Instance.ExitGame();
        }

        public void Update(InputState input, GameTime gameTime)
        {
            foreach (ButtonControl button in _menuItems)
            {
                button.Update(input, gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.Instance.SpriteBatch;
            Viewport viewport = StateManager.Instance.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            ITexture2D backgroundTexture = AssetsRepository.Instance.GetTexture("background2");

            spriteBatch.Begin();
            backgroundTexture.Draw(fullscreen, Color.White, spriteBatch);
            DrawTitle(new Vector2(viewport.Width / 2.0f, 80.0f), spriteBatch);
            DrawMenuItems(spriteBatch);
            spriteBatch.End();
        }

        private void DrawTitle(Vector2 titlePosition, SpriteBatch spriteBatch)
        {
            _title.Draw(spriteBatch);
        }

        private void DrawMenuItems(SpriteBatch spriteBatch)
        {
            foreach (ButtonControl button in _menuItems)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}