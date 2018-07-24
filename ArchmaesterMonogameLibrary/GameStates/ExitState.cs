using Input;
using Microsoft.Xna.Framework;

namespace ArchmaesterMonogameLibrary.GameStates
{
    public class ExitState : GameState
    {
        public ExitState(Game game) : base("Exit", 1.0f, false, game)
        {
        }

        public override void Initialize()
        {
        }

        public override void Update(InputState input, GameTime gameTime)
        {
            Game.Exit();
        }

        public override void Draw(GameTime gameTime)
        {
        }
    }
}