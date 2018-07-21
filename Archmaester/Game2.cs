using ArchmaesterMonogameLibrary;
using ArchmaesterMonogameLibrary.StateManagement;
using ArchmaesterMonogameLibrary.StateManagement.GameStates;
using BitmapFonts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Archmaester
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game2 : Game
    {
        private SpriteBatch _spriteBatch;

        private Cursor _cursor;

        private GraphicsDeviceManager _graphics;

        public Game2()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1600,
                PreferredBackBufferHeight = 900
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
            AssetsRepository.Instance.AddFont("TimeFont", new BmFont(@"Fonts\Montserrat-32_0", Content));
            AssetsRepository.Instance.AddFont("TestFont", new BmFont(@"Fonts\Font01_30_sheet", Content));
            AssetsRepository.Instance.AddFont("MenuBitmapFont", new BmFont(@"Fonts\Font03_30_sheet", Content));
            AssetsRepository.Instance.AddFont("MenuSpriteFont", new SpriteFontWrapper(@"Fonts\menufont", Content));

            AssetsRepository.Instance.AddTextures(@"Images", Content);

            StateManager.Instance.Game = this;
            StateManager.Instance.GraphicsDevice = GraphicsDevice;

            // Activate the first screens.
            StateManager.Instance.AddState("MainMenu", new MainMenuState(Content));
            //_screenManager.AddScreen(new BackgroundScreen());
            //_screenManager.AddScreen(new TestScreen());
            //_screenManager.AddScreen(new MainMenuScreen());

            StateManager.Instance.SetState("MainMenu");

            _cursor = new Cursor();


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            StateManager.Instance.SpriteBatch = _spriteBatch;

            _cursor.LoadContent(_spriteBatch, Content);
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

            StateManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            StateManager.Instance.Draw(gameTime);
            _cursor.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}