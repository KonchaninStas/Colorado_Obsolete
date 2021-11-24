using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGLWinForm.Enumerations;
using Colorado.OpenGLWinForm.RenderingControlStructures;
using Colorado.OpenGLWinForm.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colorado.OpenGLWinForm.Tools
{
    internal class KeyboardTool
    {
        double speed = 1.5;

        private readonly OpenGLControl openGLControl;
        private readonly Camera viewCamera;

        internal KeyboardTool(OpenGLControl openGLControl, Camera viewCamera)
        {
            this.openGLControl = openGLControl;
            this.viewCamera = viewCamera;

            openGLControl.PreviewKeyDown += PreviewKeyDownCallback;
            openGLControl.KeyDown += KeyDownCallback;
        }

        private void PreviewKeyDownCallback(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    {
                        e.IsInputKey = true;
                        break;
                    }
            }
        }

        private void KeyDownCallback(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    {
                        viewCamera.Move(MoveDirection.Forward, speed);
                        break;
                    }
                case Keys.S:
                    {
                        viewCamera.Move(MoveDirection.Backward, speed);
                        break;
                    }
                case Keys.A:
                    {
                        viewCamera.Move(MoveDirection.Left, speed);
                        break;
                    }
                case Keys.D:
                    {
                        viewCamera.Move(MoveDirection.Right, speed);
                        break;
                    }
                case Keys.ShiftKey:
                    {
                        viewCamera.Move(MoveDirection.Up, speed);
                        break;
                    }
                case Keys.ControlKey:
                    {
                        viewCamera.Move(MoveDirection.Down, speed);
                        break;
                    }
                case Keys.Up:
                    {
                        viewCamera.RotateAroundTarget(RotationDirection.Up, 5);
                        break;
                    }
                case Keys.Down:
                    {
                        viewCamera.RotateAroundTarget(RotationDirection.Down, 5);
                        break;
                    }
                case Keys.Left:
                    {
                        viewCamera.RotateAroundTarget(RotationDirection.Left, 5);
                        break;
                    }
                case Keys.Right:
                    {
                        viewCamera.RotateAroundTarget(RotationDirection.Right, 5);
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

