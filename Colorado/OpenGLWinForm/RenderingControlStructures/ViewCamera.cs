using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.OpenGLWrappers;
using Colorado.OpenGLWinForm.Enumerations;
using System;

namespace Colorado.OpenGLWinForm.RenderingControlStructures
{
    public class ViewCamera
    {
        #region Private fields

        private double width;
        private double height;
        private double scale;
        private double _FocalLength;                // distance from origin to target
        private double _VerticalFieldOfViewInDegrees;                // vertical field of view angle, in degrees
        private bool _HasNearClip;
        private bool _HasFarClip;
        private double _NearClip;                   // distance of near clipping plane from origin
        private double _FarClip;                    // distance of far clipping plane from origin
        private bool _NearClipAssigned;
        private bool _FarClipAssigned;

        // no accessible property, set through SetObjectRange() as reference of depth range
        private Point _ObjectCenter;              // this helps ortho projection
        private double _ObjectRadius;               // this helps ortho projection

        #endregion Private fields

        #region Constructor

        public ViewCamera()
        {
            CameraType = CameraType.Orthographic;
            CameraRotation = Quaternion.Identity;
            Origin = Point.ZeroPoint;
            TranslateOrigin(new Vector(0, 0, 10));
            Scale = 1;
            FocalLength = 1;
            VerticalFieldOfViewInDegrees = 45.0;
            _NearClip = 0.0;
            _FarClip = 0.0;
            _NearClipAssigned = _FarClipAssigned = false;
            _ObjectCenter = Point.ZeroPoint;
        }

        #endregion Constructor

        #region Properties

        #region Only getter

        /// <summary>
        /// Gets or sets the camera origin.
        /// </summary>
        /// <value>The origin.</value>
        public Point Origin { get; set; }

        public double Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
            }
        }

        /// <summary>
        /// Gets the camera target (focus point)
        /// </summary>
        /// <value>The target.</value>
        public Point Target
        {
            get { return Origin + ViewDirection * FocalLength; }
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
                if (_HasFarClip)
                {
                    return _FarClip;
                }

                if (_FarClipAssigned)
                {
                    return _FarClip;
                }

                if (_ObjectRadius > 0.0)
                {
                    double farClip = (_ObjectCenter - Origin).DotProduct(ViewDirection) + _ObjectRadius;
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

        internal void ResetToDefault()
        {
            FocalLength = 0;
            CameraRotation = Quaternion.Identity;
            Origin = Point.ZeroPoint;
        }



        /// <summary>
        /// Gets or sets the near clipping distance.
        /// </summary>
        /// <value>The near clipping distance (from camera origin, positive on front).</value>
        public double NearClip
        {
            get
            {
                if (_HasFarClip)
                {
                    return _FarClip;
                }
                if (_FarClipAssigned)
                {
                    return _FarClip;
                }

                if (_ObjectRadius > 0.0)
                {
                    double nearClip = (_ObjectCenter - Origin).DotProduct(ViewDirection) - _ObjectRadius;
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

        /// <summary>
        /// Gets the view direction.
        /// </summary>
        /// <value>The view direction.</value>
        public Vector ViewDirection
        {
            get { return CameraRotation * Vector.ZAxis.Inverse; }
        }

        /// <summary>
        /// Gets the camera UP vector.
        /// </summary>
        /// <value>The camera UP vector.</value>
        public Vector UpVector
        {
            get { return CameraRotation * Vector.YAxis; }
        }

        /// <summary>
        /// Gets the reference X direction.
        /// </summary>
        /// <value>The X direction.</value>
        public Vector RightVector
        {
            get
            {
                return CameraRotation * Vector.XAxis;
            }
        }

        #endregion Axis

        public Quaternion CameraRotation { get; private set; }

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

        public void ApplySettings()
        {
            if (CameraType == CameraType.Orthographic)
            {
                Vector2D imageSize = ImageSize;
                double xmin = -imageSize.X / 2;
                double xmax = imageSize.X / 2;
                double ymin = -imageSize.Y / 2;
                double ymax = imageSize.Y / 2;
                OpenGLWrapper.SetOrthographicViewSettings(xmin * Scale, xmax * Scale, ymin * Scale, ymax * Scale, NearClip, FarClip);
            }
            else
            {
                OpenGLUWrapper.SetPerspectiveCameraSettings(VerticalFieldOfViewInDegrees, AspectRatio, NearClip, FarClip);
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
            ScaleAtTarget(0.5);

        }

        public void ScaleOut()
        {
            ScaleAtTarget(1.5);
        }

        /// <summary>
        /// Zoom in and out at the center of the view by moving eye closer to or away from target
        /// </summary>
        /// <param name="scale">The scale.</param>
        public void ScaleAtTarget(double scale)
        {

            Scale *= scale;
            //double focal = FocalLength;
            //double dz = -(focal - focal / scale);
            //Vector offset = CameraRotation * new Vector(0.0, 0.0, dz);
            //Translate(offset, focal / scale);
        }

        public void TranslateOrigin(Vector translationVector)
        {
            Origin = Origin + translationVector;
        }

        internal void RotateAroundTarget(Vector direction, double angleInDegrees)
        {
            Quaternion newRotation = Quaternion.Create(direction, angleInDegrees);
            Quaternion curRotation = CameraRotation;
            Point target = Target;
            CameraRotation = newRotation * curRotation;
            Origin = target - FocalLength * ViewDirection;

            if (target.Equals(Target))
            {

            }
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
