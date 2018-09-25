using GameLogic;
using GeneralUtilities;
using Input;
using Microsoft.Xna.Framework;

namespace ArchmaesterMonogameLibrary.GameStates
{
    public class OverlandState : GameState
    {
        private Hud _hud;
        private GameMapView _gameMapView;
        private UnitsView _unitsView;

        public OverlandState(Game game) : base("Overland", 1.0f, true, game)
        {
        }

        public override void Initialize()
        {
            // setup players
            var humanPlayer = new PlayerHuman();

            // setup map/board
            Globals.Instance.GameWorld.Intialize(200, 160, humanPlayer, null);

            //humanPlayer.UnitMoved += Player_UnitMoved;
            //humanPlayer.TurnEnded += Player_TurnEnded;
            //humanPlayer.AddSettlement("Margeritaville", Point2.Create(1, 1), Globals.Instance.RaceTypes[0]);
            humanPlayer.AddUnit(4, Point2.Create(2, 2)); // cavalry
            humanPlayer.AddUnit(0, Point2.Create(0, 1)); // spearman
            humanPlayer.StartTurn();

            _hud = new Hud(Game);
            _hud.Initialize();
            _gameMapView = new GameMapView(Globals.Instance.GameWorld, new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height), _hud);
            _unitsView = new UnitsView(Globals.Instance.GameWorld);

            Globals.Instance.GameWorld.StartTurnForHumanPlayer();
        }

        public override void Update(InputState input, GameTime gameTime)
        {
            _hud.Update(input, gameTime);
            _gameMapView.Update(input, gameTime);

            if (Globals.Instance.GameWorld.HumanPlayerTurnEnded)
            {
                Globals.Instance.GameWorld.EndTurnForHumanPlayer();
                Globals.Instance.GameWorld.StartTurnForHumanPlayer();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // draw board
            _gameMapView.Draw(SpriteBatch);

            // draw settlements

            // draw units
            _unitsView.Draw(SpriteBatch);

            // draw hudoverlay
            _hud.Draw(SpriteBatch);
        }
    }
}