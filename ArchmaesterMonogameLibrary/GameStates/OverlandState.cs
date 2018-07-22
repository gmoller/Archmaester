using GameState;
using Input;
using Microsoft.Xna.Framework;

namespace ArchmaesterMonogameLibrary.GameStates
{
    public class OverlandState : IGameState
    {
        public string Name => "Overland";
        public Game Game { get; }

        public OverlandState(Game game)
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