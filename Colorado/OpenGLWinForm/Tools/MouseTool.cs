using Colorado.Common.Enumerations;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.OpenGLWrappers.View;
using Colorado.OpenGLWinForm.Enumerations;
using Colorado.OpenGLWinForm.View;

namespace Colorado.OpenGLWinForm.Tools
{
    internal class MouseTool
    {
        #region Private fields

        private readonly OpenGLControl openGLControl;
        private readonly Camera viewCamera;

        private Vector2D lastPoint;
        private ViewManipulationType viewManipulationType;
        #endregion Private fields

        #region Constructor

        internal MouseTool(OpenGLControl openGLControl, Camera viewCamera)
        {
            this.openGLControl = openGLControl;
            this.viewCamera = viewCamera;

            openGLControl.MouseWheel += MouseWheelCallback;
            openGLControl.MouseMove += MouseMoveCallback;
            openGLControl.MouseDown += MouseDownCallback;
            openGLControl.MouseUp += MouseUpCallback;
            viewManipulationType = ViewManipulationType.None;
        }

        #endregion Constructor

        #region Properties

        public Point PointUnderMouse { get; private set; }

        #endregion Properties

        #region Event handlers

        private void MouseMoveCallback(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            PointUnderMouse = OpenGLViewportWrapper.ScreenToWorld(e.X, e.Y);

            if (viewManipulationType == ViewManipulationType.Rotation)
            {
                Vector2D curPoint = new Vector2D(e.X, e.Y);
                viewCamera.RotateAroundTarget(lastPoint, curPoint);
                lastPoint = curPoint;
            }

            openGLControl.Refresh();
        }

        private void MouseWheelCallback(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            viewCamera.Move(e.Delta > 0 ? MoveDirection.Forward : MoveDirection.Backward, 10);
            openGLControl.Refresh();
        }

        private void MouseUpCallback(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                viewManipulationType = ViewManipulationType.None;
            }
        }

        private void MouseDownCallback(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                viewManipulationType = ViewManipulationType.Rotation;
                lastPoint = new Vector2D(e.X, e.Y);
            }
        }

        #endregion Event handlers
    }
}
