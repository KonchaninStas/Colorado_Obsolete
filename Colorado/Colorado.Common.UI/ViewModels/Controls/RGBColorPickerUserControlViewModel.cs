using Colorado.Common.UI.Commands;
using Colorado.Common.UI.ViewModels.Base;
using Colorado.GeometryDataStructures.Colors;
using System;
using System.Windows.Input;

namespace Colorado.Common.UI.ViewModels.Controls
{
    public class RGBColorPickerUserControlViewModel : ViewModelBase
    {
        private readonly RGB color;
        private readonly RGB defaultColorSettings;

        public RGBColorPickerUserControlViewModel(string header, RGB color, RGB defaultColorSettings)
        {
            Header = header;
            this.color = color;
            this.defaultColorSettings = defaultColorSettings;
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

        public ICommand RestoreDefaultColorSettingsCommand
        {
            get
            {
                return new CommandHandler(RestoreDefaultColorSettings);
            }
        }

        private void RestoreDefaultColorSettings()
        {
            RedColorValue = defaultColorSettings.Red;
            GreenColorValue = defaultColorSettings.Green;
            BlueColorValue = defaultColorSettings.Blue;
        }
    }
}
