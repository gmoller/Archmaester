using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArchmaesterMonogameLibrary.ScreenManagement.Screens
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    public class OptionsMenuScreen : MenuScreen
    {
        #region Fields

        private readonly MenuEntry _ungulateMenuEntry;
        private readonly MenuEntry _languageMenuEntry;
        private readonly MenuEntry _frobnicateMenuEntry;
        private readonly MenuEntry _elfMenuEntry;

        private enum Ungulate
        {
            BactrianCamel,
            Dromedary,
            Llama,
        }

        private readonly string[] _languages = { "C#", "French", "Deoxyribonucleic acid" };

        private Ungulate _currentUngulate = Ungulate.Dromedary;
        private int _currentLanguage;
        private bool _frobnicate = true;
        private int _elf = 23;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen() : base("Options")
        {
            // Create our menu entries.
            _ungulateMenuEntry = new MenuEntry(string.Empty, this);
            _languageMenuEntry = new MenuEntry(string.Empty, this);
            _frobnicateMenuEntry = new MenuEntry(string.Empty, this);
            _elfMenuEntry = new MenuEntry(string.Empty, this);

            SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back", this);

            // Hook up menu event handlers.
            _ungulateMenuEntry.Selected += UngulateMenuEntrySelected;
            _languageMenuEntry.Selected += LanguageMenuEntrySelected;
            _frobnicateMenuEntry.Selected += FrobnicateMenuEntrySelected;
            _elfMenuEntry.Selected += ElfMenuEntrySelected;
            back.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(_ungulateMenuEntry);
            MenuEntries.Add(_languageMenuEntry);
            MenuEntries.Add(_frobnicateMenuEntry);
            MenuEntries.Add(_elfMenuEntry);
            MenuEntries.Add(back);
        }

        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        private void SetMenuEntryText()
        {
            _ungulateMenuEntry.Text = "Preferred ungulate: " + _currentUngulate;
            _languageMenuEntry.Text = "Language: " + _languages[_currentLanguage];
            _frobnicateMenuEntry.Text = "Frobnicate: " + (_frobnicate ? "on" : "off");
            _elfMenuEntry.Text = "elf: " + _elf;
        }

        #endregion

        #region Handle Input

        /// <summary>
        /// Event handler for when the Ungulate menu entry is selected.
        /// </summary>
        private void UngulateMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            _currentUngulate++;

            if (_currentUngulate > Ungulate.Llama)
                _currentUngulate = 0;

            SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the Language menu entry is selected.
        /// </summary>
        private void LanguageMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            _currentLanguage = (_currentLanguage + 1) % _languages.Length;

            SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the Frobnicate menu entry is selected.
        /// </summary>
        private void FrobnicateMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            _frobnicate = !_frobnicate;

            SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the Elf menu entry is selected.
        /// </summary>
        private void ElfMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            _elf++;

            SetMenuEntryText();
        }

        #endregion
    }
}