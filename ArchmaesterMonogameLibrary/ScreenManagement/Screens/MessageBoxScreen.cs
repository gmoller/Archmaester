using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BitmapFonts;
using Input;

namespace ArchmaesterMonogameLibrary.ScreenManagement.Screens
{
    /// <summary>
    /// A popup message box screen, used to display "are you sure?"
    /// confirmation messages.
    /// </summary>
    public class MessageBoxScreen : GameScreen
    {
        #region Fields

        private readonly string _message;
        private Texture2D _gradientTexture;

        #endregion

        #region Events

        public event EventHandler<PlayerIndexEventArgs> Accepted;
        public event EventHandler<PlayerIndexEventArgs> Cancelled;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor automatically includes the standard "A=ok, B=cancel"
        /// usage text prompt.
        /// </summary>
        public MessageBoxScreen(string message) : this(message, true) { }

        /// <summary>
        /// Constructor lets the caller specify whether to include the standard
        /// "A=ok, B=cancel" usage text prompt.
        /// </summary>
        public MessageBoxScreen(string message, bool includeUsageText)
        {
            const string usageText = "\nSpace, Enter = ok" +
                                     "\nEsc = cancel";

            if (includeUsageText)
                _message = message + usageText;
            else
                _message = message;

            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.1);
            TransitionOffTime = TimeSpan.FromSeconds(0.1);
        }

        /// <summary>
        /// Loads graphics content for this screen. This uses the shared ContentManager
        /// provided by the Game class, so the content will remain loaded forever.
        /// Whenever a subsequent MessageBoxScreen tries to load this same content,
        /// it will just get back another reference to the already loaded data.
        /// </summary>
        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;

            _gradientTexture = content.Load<Texture2D>(@"Images\gradient");
        }

        #endregion

        #region Handle Input

        /// <summary>
        /// Responds to user input, accepting or cancelling the message box.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            IFont font = ScreenManager.Font;
            Vector2 textSize = GetTextSize(_message, font);
            Vector2 textPosition = GetTextPosition(textSize);
            Rectangle backgroundRectangle = GetBackgroundRectangle(textPosition, textSize);

            if (input.IsMenuSelect() || input.IsLeftMouseButtonPressedInAnArea(backgroundRectangle))
            {
                // Raise the accepted event, then exit the message box.
                Accepted?.Invoke(this, new PlayerIndexEventArgs(0));

                ExitScreen();
            }
            else if (input.IsMenuCancel() || input.IsRightMouseButtonPressedInAnArea(backgroundRectangle))
            {
                // Raise the cancelled event, then exit the message box.
                Cancelled?.Invoke(this, new PlayerIndexEventArgs(0));

                ExitScreen();
            }
        }

        #endregion

        #region Draw

        /// <summary>
        /// Draws the message box.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            IFont font = ScreenManager.Font;

            // Darken down any other screens that were drawn beneath the popup.
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);

            Vector2 textSize = GetTextSize(_message, font);
            Vector2 textPosition = GetTextPosition(textSize);
            Rectangle backgroundRectangle = GetBackgroundRectangle(textPosition, textSize);

            // Fade the popup alpha during transitions.
            Color color = Color.White * TransitionAlpha;

            spriteBatch.Begin();

            // Draw the background rectangle.
            spriteBatch.Draw(_gradientTexture, backgroundRectangle, color);

            // Draw the message box text.
            spriteBatch.DrawString(font, _message, textPosition, color);

            spriteBatch.End();
        }

        private Vector2 GetTextSize(string message, IFont font)
        {
            Vector2 textSize = font.MeasureString(message, 1.0f);

            return textSize;
        }

        private Vector2 GetTextPosition(Vector2 textSize)
        {
            // Center the message text in the viewport.
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            var viewportSize = new Vector2(viewport.Width, viewport.Height);
            Vector2 textPosition = (viewportSize - textSize) / 2;

            return textPosition;
        }

        private Rectangle GetBackgroundRectangle(Vector2 textPosition, Vector2 textSize)
        {
            // The background includes a border somewhat larger than the text itself.
            const int hPad = 32;
            const int vPad = 16;

            var backgroundRectangle = new Rectangle((int)textPosition.X - hPad,
                (int)textPosition.Y - vPad,
                (int)textSize.X + hPad * 2,
                (int)textSize.Y + vPad * 2);

            return backgroundRectangle;
        }

        #endregion
    }
}