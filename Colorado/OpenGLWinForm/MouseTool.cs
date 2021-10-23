﻿using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.OpenGLWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGLWinForm
{
    public class MouseTool
    {
        private readonly OpenGLControl openGLControl;
        private readonly ViewCamera viewCamera;

        internal MouseTool(OpenGLControl openGLControl, ViewCamera viewCamera)
        {
            this.openGLControl = openGLControl;
            this.viewCamera = viewCamera;

            openGLControl.MouseWheel += MouseWheelCallback;
            openGLControl.MouseMove += MouseMoveCallback;
        }

        public Point PointUnderMouse { get; private set; }

        private void MouseMoveCallback(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            PointUnderMouse = OpenGLUWrapper.ScreenToWorld(e.X, e.Y);
            openGLControl.Refresh();
        }

        private void MouseWheelCallback(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                viewCamera.ScaleIn();
            }
            else
            {
                viewCamera.ScaleOut();
            }
            openGLControl.Refresh();
        }
    }
}
