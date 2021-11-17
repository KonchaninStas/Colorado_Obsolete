using Colorado.Common.Utilities;
using Colorado.GeometryDataStructures.Primitives;
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

        public void ResetToDefault()
        {
            ViewCameraTransform.ResetToDefault();
        }

        public void ApplySettings()
        {
            if (CameraType == CameraType.Orthographic)
            {
                Vector2D imageSize = ImageSize;
                double xmin = -imageSize.X / 2;
                double xmax = imageSize.X / 2;
                double ymin = -imageSize.Y / 2;
                double ymax = imageSize.Y / 2;
                OpenGLViewportWrapper.SetOrthographicViewSettings(
                     xmin * ViewCameraTransform.Scale, xmax * ViewCameraTransform.Scale, ymin * ViewCameraTransform.Scale, 
                     ymax * ViewCameraTransform.Scale, NearClip, FarClip);
            }
            else
            {
                OpenGLViewportWrapper.SetPerspectiveCameraSettings(
                    VerticalFieldOfViewInDegrees, AspectRatio, NearClip, FarClip);
            }
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
            ViewCameraTransform.ScaleAtTarget(0.5);

        }

        public void ScaleOut()
        {
            ViewCameraTransform.ScaleAtTarget(1.5);
        }



        #endregion Public fields

        /// <summary>
        /// Map the view coordinate to unit track ball centered at viewport center.
        /// </summary>
        /// <param name="screenPoint">The screen point.</param>
        /// <returns>Point on unit sphere (unit trackball, centered at target)</returns>
        private Vector TrackballMapping(Vector2D screenPoint)
        {
            double width = Width;
            double height = Height;
            Vector result = new Vector((2.0 * screenPoint.X - width) / width, (height - 2.0 * screenPoint.Y) / height, 0);
            double d = result.Length;
            d = (d < 1.0f) ? d : 1.0f;

            result = new Vector(result.X, result.Y, Math.Sqrt(1.001 - d * d));
            return result.UnitVector();
        }

        #region Private fields

        /// <summary>
        /// Map the view coordinate to unified values in the range of (-1, 1).
        /// </summary>
        /// <param name="screenPoint">The screen point.</param>
        /// <returns>Point on unified image plane in the range of [-1, 1]</returns>
        private Vector UnifiedMapping(Vector2D screenPoint)
        {
            return new Vector((2.0 * screenPoint.X - Width) / Width, (Height - 2.0 * screenPoint.Y) / Height, 0);
        }

        #endregion Private fields
    }
}
