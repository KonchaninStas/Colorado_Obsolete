using Colorado.Common.UI.ViewModels.Base;
using Colorado.GeometryDataStructures.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Colorado.Viewer.Controls.ViewModels.Tabs.AppearanceTab
{
    public class ColorSettingsUserControlViewModel : ViewModelBase
    {
        private RGB color;

        public ColorSettingsUserControlViewModel(RGB color)
        {
            this.color = color;
        }

        public Color SelectedColor
        {
            get
            {
                return color.ToColor();
            }
            set
            {
                color = new RGB(value.R, value.G, value.B);
                OnPropertyChanged(nameof(SelectedColor));
            }
        }
    }
}
