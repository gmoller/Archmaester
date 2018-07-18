using System;
using ArchmaesterMonogameLibrary;
using ArchmaesterMonogameLibrary.ScreenManagement;
using ArchmaesterMonogameLibrary.ScreenManagement.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BitmapFonts;

namespace Archmaester
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;
        private IFont _fontTime;
        private IFont _fontTest;
        private Cursor _cursor;
        private BlankScroll _blankScroll;

        private GraphicsDeviceManager _graphics;
        private ScreenManager _screenManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1600, // 853 - GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width
                PreferredBackBufferHeight = 900 // 480 - GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
            };
            Content.RootDirectory = "Content";

            // Create the screen manager component.
            _screenManager = new ScreenManager(this);

            Components.Add(_screenManager);

            // Activate the first screens.
            _screenManager.AddScreen(new BackgroundScreen());
            _screenManager.AddScreen(new MainMenuScreen());
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //IsMouseVisible = true;

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

            _cursor = new Cursor();
            _cursor.LoadContent(_spriteBatch, Content);
            _blankScroll = new BlankScroll();
            _blankScroll.LoadContent(_spriteBatch, Content);
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
            _cursor.Update(gameTime);

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

            _spriteBatch.DrawString(_fontTime, DateTime.Now.ToString("HH mm"), new Vector2(10, 10));
            _spriteBatch.DrawString(_fontTest, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", new Vector2(0, 50));
            _spriteBatch.DrawString(_fontTest, "abcdefghijklmnopqrstuvwxyz", new Vector2(0, 100));
            _spriteBatch.DrawString(_fontTest, "0123456789.,;:?!-&/+%$\"", new Vector2(0, 150));
            _spriteBatch.DrawString(_fontTest, "In a hole in the ground lived a hobbit.", new Vector2(0, 200));

            _spriteBatch.DrawString(_fontTest, "Hey diddle diddle.", new Vector2(0, 250), Color.Red);
            _spriteBatch.DrawString(_fontTest, "The cat and the fiddle.", new Vector2(0, 300), Color.Red, 0.5f);
            _spriteBatch.DrawString(_fontTest, "The cow jumped over the moon.", new Vector2(0, 350), 0.5f);

            _spriteBatch.End();

            //_blankScroll.Draw(gameTime);
            _cursor.Draw(gameTime);
        }
    }
}