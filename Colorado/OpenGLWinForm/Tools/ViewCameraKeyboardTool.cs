using Colorado.Common.Enumerations;
using Colorado.Common.Tools.Keyboard;
using Colorado.OpenGLWinForm.Enumerations;
using Colorado.OpenGLWinForm.View;
using System.Windows.Forms;

namespace Colorado.OpenGLWinForm.Tools
{
    public class ViewCameraKeyboardTool : KeyboardToolHandler
    {
        private const double speed = 1.5;

        private readonly OpenGLControl openGLControl;
        private readonly Camera viewCamera;

        internal ViewCameraKeyboardTool(OpenGLControl openGLControl, Camera viewCamera)
        {
            this.openGLControl = openGLControl;
            this.viewCamera = viewCamera;
        }

        public override string Name => nameof(ViewCameraKeyboardTool);

        public override void PreviewKeyDownCallback(PreviewKeyDownEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    {
                        args.IsInputKey = true;
                        break;
                    }
            }
        }

        public override void KeyDownCallback(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    {
                        viewCamera.Move(MoveDirection.Forward, speed);
                        args.Handled = true;
                        break;
                    }
                case Keys.S:
                    {
                        viewCamera.Move(MoveDirection.Backward, speed);
                        args.Handled = true;
                        break;
                    }
                case Keys.A:
                    {
                        viewCamera.Move(MoveDirection.Left, speed);
                        args.Handled = true;
                        break;
                    }
                case Keys.D:
                    {
                        viewCamera.Move(MoveDirection.Right, speed);
                        args.Handled = true;
                        break;
                    }
                case Keys.ShiftKey:
                    {
                        viewCamera.Move(MoveDirection.Up, speed);
                        args.Handled = true;
                        break;
                    }
                case Keys.ControlKey:
                    {
                        viewCamera.Move(MoveDirection.Down, speed);
                        args.Handled = true;
                        break;
                    }
                case Keys.Up:
                    {
                        viewCamera.RotateAroundTarget(RotationDirection.Up, 5);
                        args.Handled = true;
                        break;
                    }
                case Keys.Down:
                    {
                        viewCamera.RotateAroundTarget(RotationDirection.Down, 5);
                        args.Handled = true;
                        break;
                    }
                case Keys.Left:
                    {
                        viewCamera.RotateAroundTarget(RotationDirection.Left, 5);
                        args.Handled = true;
                        break;
                    }
                case Keys.Right:
                    {
                        viewCamera.RotateAroundTarget(RotationDirection.Right, 5);
                        args.Handled = true;
                        break;
                    }
                case Keys.Space:
                    {
                        viewCamera.ResetToDefault();
                        break;
                    }

            }
            openGLControl.Refresh();
        }
    }
}
