using System.Windows;

namespace Colorado.Viewer.Utilities
{
    internal class UserPreferences
    {
        #region Constructor

        public UserPreferences()
        {
            //Load the settings
            Load();

            //Size it to fit the current screen
            SizeToFit();

            //Move the window at least partially into view
            MoveIntoView();
        }

        #endregion Constructor

        #region Porperties

        public double WindowTop { get; set; }

        public double WindowLeft { get; set; }

        public double WindowHeight { get; set; }

        public double WindowWidth { get; set; }

        public WindowState WindowState { get; set; }

        #endregion Properties

        #region Public logic

        public void Save()
        {
            if (WindowState != System.Windows.WindowState.Minimized)
            {
                Properties.Settings.Default.WindowTop = WindowTop;
                Properties.Settings.Default.WindowLeft = WindowLeft;
                Properties.Settings.Default.WindowHeight = WindowHeight;
                Properties.Settings.Default.WindowWidth = WindowWidth;
                Properties.Settings.Default.WindowState = WindowState;

                Properties.Settings.Default.Save();
            }
        }

        #endregion Public logic

        #region Private logic

        private void Load()
        {
            WindowTop = Properties.Settings.Default.WindowTop;
            WindowLeft = Properties.Settings.Default.WindowLeft;
            WindowHeight = Properties.Settings.Default.WindowHeight;
            WindowWidth = Properties.Settings.Default.WindowWidth;
            WindowState = Properties.Settings.Default.WindowState;
        }

        private void SizeToFit()
        {
            if (WindowHeight > SystemParameters.VirtualScreenHeight)
            {
                WindowHeight = SystemParameters.VirtualScreenHeight;
            }

            if (WindowWidth > SystemParameters.VirtualScreenWidth)
            {
                WindowWidth = SystemParameters.VirtualScreenWidth;
            }
        }

        private void MoveIntoView()
        {
            if (WindowTop + WindowHeight / 2 > SystemParameters.VirtualScreenHeight)
            {
                WindowTop = SystemParameters.VirtualScreenHeight - WindowHeight;
            }

            if (WindowLeft + WindowWidth / 2 > SystemParameters.VirtualScreenWidth)
            {
                WindowLeft = SystemParameters.VirtualScreenWidth - WindowWidth;
            }

            if (WindowTop < 0)
            {
                WindowTop = 0;
            }

            if (WindowLeft < 0)
            {
                WindowLeft = 0;
            }
        }

        #endregion Private logic
    }
}
