using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Colorado.Common.UI.Controls
{
    /// <summary>
    /// Interaction logic for SliderWithNumberBox.xaml
    /// </summary>
    public partial class SliderWithNumberBox : UserControl
    {
        #region Fields

        private bool isSliderUpdating;
        private int lastSavedValue;

        #endregion Fields

        #region Constructor

        public SliderWithNumberBox()
        {
            InitializeComponent();
            Value = 0;
            Slider.PreviewMouseUp += PreviewMouseUpCallback;
            Slider.PreviewMouseDown += PreviewMouseDownCallback;
        }

        #endregion Constructor

        #region Events

        public event EventHandler ValueChanged;

        #endregion Events

        #region Dependency properties

        public Brush SliderBackgroundColor
        {
            get { return (Brush)GetValue(SliderBackgroundColorProperty); }
            set { SetValue(SliderBackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty SliderBackgroundColorProperty =
            DependencyProperty.Register(nameof(SliderBackgroundColorProperty), typeof(Brush),
              typeof(SliderWithNumberBox), new PropertyMetadata(default(Brush)));

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(nameof(Maximum), typeof(int),
              typeof(SliderWithNumberBox), new PropertyMetadata(0));

        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(nameof(Minimum), typeof(int),
              typeof(SliderWithNumberBox), new PropertyMetadata(100));

        [System.ComponentModel.Bindable(true, BindingDirection.TwoWay)]
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set
            {
                NumberBox.Text = value.ToString();
                SetValue(ValueProperty, value);
            }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(int),
              typeof(SliderWithNumberBox), new PropertyMetadata(0));

        #endregion Dependency properties

        #region Event handlers

        private void PreviewMouseDownCallback(object sender, MouseButtonEventArgs e)
        {
            lastSavedValue = Value;
            isSliderUpdating = true;
        }

        private void PreviewMouseUpCallback(object sender, MouseButtonEventArgs e)
        {
            isSliderUpdating = false;
            if (lastSavedValue != Value)
            {
                ValueChangedEventInvoke();
            }  
        }

        private void NumberBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !IsNumberKey(e.Key) && !IsDelOrBackspaceOrTabKey(e.Key);
        }

        #endregion Event handlers

        #region Key logic

        private bool IsNumberKey(Key inKey)
        {
            return inKey >= Key.D0 && inKey <= Key.D9 || inKey >= Key.NumPad0 && inKey <= Key.NumPad9;
        }

        private bool IsDelOrBackspaceOrTabKey(Key inKey)
        {
            return inKey == Key.Delete || inKey == Key.Back || inKey == Key.Tab;
        }

        private string LeaveOnlyNumbers(string inString)
        {
            string tmp = inString;
            foreach (char c in inString.ToCharArray())
            {
                if (!Regex.IsMatch(c.ToString(), "^[0-9]*$"))
                {
                    tmp = tmp.Replace(c.ToString(), "");
                }
            }
            return tmp.TrimStart('0');
        }
        #endregion Key logic

        #region Protected logic

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == MaximumProperty)
            {
                Slider.Maximum = Maximum;
            }
            else if (e.Property == MinimumProperty)
            {
                Slider.Minimum = Minimum;
            }
            else if (e.Property == ValueProperty)
            {
                Slider.Value = Value;
                NumberBox.Text = Value.ToString();
            }
            else if (e.Property == SliderBackgroundColorProperty)
            {
                Slider.Background = SliderBackgroundColor;
            }
        }

        #endregion Protected logic

        #region Private logic

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            NumberBox.Text = ((int)e.NewValue).ToString();
        }

        private void NumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = LeaveOnlyNumbers(NumberBox.Text);

            if (double.TryParse(text, out double value))
            {
                if (value < Minimum || value > Maximum)
                {
                    NumberBox.Text = Value.ToString();
                }
                else
                {
                    int newValue = (int)value;
                    if (Value == 0 && newValue != 0)
                    {
                        Value = int.Parse(newValue.ToString().Replace("0", string.Empty));
                        NumberBox.CaretIndex = NumberBox.Text.Length;
                    }
                    else
                    {
                        Value = newValue;
                    }
                    ValueChangedEventInvoke();
                }
            }
            else
            {
                Value = 0;
                ValueChangedEventInvoke();
            }
        }

        private void ValueChangedEventInvoke()
        {
            if (!isSliderUpdating)
            {
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }    
        }

        #endregion Private logic
    }
}
