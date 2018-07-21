using System;
using System.Collections.Generic;
using Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArchmaesterMonogameLibrary.StateManagement
{
    public sealed class StateManager
    {
        private static readonly Lazy<StateManager> Lazy = new Lazy<StateManager>(() => new StateManager());

        private readonly Dictionary<string, IGameState> _states;
        private IGameState _currentState;
        private readonly InputState _input;

        public Game Game { get; set; }
        public GraphicsDevice GraphicsDevice { get; set; }
        public SpriteBatch SpriteBatch { get; set; }

        public static StateManager Instance => Lazy.Value;

        private StateManager()
        {
            _states = new Dictionary<string, IGameState>();
            _input = new InputState();
        }

        public void AddState(string name, IGameState state)
        {
            _states.Add(name, state);
        }

        public void SetState(string name)
        {
            _currentState = _states[name];
        }

        public void Update(GameTime gameTime)
        {
            _input.Update();
            _currentState.Update(_input, gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            _currentState.Draw(gameTime);
        }

        public void ExitGame()
        {
            Game.Exit();
        }
    }
}