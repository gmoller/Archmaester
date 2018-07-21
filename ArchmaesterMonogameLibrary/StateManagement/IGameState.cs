using Input;
using Microsoft.Xna.Framework;

namespace ArchmaesterMonogameLibrary.StateManagement
{
    public interface IGameState
    {
        void Update(InputState input, GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}