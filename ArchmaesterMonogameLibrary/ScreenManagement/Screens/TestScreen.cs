using System;
using BitmapFonts;
using Common;
using Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArchmaesterMonogameLibrary.ScreenManagement.Screens
{
    public class TestScreen : GameScreen
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public TestScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(5.0);
            TransitionOffTime = TimeSpan.FromSeconds(5.0);
        }

        #region Update and Draw

        /// <summary>
        /// Updates the background screen. Unlike most screens, this should not
        /// transition off even if it has been covered by another screen: it is
        /// supposed to be covered, after all! This overload forces the
        /// coveredByOtherScreen parameter to false in order to stop the base
        /// Update method wanting to transition off.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
        }

        /// <summary>
        /// Draws the background screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            IFont fontTime = AssetsRepository.Instance.GetFont("TimeFont");
            IFont fontTest = AssetsRepository.Instance.GetFont("TestFont");

            spriteBatch.DrawString(fontTime, DateTime.Now.ToString("HH mm"), new Vector2(10, 10));
            spriteBatch.DrawString(fontTest, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", new Vector2(0, 50));
            spriteBatch.DrawString(fontTest, "abcdefghijklmnopqrstuvwxyz", new Vector2(0, 100));
            spriteBatch.DrawString(fontTest, "0123456789.,;:?!-&/+%$\"", new Vector2(0, 150));
            spriteBatch.DrawString(fontTest, "In a hole in the ground lived a hobbit.", new Vector2(0, 200));

            spriteBatch.DrawString(fontTest, "Hey diddle diddle.", new Vector2(0, 250), Color.Red * TransitionAlpha);
            spriteBatch.DrawString(fontTest, "The cat and the fiddle.", new Vector2(0, 300), Color.Red * TransitionAlpha, 0.5f);
            spriteBatch.DrawString(fontTest, "The cow jumped over the moon.", new Vector2(0, 350), 0.5f);

            spriteBatch.End();
        }

        #endregion
    }
}