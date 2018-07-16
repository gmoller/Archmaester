using System;
using Archmaester.Fonts;
using Archmaester.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Archmaester
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;
        private BmFont _fontTime;
        private BmFont _fontTest;

        private GraphicsDeviceManager _graphics;
        private ScreenManager _screenManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1600, // 853
                PreferredBackBufferHeight = 900 // 480
            };
            Content.RootDirectory = "Content";

            // Create the screen manager component.
            _screenManager = new ScreenManager(this);

            Components.Add(_screenManager);

            // Activate the first screens.
            _screenManager.AddScreen(new BackgroundScreen(), null);
            _screenManager.AddScreen(new MainMenuScreen(), null);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _fontTime = new BmFont(@"Fonts\Montserrat.fnt", @"Fonts\Montserrat-32_0", Content);
            _fontTest = new BmFont(@"Fonts\Font01_30.fnt", @"Fonts\Font01_30_sheet", Content);

            Content.Load<object>(@"Images\gradient");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);

            _spriteBatch.Begin();
            _fontTime.Draw(DateTime.Now.ToString("HH mm"), new Vector2(10, 10), _spriteBatch);
            _fontTest.Draw("ABCDEFGHIJKLMNOPQRSTUVWXYZ", new Vector2(0, 50), _spriteBatch);
            _fontTest.Draw("abcdefghijklmnopqrstuvwxyz", new Vector2(0, 100), _spriteBatch);
            _fontTest.Draw("0123456789.,;:?!-&/+%$\"", new Vector2(0, 150), _spriteBatch);
            _fontTest.Draw("In a hole in the ground lived a hobbit.", new Vector2(0, 200), _spriteBatch);
            _spriteBatch.End();
        }
    }
}