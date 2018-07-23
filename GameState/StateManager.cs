using System;
using System.Collections.Generic;
using Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameState
{
    public sealed class StateManager
    {
        private static readonly Lazy<StateManager> Lazy = new Lazy<StateManager>(() => new StateManager());

        private readonly Dictionary<string, IGameState> _states;
        private IGameState _currentState;
        private string _stateToChangeTo;
        private readonly InputState _input;

        public SpriteBatch SpriteBatch { get; set; }

        public static StateManager Instance => Lazy.Value;

        private StateManager()
        {
            _states = new Dictionary<string, IGameState>();
            _input = new InputState();
            _stateToChangeTo = string.Empty;
        }

        public void AddState(IGameState state)
        {
            _states.Add(state.Name, state);

            if (_states.Count == 1)
            {
                SetState(state.Name);
            }
        }

        public void SignalStateChange(string name)
        {
            _stateToChangeTo = name;
        }

        public void Update(GameTime gameTime)
        {
            // fade in for x seconds
            _currentState.TransitionPosition = CalculateTransitionPosition(_currentState.TransitionPosition, _currentState.TransitionOnTime, (float) gameTime.ElapsedGameTime.TotalSeconds);

            _input.Update();
            _currentState.Update(_input, gameTime);

            if (!string.IsNullOrEmpty(_stateToChangeTo))
            {
                ChangeState(_currentState.Name, _stateToChangeTo);
                _stateToChangeTo = string.Empty;
            }
        }

        public void Draw(GameTime gameTime)
        {
            _currentState.Draw(gameTime);
        }

        private void ChangeState(string currentState, string requestedState)
        {
            SetState(requestedState);
        }

        private void SetState(string name)
        {
            _currentState = _states[name];
            _currentState.TransitionPosition = 0.0f;
        }

        private float CalculateTransitionPosition(float currentTransitionPosition, float transitionOnTime, float elapsedTimeInSeconds)
        {
            float transitionPosition = currentTransitionPosition;
            if (currentTransitionPosition < 1.0f)
            {
                transitionPosition += elapsedTimeInSeconds / transitionOnTime;
            }

            return transitionPosition;
        }
    }
}