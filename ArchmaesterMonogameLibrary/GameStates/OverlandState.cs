using System;
using GameLogic;
using GameMap;
using Input;
using Microsoft.Xna.Framework;

namespace ArchmaesterMonogameLibrary.GameStates
{
    public class OverlandState : GameState
    {
        private GameMapView _gameMapView;

        public OverlandState(Game game) : base("Overland", 1.0f, true, game)
        {
        }

        public override void Initialize()
        {
            // setup map/board
            int[,] terrain = MapGenerator.Generate(200, 160);
            GameBoard testMap = GameBoard.Create(1, terrain, true);
            Globals.Instance.GameWorld.SetGameBoard(testMap);

            // setup players
            var player = new Player();
            player.StartTurn();
            Globals.Instance.GameWorld.SetPlayer(player);

            _gameMapView = new GameMapView(Globals.Instance.GameWorld, new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height));

            // TODO: move mouse cursor to middle tile to stop annoying auto-scrolling after clicking the start button.
        }

        public override void Update(InputState input, GameTime gameTime)
        {
            if (input.IsRightMouseButtonPressed())
            {
                _gameMapView.CenterOnViewPosition(input.CurrentMouseState.Position);
            }

            if (input.IsLeftMouseButtonDown())
            {
                // determine where mouse pointer is in relation to the center
                Vector2 v = new Vector2(input.CurrentMouseState.Position.X - _gameMapView.ViewCenter.X, input.CurrentMouseState.Position.Y - _gameMapView.ViewCenter.Y);

                CompassDirection2 dir = GetDirection(v);

                switch (dir)
                {
                    case CompassDirection2.North:
                        _gameMapView.CenterOnViewPosition(new Point((int)_gameMapView.ViewCenter.X, (int)_gameMapView.ViewCenter.Y - _gameMapView.CellHeight));
                        break;
                    case CompassDirection2.NorthEast:
                        _gameMapView.CenterOnViewPosition(new Point((int)_gameMapView.ViewCenter.X + _gameMapView.CellWidth, (int)_gameMapView.ViewCenter.Y - _gameMapView.CellHeight));
                        break;
                    case CompassDirection2.East:
                        _gameMapView.CenterOnViewPosition(new Point((int)_gameMapView.ViewCenter.X + _gameMapView.CellWidth, (int)_gameMapView.ViewCenter.Y));
                        break;
                    case CompassDirection2.SouthEast:
                        _gameMapView.CenterOnViewPosition(new Point((int)_gameMapView.ViewCenter.X + _gameMapView.CellWidth, (int)_gameMapView.ViewCenter.Y + _gameMapView.CellHeight));
                        break;
                    case CompassDirection2.South:
                        _gameMapView.CenterOnViewPosition(new Point((int)_gameMapView.ViewCenter.X, (int)_gameMapView.ViewCenter.Y + _gameMapView.CellHeight));
                        break;
                    case CompassDirection2.SouthWest:
                        _gameMapView.CenterOnViewPosition(new Point((int)_gameMapView.ViewCenter.X - _gameMapView.CellWidth, (int)_gameMapView.ViewCenter.Y + _gameMapView.CellHeight));
                        break;
                    case CompassDirection2.West:
                        _gameMapView.CenterOnViewPosition(new Point((int)_gameMapView.ViewCenter.X - _gameMapView.CellWidth, (int)_gameMapView.ViewCenter.Y));
                        break;
                    case CompassDirection2.NorthWest:
                        _gameMapView.CenterOnViewPosition(new Point((int)_gameMapView.ViewCenter.X - _gameMapView.CellWidth, (int)_gameMapView.ViewCenter.Y - _gameMapView.CellHeight));
                        break;
                }
            }
        }

        private CompassDirection2 GetDirection(Vector2 v)
        {
            double angle = Math.Atan2(v.X, v.Y);
            int octant = (int)Math.Round(8 * angle / (2 * Math.PI) + 8) % 8;

            CompassDirection2 dir = (CompassDirection2)octant;

            return dir;
        }

        public override void Draw(GameTime gameTime)
        {
            // draw board
            _gameMapView.Draw(SpriteBatch);

            // draw settlememts
            // draw units

            // draw hudoverlay
        }

        private enum CompassDirection2
        {
            South,
            SouthEast,
            East,
            NorthEast,
            North,
            NorthWest,
            West,
            SouthWest
        }
    }
}