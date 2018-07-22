using GameState;
using Input;
using Microsoft.Xna.Framework;

namespace ArchmaesterMonogameLibrary.GameStates
{
    public class BattlescapeState : IGameState
    {
        public string Name => "Battlescape";
        public Game Game { get; }

        public BattlescapeState(Game game)
        {
            Game = game;
        }

        public void Update(InputState input, GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
        }
    }
}