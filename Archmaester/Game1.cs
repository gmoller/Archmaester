using ArchmaesterMonogameLibrary;
using ArchmaesterMonogameLibrary.ScreenManagement;
using ArchmaesterMonogameLibrary.ScreenManagement.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BitmapFonts;
using Common;

namespace Archmaester
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;

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
            AssetsRepository.Instance.AddFont("TimeFont", new BmFont(@"Fonts\Montserrat-32_0", Content));
            AssetsRepository.Instance.AddFont("TestFont", new BmFont(@"Fonts\Font01_30_sheet", Content));
            AssetsRepository.Instance.AddFont("MenuBitmapFont", new BmFont(@"Fonts\Font03_30_sheet", Content));
            AssetsRepository.Instance.AddFont("MenuSpriteFont", new SpriteFontWrapper(@"Fonts\menufont", Content));

            // Create the screen manager component.
            _screenManager = new ScreenManager(this);

            Components.Add(_screenManager);

            // Activate the first screens.
            _screenManager.AddScreen(new BackgroundScreen());
            _screenManager.AddScreen(new TestScreen());
            _screenManager.AddScreen(new MainMenuScreen());

            _cursor = new Cursor();
            _blankScroll = new BlankScroll();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Content.Load<object>(@"Images\gradient");

            //_cursor.LoadContent(_spriteBatch, Content);
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

            //_blankScroll.Draw(gameTime);
            _cursor.Draw(gameTime);
        }
    }
}