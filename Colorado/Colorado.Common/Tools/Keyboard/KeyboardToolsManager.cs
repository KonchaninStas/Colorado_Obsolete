using System.Collections.Generic;
using System.Windows.Forms;

namespace Colorado.Common.Tools.Keyboard
{
    public class KeyboardToolsManager
    {
        private readonly List<KeyboardToolHandler> keyboardToolHandlers;

        public KeyboardToolsManager(UserControl userControl)
        {
            keyboardToolHandlers = new List<KeyboardToolHandler>();
            userControl.PreviewKeyDown += PreviewKeyDownCallback;
            userControl.KeyDown += KeyDownCallback;
        }

        public void RegisterKeyboardToolHandler(KeyboardToolHandler keyboardToolHandler)
        {
            keyboardToolHandlers.Add(keyboardToolHandler);
        }

        public void UnregisterKeyboardToolHandler(KeyboardToolHandler keyboardToolHandler)
        {
            keyboardToolHandlers.Remove(keyboardToolHandler);
        }

        private void PreviewKeyDownCallback(object sender, PreviewKeyDownEventArgs args)
        {
            for (int i = keyboardToolHandlers.Count - 1; i >= 0; i--)
            {
                keyboardToolHandlers[i].PreviewKeyDownCallback(args);
            }
        }

        private void KeyDownCallback(object sender, KeyEventArgs args)
        {
            for (int i = keyboardToolHandlers.Count - 1; i >= 0; i--)
            {
                keyboardToolHandlers[i].KeyDownCallback(args);

                if (args.Handled)
                {
                    return;
                }
            }
        }
    }
}
