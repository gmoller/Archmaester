using System.IO;
using ArchmaesterMonogameLibrary;
using ArchmaesterMonogameLibrary.GameStates;
using BitmapFonts;
using Common;
using GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Textures;

namespace Archmaester
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game2 : Game
    {
        private SpriteBatch _spriteBatch;

        private Cursor _cursor;
        private FrameRateCounter _fps;

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
            AddFonts();
            AddTextures(@"Images", Content);
            AddSounds();

            StateManager.Instance.AddState(new MainMenuState(this));
            StateManager.Instance.AddState(new OverlandState(this));
            StateManager.Instance.AddState(new CityscapeState(this));
            StateManager.Instance.AddState(new BattlescapeState(this));
            StateManager.Instance.AddState(new ExitState(this));

            _cursor = new Cursor();
            _fps = new FrameRateCounter();

            base.Initialize();
        }

        private void AddFonts()
        {
            AssetsRepository.Instance.AddFont("TimeFont", new BmFont(@"Fonts\Montserrat-32_0", Content));
            AssetsRepository.Instance.AddFont("TestFont", new BmFont(@"Fonts\Font01_30_sheet", Content));
            AssetsRepository.Instance.AddFont("MenuBitmapFont", new BmFont(@"Fonts\Font03_30_sheet", Content));
            AssetsRepository.Instance.AddFont("MenuSpriteFont", new SpriteFontWrapper(@"Fonts\menufont", Content));
            AssetsRepository.Instance.AddFont("GameSpriteFont", new SpriteFontWrapper(@"Fonts\gamefont", Content));
        }

        private void AddTextures(string path, ContentManager content)
        {
            string[] files = Directory.GetFiles(Path.Combine("Content", path), "*.xnb", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                string fileName = Path.ChangeExtension(file, null);
                string textureName = fileName.Remove(0, 8); // remove "Content\"
                string key = Path.GetFileNameWithoutExtension(file);
                AssetsRepository.Instance.AddTexture(key, new Texture2DWrapper(textureName, content));
            }
        }

        private void AddSounds()
        {
            AssetsRepository.Instance.AddSound("ButtonClick", "Sounds\\Button_click");
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
            _fps.Update(gameTime);
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
            GraphicsDevice.Clear(Color.Black);

            StateManager.Instance.Draw(gameTime);
            _cursor.Draw(gameTime);
            _fps.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}