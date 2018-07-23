using GameState;
using Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArchmaesterMonogameLibrary.GameStates
{
    public abstract class GameState : IGameState
    {
        public Game Game { get; }

        public string Name { get; }

        public float TransitionPosition { get; set; } // Ranges from 0 - nothing, to 1 - fully active.

        public float TransitionOnTime { get; } // in seconds

        protected SpriteBatch SpriteBatch => StateManager.Instance.SpriteBatch;

        protected GameState(string name, float transitionOnTime, Game game)
        {
            Name = name;
            Game = game;
            TransitionOnTime = transitionOnTime;
        }

        public abstract void Draw(GameTime gameTime);

        public abstract void Update(InputState input, GameTime gameTime);
    }
}