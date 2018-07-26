using System.IO;
using System.Threading.Tasks;
using BitmapFonts;
using Common;
using GameState;
using GuiControls;
using Input;
using Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;
using Textures;

namespace ArchmaesterMonogameLibrary.GameStates
{
    public class LoadingState : GameState
    {
        private Label _loading;
        private bool _isLoading;
        private bool _loadingComplete;

        public LoadingState(Game game) : base("Loading", 0.3f, false, game)
        {
        }

        public override void Initialize()
        {
            AssetsRepository.Instance.AddFont("TestFont", new BmFont(@"Fonts\Font01_30_sheet", Game.Content));
            AssetsRepository.Instance.AddFont("TimeFont", new BmFont(@"Fonts\Montserrat-32_0", Game.Content));

            var font = AssetsRepository.Instance.GetFont("TestFont");
            var postion = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f, Game.GraphicsDevice.Viewport.Height / 2.0f);
            _loading = Label.Create(font, postion, "Loading Assets...", Color.White, 1.0f);

            _isLoading = false;
            _loadingComplete = false;
        }

        public override void Update(InputState input, GameTime gameTime)
        {
            _loading.Alpha = TransitionPosition;

            if (!_isLoading && !_loadingComplete)
            {
                _isLoading = true;
                Task.Factory.StartNew(LoadAssets);
            }

            if (_loadingComplete)
            {
                _isLoading = false;
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
            AssetsRepository.Instance.AddFont("MenuBitmapFont", new BmFont(@"Fonts\Font03_30_sheet", content));
            AssetsRepository.Instance.AddFont("MenuSpriteFont", new SpriteFontWrapper(@"Fonts\menufont", content));
            AssetsRepository.Instance.AddFont("GameSpriteFont", new SpriteFontWrapper(@"Fonts\gamefont", content));
        }

        private void AddTextures(string path, ContentManager content)
        {
            string[] files = Directory.GetFiles(Path.Combine("Content", path), "*.xnb", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                string filename = Path.ChangeExtension(file, null);
                string textureName = filename.Remove(0, 8); // remove "Content\"
                string key = Path.GetFileNameWithoutExtension(file);
                Frame[] frames = GetFrames(filename);
                AssetsRepository.Instance.AddTexture(key, new Texture2DWrapper(textureName, frames, content));
            }
        }

        private Frame[] GetFrames(string filename)
        {
            if (File.Exists($"{filename}.json"))
            {
                string jsonString = File.ReadAllText($"{filename}.json");
                var frames = JsonConvert.DeserializeObject<Frame2[]>(jsonString);

                // convert frames
                Frame[] returnFrames = new Frame[frames.Length];
                int i = 0;
                foreach (Frame2 item in frames)
                {
                    returnFrames[i] = new Frame { Id = item.Id, Name = item.Name, Rectangle = new Rectangle { X = item.X, Y = item.Y, Width = item.Width, Height = item.Height} };
                    i++;
                }

                return returnFrames;
            }

            return null;
        }

        private void AddSounds()
        {
            AssetsRepository.Instance.AddSound("ButtonClick", "Sounds\\Button_click");
        }
    }

    public struct Frame2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}