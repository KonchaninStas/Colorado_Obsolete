using System;
using System.Windows.Forms;

namespace Colorado.Common.Tools.Keyboard
{
    public abstract class KeyboardToolHandler : IEquatable<KeyboardToolHandler>
    {
        public abstract string Name { get; }

        public abstract void PreviewKeyDownCallback(PreviewKeyDownEventArgs args);

        public abstract void KeyDownCallback(KeyEventArgs args);

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((KeyboardToolHandler)obj);
        }

        public bool Equals(KeyboardToolHandler other)
        {
            return Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
