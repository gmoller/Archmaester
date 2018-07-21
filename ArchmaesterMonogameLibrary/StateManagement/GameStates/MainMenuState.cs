using System;
using System.Collections.Generic;
using BitmapFonts;
using GuiControls;
using Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Textures;

namespace ArchmaesterMonogameLibrary.StateManagement.GameStates
{
    public class MainMenuState : IGameState
    {
        private readonly IFont _titleFont;
        private readonly string _menuTitle;
        private readonly List<ButtonControl> _menuItems;

        public MainMenuState()
        {
            _menuTitle = "Archmaester";

            _titleFont = AssetsRepository.Instance.GetFont("MenuSpriteFont");
            IFont menuItemFont = AssetsRepository.Instance.GetFont("MenuSpriteFont");
            ITexture2D buttonTextureTopLeft = AssetsRepository.Instance.GetTexture("lite_translucent-border-topleft");
            ITexture2D buttonTextureTop = AssetsRepository.Instance.GetTexture("lite_translucent-border-top");
            ITexture2D buttonTextureTopRight = AssetsRepository.Instance.GetTexture("lite_translucent-border-topright");
            ITexture2D buttonTextureLeft = AssetsRepository.Instance.GetTexture("lite_translucent-border-left");
            ITexture2D buttonTextureRight = AssetsRepository.Instance.GetTexture("lite_translucent-border-right");
            ITexture2D buttonTextureBackground = AssetsRepository.Instance.GetTexture("lite_translucent-background");
            ITexture2D buttonTextureBottomLeft = AssetsRepository.Instance.GetTexture("lite_translucent-border-botleft");
            ITexture2D buttonTextureBottom = AssetsRepository.Instance.GetTexture("lite_translucent-border_bottom");
            ITexture2D buttonTextureBottomRight = AssetsRepository.Instance.GetTexture("lite_translucent-border-botright");
            ITexture2D[] translucentTextures = { buttonTextureTopLeft, buttonTextureTop, buttonTextureTopRight, buttonTextureLeft, buttonTextureBackground, buttonTextureRight, buttonTextureBottomLeft, buttonTextureBottom, buttonTextureBottomRight };

            ITexture2D buttonTextureTopLeft2 = AssetsRepository.Instance.GetTexture("lite_opaque-border-topleft");
            ITexture2D buttonTextureTop2 = AssetsRepository.Instance.GetTexture("lite_opaque-border-top");
            ITexture2D buttonTextureTopRight2 = AssetsRepository.Instance.GetTexture("lite_opaque-border-topright");
            ITexture2D buttonTextureLeft2 = AssetsRepository.Instance.GetTexture("lite_opaque-border-left");
            ITexture2D buttonTextureRight2 = AssetsRepository.Instance.GetTexture("lite_opaque-border-right");
            ITexture2D buttonTextureBackground2 = AssetsRepository.Instance.GetTexture("lite_opaque-background");
            ITexture2D buttonTextureBottomLeft2 = AssetsRepository.Instance.GetTexture("lite_opaque-border-botleft");
            ITexture2D buttonTextureBottom2 = AssetsRepository.Instance.GetTexture("lite_opaque-border_bottom");
            ITexture2D buttonTextureBottomRight2 = AssetsRepository.Instance.GetTexture("lite_opaque-border-botright");
            ITexture2D[] opaqueTextures = { buttonTextureTopLeft2, buttonTextureTop2, buttonTextureTopRight2, buttonTextureLeft2, buttonTextureBackground2, buttonTextureRight2, buttonTextureBottomLeft2, buttonTextureBottom2, buttonTextureBottomRight2 };

            Viewport viewport = StateManager.Instance.GraphicsDevice.Viewport;
            float x = viewport.Width / 2.0f;
            float y = viewport.Height - 350.0f;

            var continueButton = new ButtonControl(menuItemFont, new Vector2(x, y), 200, 70, "Continue", opaqueTextures);
            continueButton.Click += continueButton_Click;
            y += continueButton.Height;
            var loadGameButton = new ButtonControl(menuItemFont, new Vector2(x, y), 200, 70, "Load Game", opaqueTextures);
            loadGameButton.Click += continueButton_Click;
            y += loadGameButton.Height;
            var newGameButton = new ButtonControl(menuItemFont, new Vector2(x, y), 200, 70, "New Game", opaqueTextures);
            newGameButton.Click += continueButton_Click;
            y += newGameButton.Height;
            var hallOfFameButton = new ButtonControl(menuItemFont, new Vector2(x, y), 200, 70, "Hall Of Fame", opaqueTextures);
            hallOfFameButton.Click += continueButton_Click;
            y += hallOfFameButton.Height;
            var quitButton = new ButtonControl(menuItemFont, new Vector2(x, y), 200, 70, "Quit", opaqueTextures);
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
            float titleScale = 1.25f;

            Vector2 titleOrigin = _titleFont.MeasureString(_menuTitle, titleScale) / 2.0f;
            spriteBatch.DrawString(_titleFont, _menuTitle, titlePosition, Color.Red, 0.0f, titleOrigin, titleScale, SpriteEffects.None, 0.0f);
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