using Colorado.Common.Utilities;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.Enumerations;
using Colorado.OpenGL.OpenGLWrappers;
using Colorado.OpenGL.OpenGLWrappers.View;
using Colorado.OpenGLWinForm.Enumerations;
using System;

namespace Colorado.OpenGLWinForm.RenderingControlStructures
{
    public class ViewCamera
    {
        #region Private fields

        private double _FocalLength;                // distance from origin to target
        private double _VerticalFieldOfViewInDegrees;                // vertical field of view angle, in degrees

        // no accessible property, set through SetObjectRange() as reference of depth range
        private Point _ObjectCenter;              // this helps ortho projection
        private double _ObjectRadius;               // this helps ortho projection

        #endregion Private fields

        #region Constructor

        public ViewCamera()
        {
            ViewCameraTransform = new ViewCameraTransform();
            CameraType = CameraType.Orthographic;
            FocalLength = 1;
            VerticalFieldOfViewInDegrees = 45.0;
            _ObjectCenter = Point.ZeroPoint;
        }

        #endregion Constructor

        #region Properties

        public ViewCameraTransform ViewCameraTransform { get; }

        #region Only getter

        public Point TargetPoint
        {
            get
            {
                return Point.ZeroPoint + ViewCameraTransform.Translation;
            }
        }

        public Vector2D ImageSize
        {
            get
            {
                double imageY = 2.0 * FocalLength * Math.Tan(_VerticalFieldOfViewInDegrees * Math.PI / 360);
                return new Vector2D(imageY * AspectRatio, imageY);
            }
        }

        /// <summary>
        /// Gets or sets the aspect ratio (width / height).
        /// </summary>
        /// <value>The aspect ratio.</value>
        public double AspectRatio
        {
            get { return (Height == 0) ? 1.0 : Width / (double)Height; }
        }

        /// <summary>
        /// Gets or sets the far clipping distance.
        /// </summary>
        /// <value>The far clipping distance (from camera origin, positive on front).</value>
        public double FarClip
        {
            get
            {
                if (_ObjectRadius > 0.0)
                {
                    double farClip = (_ObjectCenter - ViewCameraTransform.Translation.ToPoint()).DotProduct(ViewDirection) + _ObjectRadius;
                    if (CameraType == CameraType.Orthographic)
                    {
                        return (farClip > 0.0) ? farClip * 1.001 : farClip * 0.999;
                    }
                    else
                    {
                        return (farClip > 0.01) ? farClip * 1.01 : 0.01;
                    }
                }
                if (CameraType == CameraType.Orthographic)
                {
                    return 300 * FocalLength;
                }
                else
                {
                    return 1000 * FocalLength;
                }
            }
        }

        /// <summary>
        /// Gets or sets the near clipping distance.
        /// </summary>
        /// <value>The near clipping distance (from camera origin, positive on front).</value>
        public double NearClip
        {
            get
            {
                if (_ObjectRadius > 0.0)
                {
                    double nearClip = (_ObjectCenter - ViewCameraTransform.Translation.ToPoint()).DotProduct(ViewDirection) - _ObjectRadius;
                    if (CameraType == CameraType.Orthographic)
                    {
                        return (nearClip > 0.0) ? nearClip * 0.999 : nearClip * 1.001;
                    }

                    double minClip = _ObjectRadius * 0.001;
                    return (nearClip > minClip) ? nearClip * 0.999 : minClip;
                }
                if (CameraType == CameraType.Orthographic)
                {
                    return -300 * FocalLength;
                }
                else
                {
                    return 0.001 * FocalLength;
                }
            }
        }

        #endregion Only getter

        #region Axis

        public Vector ViewDirection
        {
            get { return ViewCameraTransform.CameraRotation * Vector.ZAxis.Inverse; }
        }

        public Vector UpVector
        {
            get { return ViewCameraTransform.CameraRotation * Vector.YAxis; }
        }

