using ArchmaesterMonogameLibrary.GameStates;
using GameState;
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

        private FrameRateCounter _fps;

        private GraphicsDeviceManager _graphics;

        public Game1()
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
            StateManager.Instance.AddState(new LoadingState(this));
            StateManager.Instance.AddState(new MainMenuState(this));
            StateManager.Instance.AddState(new OverlandState(this));
            StateManager.Instance.AddState(new CityscapeState(this));
            StateManager.Instance.AddState(new BattlescapeState(this));
            StateManager.Instance.AddState(new LoadGameState(this));
            StateManager.Instance.AddState(new NewGameState(this));
            StateManager.Instance.AddState(new HallOfFameState(this));
            StateManager.Instance.AddState(new ExitState(this));

            _fps = new FrameRateCounter();

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
            StateManager.Instance.Update(gameTime);
            _fps.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            StateManager.Instance.Draw(gameTime);
            _fps.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}