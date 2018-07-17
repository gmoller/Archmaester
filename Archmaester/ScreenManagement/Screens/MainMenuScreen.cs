namespace Archmaester.ScreenManagement.Screens
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    public class MainMenuScreen : MenuScreen
    {
        #region Initialization

        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen() : base("Archmaester")
        {
            // Create our menu entries.
            MenuEntry continueMenuEntry = new MenuEntry("Continue", this);
            MenuEntry loadMenuEntry = new MenuEntry("Load Game", this);
            MenuEntry newMenuEntry = new MenuEntry("New Game", this);
            MenuEntry hallOfFameMenuEntry = new MenuEntry("Hall of Fame", this);
            MenuEntry quitMenuEntry = new MenuEntry("Quit", this);

            // Hook up menu event handlers.
            continueMenuEntry.Selected += PlayGameMenuEntrySelected;
            loadMenuEntry.Selected += OptionsMenuEntrySelected;
            quitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(continueMenuEntry);
            MenuEntries.Add(loadMenuEntry);
            MenuEntries.Add(newMenuEntry);
            MenuEntries.Add(hallOfFameMenuEntry);
            MenuEntries.Add(quitMenuEntry);
        }

        #endregion

        #region Handle Input

        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        private void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, new GameplayScreen());
        }

        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        private void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen());
        }

        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel()
        {
            const string message = "Are you sure you want to exit this sample?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox);
        }

        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        private void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }

        #endregion
    }
}