        public Vector RightVector
        {
            get
            {
                return ViewCameraTransform.CameraRotation * Vector.XAxis;
            }
        }

        #endregion Axis

        public CameraType CameraType { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public double FocalLength
        {
            get
            {
                return _FocalLength;
            }
            private set
            {
                if (value > 0.0)
                {
                    _FocalLength = value;
                }
            }
        }

        public double VerticalFieldOfViewInDegrees
        {
            get
            {
                return _VerticalFieldOfViewInDegrees;
            }
            set
            {
                if (value > 0.0 && value < 180.0)
                {
                    _VerticalFieldOfViewInDegrees = value;
                }
            }
        }

        #endregion Properties

        #region Public fields

        public void RotateAroundTarget(Vector2D from, Vector2D to)
        {
            double deltaX = to.X - from.X;
            double deltaY = to.Y - from.Y;
            if (deltaX != 0)
            {
                Console.WriteLine(deltaX);
                ViewCameraTransform.RotateAroundTarget(Vector.ZAxis, -deltaX/5);
            }
            if (deltaY != 0)
            {
                Console.WriteLine(deltaX);
                ViewCameraTransform.RotateAroundTarget(RightVector, -deltaY / 5);
            }
        }

        public void SetViewportParameters(System.Drawing.Rectangle clientRectangle)
        {
            Width = clientRectangle.Width;
            Height = clientRectangle.Height;
        }

        public void Apply()
        {
            OpenGLViewportWrapper.SetViewport(0, 0, Width, Height);

            // projection scale
            OpenGLMatrixOperationWrapper.SetActiveMatrixType(MatrixType.Projection);
            OpenGLMatrixOperationWrapper.MakeActiveMatrixIdentity();

            ApplySettings();

            // offset & orientation
            OpenGLMatrixOperationWrapper.SetActiveMatrixType(MatrixType.ModelView);
            OpenGLMatrixOperationWrapper.MakeActiveMatrixIdentity();

            OpenGLMatrixOperationWrapper.ScaleCurrentMatrix(ViewCameraTransform.Scale);
            OpenGLMatrixOperationWrapper.RotateCurrentMatrix(
                -MathUtilities.ConvertRadiansToDegrees(ViewCameraTransform.CameraRotation.AngleInRadians),
                ViewCameraTransform.CameraRotation.Axis);
            OpenGLMatrixOperationWrapper.TranslateCurrentMatrix(ViewCameraTransform.Translation.Inverse);
        }

        public void ResetToDefault()
        {
            ViewCameraTransform.ResetToDefault();
        }

        public void SetObjectRange(BoundingBox boundingBox)
        {
            if (boundingBox.IsEmpty)
            {
                _ObjectCenter = Point.ZeroPoint;
                _ObjectRadius = 0.0;
            }
            else
            {
                _ObjectCenter = boundingBox.Center;
                _ObjectRadius = boundingBox.Diagonal / 2;
            }
        }

        public void ScaleIn()
        {
            ViewCameraTransform.ScaleAtTarget(1.5);

        }

        public void ScaleOut()
        {
            ViewCameraTransform.ScaleAtTarget(0.5);
        }

        #endregion Public fields

        #region Private fields

        private void ApplySettings()
        {
            if (CameraType == CameraType.Orthographic)
            {
                Vector2D imageSize = ImageSize;
                double xmin = -imageSize.X / 2;
                double xmax = imageSize.X / 2;
                double ymin = -imageSize.Y / 2;
                double ymax = imageSize.Y / 2;
                OpenGLViewportWrapper.SetOrthographicViewSettings(
                     xmin, xmax, ymin,
                     ymax, NearClip, FarClip);
            }
            else
            {
                OpenGLViewportWrapper.SetPerspectiveCameraSettings(
                    VerticalFieldOfViewInDegrees, AspectRatio, NearClip, FarClip);
            }
        }

        #endregion Private fields
    }
}
