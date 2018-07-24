using Input;
using Microsoft.Xna.Framework;

namespace GameState
{
    public interface IGameState
    {
        string Name { get; }
        Game Game { get; }
        float TransitionOnTime {get; }
        float TransitionPosition { get; set; }
        bool ShowMousePointer { get; set; }

        void Initialize();
        void Update(InputState input, GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}