using System.Windows;

namespace Colorado.Common.Extensions
{
    public static class WindowExtensions
    {
        public static Visibility ToVisibility(this bool visibility)
        {
            return visibility ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
