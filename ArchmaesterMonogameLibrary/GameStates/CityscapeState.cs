using GameState;
using Input;
using Microsoft.Xna.Framework;

namespace ArchmaesterMonogameLibrary.GameStates
{
    public class CityscapeState : IGameState
    {
        public string Name => "Cityscape";
        public Game Game { get; }

        public CityscapeState(Game game)
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