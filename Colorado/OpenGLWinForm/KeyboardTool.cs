using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colorado.OpenGLWinForm
{
    internal class KeyboardTool
    {
        private readonly OpenGLControl openGLControl;
        private readonly ViewCamera viewCamera;

        internal KeyboardTool(OpenGLControl openGLControl, ViewCamera viewCamera)
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
                        MoveForward();
                        break;
                    }
                case Keys.S:
                    {
                        MoveBack();
                        break;
                    }
                case Keys.A:
                    {
                        MoveLeft();
                        break;
                    }
                case Keys.D:
                    {
                        MoveRight();
                        break;
                    }
                case Keys.ShiftKey:
                    {
                        MoveUp();
                        break;
                    }
                case Keys.ControlKey:
                    {
                        MoveDown();
                        break;
                    }
                case Keys.Up:
                    {
                        RotateAroundTarget(viewCamera.RightVector, 0.5);
                        break;
                    }
                case Keys.Down:
                    {
                        RotateAroundTarget(viewCamera.RightVector, -10);
                        break;
                    }
                case Keys.Left:
                    {
                        RotateAroundTarget(viewCamera.UpVector, -10);
                        break;
                    }
                case Keys.Right:
                    {
                        RotateAroundTarget(viewCamera.UpVector, 10);
                        break;
                    }
            }
        }

        private void MoveRight()
        {
            MoveOrigin(viewCamera.RightVector);
        }

        private void MoveLeft()
        {
            MoveOrigin(viewCamera.RightVector.Inverse);
        }

        private void MoveBack()
        {
            ScaleAtTarget(0.5);
        }

        private void MoveForward()
        {
            ScaleAtTarget(1.5);
        }

        private void MoveUp()
        {
            MoveOrigin(viewCamera.UpVector);
        }

        private void MoveDown()
        {
            MoveOrigin(viewCamera.UpVector.Inverse);
        }

        private void ScaleAtTarget(double scale)
        {
            viewCamera.ScaleAtTarget(scale);
            openGLControl.Refresh();
        }

        private void MoveOrigin(Vector direction)
        {
            viewCamera.Translate(direction * 0.5);
            openGLControl.Refresh();
        }

        private void RotateAroundTarget(Vector axis, double rotationAngleInDegrees)
        {
            viewCamera.RotateAroundTarget(axis, rotationAngleInDegrees);
            openGLControl.Refresh();
        }
    }
}
