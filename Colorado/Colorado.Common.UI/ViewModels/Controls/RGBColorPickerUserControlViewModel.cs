using Colorado.Common.UI.ViewModels.Base;
using Colorado.GeometryDataStructures.Colors;
using System;

namespace Colorado.Common.UI.ViewModels.Controls
{
    public class RGBColorPickerUserControlViewModel : ViewModelBase
    {
        private readonly RGB color;

        public RGBColorPickerUserControlViewModel(string header, RGB color)
        {
            Header = header;
            this.color = color;
        }

        public event EventHandler ColorChanged;

        public string Header { get; }

        public byte RedColorValue
        {
            get
            {
                return color.Red;
            }
            set
            {
                color.Red = value;
                OnPropertyChanged(nameof(RedColorValue));
                ColorChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public byte GreenColorValue
        {
            get
            {
                return color.Green;
            }
            set
            {
                color.Green = value;
                OnPropertyChanged(nameof(GreenColorValue));
                ColorChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public byte BlueColorValue
        {
            get
            {
                return color.Blue;
            }
            set
            {
                color.Blue = value;
                OnPropertyChanged(nameof(BlueColorValue));
                ColorChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public int IntensityValue
        {
            get
            {
                return color.Intensity;
            }
            set
            {
                color.Intensity = value;
                OnPropertyChanged(nameof(IntensityValue));
                ColorChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
