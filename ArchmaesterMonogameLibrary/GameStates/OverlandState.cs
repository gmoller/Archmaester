using GameLogic;
using GameMap;
using Input;
using Microsoft.Xna.Framework;

namespace ArchmaesterMonogameLibrary.GameStates
{
    public class OverlandState : GameState
    {
        private readonly GameMapView _mapWindow;

        public OverlandState(Game game) : base("Overland", 1.0f, game)
        {
            // setup map/board
            int[,] terrain = MapGenerator.Generate(200, 160);
            GameBoard testMap = GameBoard.Create(1, terrain, true);
            Globals.Instance.GameWorld.SetGameBoard(testMap);

            // setup players
            var player = new Player();
            player.StartTurn();
            Globals.Instance.GameWorld.SetPlayer(player);

            _mapWindow = new GameMapView(Globals.Instance.GameWorld, new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height));
        }

        public override void Update(InputState input, GameTime gameTime)
        {
            if (input.IsRightMouseButtonPressed())
            {
                _mapWindow.CenterOnViewPosition(input.CurrentMouseState.Position);
            }

            if (input.IsLeftMouseButtonDown())
            {
                _mapWindow.CenterOnViewPosition(input.CurrentMouseState.Position);
                //_mapWindow.MoveMap() // in the direction of a vector from the center of the screen to where the user clicked
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // draw board
            _mapWindow.Draw(SpriteBatch);

            // draw settlememts
            // draw units

            // draw hudoverlay
        }
    }
}