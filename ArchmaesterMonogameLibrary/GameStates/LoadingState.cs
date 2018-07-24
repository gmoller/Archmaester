using System.IO;
using System.Threading.Tasks;
using BitmapFonts;
using Common;
using GameState;
using GuiControls;
using Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Textures;

namespace ArchmaesterMonogameLibrary.GameStates
{
    public class LoadingState : GameState
    {
        private Label _loading;
        private bool _loadingComplete;

        public LoadingState(Game game) : base("Loading", 0.3f, false, game)
        {
        }

        public override void Initialize()
        {
            AssetsRepository.Instance.AddFont("TestFont", new BmFont(@"Fonts\Font01_30_sheet", Game.Content));

            var font = AssetsRepository.Instance.GetFont("TestFont");
            var postion = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f, Game.GraphicsDevice.Viewport.Height / 2.0f);
            _loading = Label.Create(font, postion, "Loading Assets...", Color.White, 1.0f);

            Task.Factory.StartNew(LoadAssets);
        }

        public override void Update(InputState input, GameTime gameTime)
        {
            _loading.Alpha = TransitionPosition;

            if (_loadingComplete)
            {
                StateManager.Instance.SignalStateChange("MainMenu");
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(_loading);
            SpriteBatch.End();
        }

        private void LoadAssets()
        {
            Parallel.Invoke(
                () => AddFonts(Game.Content),
                () => AddTextures(@"Images", Game.Content),
                () => AddSounds());

            _loadingComplete = true;
        }

        private void AddFonts(ContentManager content)
        {
            AssetsRepository.Instance.AddFont("TimeFont", new BmFont(@"Fonts\Montserrat-32_0", Game.Content));
            AssetsRepository.Instance.AddFont("MenuBitmapFont", new BmFont(@"Fonts\Font03_30_sheet", content));
            AssetsRepository.Instance.AddFont("MenuSpriteFont", new SpriteFontWrapper(@"Fonts\menufont", content));
            AssetsRepository.Instance.AddFont("GameSpriteFont", new SpriteFontWrapper(@"Fonts\gamefont", content));
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
    }
}