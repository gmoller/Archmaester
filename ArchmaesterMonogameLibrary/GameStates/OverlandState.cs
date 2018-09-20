using GameLogic;
using Input;
using Microsoft.Xna.Framework;

namespace ArchmaesterMonogameLibrary.GameStates
{
    public class OverlandState : GameState
    {
        private Hud _hud;
        private GameMapView _gameMapView;

        public OverlandState(Game game) : base("Overland", 1.0f, true, game)
        {
        }

        public override void Initialize()
        {
            // setup map/board
            Globals.Instance.GameWorld.CreateGameBoard(200, 160);

            // setup players
            var player = new Player();
            player.StartTurn();
            Globals.Instance.GameWorld.SetPlayer(player);

            _hud = new Hud(Game);
            _hud.Initialize();
            _gameMapView = new GameMapView(Globals.Instance.GameWorld, new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height), _hud);
        }

        public override void Update(InputState input, GameTime gameTime)
        {
            _hud.Update(input, gameTime);
            _gameMapView.Update(input, gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // draw board
            _gameMapView.Draw(SpriteBatch);

            // draw settlements

            // draw units

            // draw hudoverlay
            _hud.Draw(SpriteBatch);
        }
    }
}