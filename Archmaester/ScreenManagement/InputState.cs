using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Archmaester.ScreenManagement
{
    /// <summary>
    /// Helper for reading input from keyboard. This class 
    /// tracks both the current and previous state of the input devices, and implements 
    /// query methods for high level input actions such as "move up through the menu"
    /// or "pause the game".
    /// </summary>
    public class InputState
    {
        #region Fields

        public KeyboardState CurrentKeyboardState { get; private set; }
        public MouseState CurrentMouseState { get; private set; }

        public KeyboardState LastKeyboardState { get; private set; }
        public MouseState LastMouseState { get; private set; }

        #endregion

        #region Initialization

        /// <summary>
        /// Constructs a new input state.
        /// </summary>
        public InputState()
        {
            CurrentKeyboardState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();

            LastKeyboardState = Keyboard.GetState();
            LastMouseState = Mouse.GetState();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Reads the latest state of the keyboard.
        /// </summary>
        public void Update()
        {
            LastKeyboardState = CurrentKeyboardState;
            LastMouseState = CurrentMouseState;

            CurrentKeyboardState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();
        }

        /// <summary>
        /// Helper for checking if a key was newly pressed during this update. The
        /// controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a keypress
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsNewKeyPress(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key) &&
                   LastKeyboardState.IsKeyUp(key);
        }

        public bool IsLeftMouseButtonPressed()
        {
            return CurrentMouseState.LeftButton == ButtonState.Pressed &&
                   LastMouseState.LeftButton != ButtonState.Pressed;
        }

        public bool IsLeftMouseButtonPressedInAnArea(Rectangle area)
        {
            return IsLeftMouseButtonPressed() && area.Contains(CurrentMouseState.X, CurrentMouseState.Y);
        }

        public bool IsLeftMouseButtonPressedInOneOfAreas(out int index, params Rectangle[] areas)
        {
            index = 0;
            foreach (Rectangle area in areas)
            {
                if (IsLeftMouseButtonPressedInAnArea(area))
                {
                    return true;
                }
                index++;
            }

            index = -1;
            return false;
        }

        public bool IsRightMouseButtonPressed()
        {
            return CurrentMouseState.RightButton == ButtonState.Pressed &&
                   LastMouseState.RightButton != ButtonState.Pressed;
        }

        public bool IsRightMouseButtonPressedInAnArea(Rectangle area)
        {
            return IsRightMouseButtonPressed() && area.Contains(CurrentMouseState.X, CurrentMouseState.Y);
        }

        public bool IsMouseInOneOfAreas(out int index, params Rectangle[] areas)
        {
            index = 0;
            foreach (Rectangle area in areas)
            {
                if (area.Contains(CurrentMouseState.Position))
                {
                    return true;
                }
                index++;
            }

            index = -1;
            return false;
        }

        /// <summary>
        /// Checks for a "menu select" input action.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When the action
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsMenuSelect()
        {
            return IsNewKeyPress(Keys.Space) ||
                   IsNewKeyPress(Keys.Enter);
        }

        /// <summary>
        /// Checks for a "menu cancel" input action.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When the action
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsMenuCancel()
        {
            return IsNewKeyPress(Keys.Escape);
        }

        /// <summary>
        /// Checks for a "menu up" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public bool IsMenuUp()
        {
            return IsNewKeyPress(Keys.Up);
        }

        /// <summary>
        /// Checks for a "menu down" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public bool IsMenuDown()
        {
            return IsNewKeyPress(Keys.Down);
        }

        /// <summary>
        /// Checks for a "pause the game" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public bool IsPauseGame()
        {
            return IsNewKeyPress(Keys.Escape);
        }

        #endregion
    }
}