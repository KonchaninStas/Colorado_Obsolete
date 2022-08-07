using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Colorado.Common.UI.Controls
{
    public class NumberBox : TextBox
    {
        public NumberBox()
        {
            KeyDown += NumberBox_KeyDown;
            TextChanged += NumberBox_TextChanged;
        }

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(nameof(Maximum), typeof(int),
              typeof(NumberBox), new PropertyMetadata(100));

        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(nameof(Minimum), typeof(int),
              typeof(NumberBox), new PropertyMetadata(0));

        [System.ComponentModel.Bindable(true, BindingDirection.TwoWay)]
        public int Value
        {
            get { return (int)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
           DependencyProperty.Register(nameof(Value), typeof(int),
               typeof(NumberBox), new UIPropertyMetadata(0));

        public event EventHandler ValueChanged;

        #region Protected logic

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == ValueProperty)
            {
                Text = Value.ToString();
            }
        }

        #endregion Protected logic

        private void NumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = LeaveOnlyNumbers(Text);

            if (int.TryParse(text, out int value))
            {
                if (value < Minimum || value > Maximum)
                {
                    Text = Value.ToString();
                }
                else
                {
                    Value = value;
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                Value = Minimum;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }

            Text = Value.ToString();
        }

        private void NumberBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = !IsNumberKey(e.Key) && !IsDelOrBackspaceOrTabKey(e.Key);
        }

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
    }
}
