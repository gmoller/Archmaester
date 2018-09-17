using ArchmaesterMonogameLibrary.GameStates;
using GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArchmaesterMonogameLibrary
{
    public class God
    {
        private FrameRateCounter _fps;

        public void Initialize(Game game)
        {
            StateManager.Instance.AddState(new LoadingState(game));
            StateManager.Instance.AddState(new MainMenuState(game));
            StateManager.Instance.AddState(new OverlandState(game));
            StateManager.Instance.AddState(new CityscapeState(game));
            StateManager.Instance.AddState(new BattlescapeState(game));
            StateManager.Instance.AddState(new LoadGameState(game));
            StateManager.Instance.AddState(new NewGameState(game));
            StateManager.Instance.AddState(new HallOfFameState(game));
            StateManager.Instance.AddState(new ExitState(game));

            _fps = new FrameRateCounter();
        }

        public void LoadContent(SpriteBatch spriteBatch)
        {
            StateManager.Instance.SpriteBatch = spriteBatch;
        }

        public void Update(GameTime gameTime)
        {
            StateManager.Instance.Update(gameTime);
            _fps.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            StateManager.Instance.Draw(gameTime);
            _fps.Draw(gameTime);
        }
    }
}