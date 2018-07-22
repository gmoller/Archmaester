using Input;
using Microsoft.Xna.Framework;

namespace GameState
{
    public interface IGameState
    {
        string Name { get; }
        Game Game { get; }
        void Update(InputState input, GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}