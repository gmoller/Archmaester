using GameState;
using Input;
using Microsoft.Xna.Framework;

namespace ArchmaesterMonogameLibrary.GameStates
{
    public class ExitState : IGameState
    {
        public string Name => "Exit";
        public Game Game { get; }

        public ExitState(Game game)
        {
            Game = game;
        }

        public void Update(InputState input, GameTime gameTime)
        {
            Game.Exit();
        }

        public void Draw(GameTime gameTime)
        {
        }
    }
}