using System;
using BitmapFonts;
using Common;
using GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Archmaester
{
    public class FrameRateCounter
    {
        private int _frameRate;
        private int _frameCounter;
        private TimeSpan _elapsedTime = TimeSpan.Zero;

        public void Update(GameTime gameTime)
        {
            _elapsedTime += gameTime.ElapsedGameTime;

            if (_elapsedTime > TimeSpan.FromSeconds(1.0d))
            {
                _elapsedTime -= TimeSpan.FromSeconds(1.0d);
                _frameRate = _frameCounter;
                _frameCounter = 0;
            }
        }

        public void Draw(GameTime gameTime)
        {
            _frameCounter++;

            string fps = $"fps: {_frameRate}\nmem: {GC.GetTotalMemory(false) / 1024} KB";

            var font = AssetsRepository.Instance.GetFont("TimeFont");

            SpriteBatch spriteBatch = StateManager.Instance.SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.DrawString(font, fps, new Vector2(1, 1), Color.Magenta, 0.5f);
            spriteBatch.DrawString(font, fps, new Vector2(0, 0), Color.White, 0.5f);
            spriteBatch.End();
        }
    }
}