using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Input
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

        public bool IsLeftMouseButtonDown()
        {
            return CurrentMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool IsLeftMouseButtonPressed()
        {
            return CurrentMouseState.LeftButton == ButtonState.Pressed &&
                   LastMouseState.LeftButton != ButtonState.Pressed;
        }

        public bool IsLeftMouseButtonPressedInAnArea(Rectangle area)
        {
            return IsLeftMouseButtonPressed() && IsMouseInArea(area);
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

        public bool IsRightMouseButtonDown()
        {
            return CurrentMouseState.RightButton == ButtonState.Pressed;
        }

        public bool IsRightMouseButtonPressed()
        {
            return CurrentMouseState.RightButton == ButtonState.Pressed &&
                   LastMouseState.RightButton != ButtonState.Pressed;
        }

        public bool IsRightMouseButtonPressedInAnArea(Rectangle area)
        {
            return IsRightMouseButtonPressed() && IsMouseInArea(area);
        }

        public bool IsMouseInArea(Rectangle area)
        {
            return area.Contains(CurrentMouseState.Position);
        }

        public bool IsMouseInOneOfAreas(out int index, params Rectangle[] areas)
        {
            index = 0;
            foreach (Rectangle area in areas)
            {
                if (IsMouseInArea(area))
                {
                    return true;
                }
                index++;
            }

            index = -1;

            return false;
        }

        #endregion
    }
}