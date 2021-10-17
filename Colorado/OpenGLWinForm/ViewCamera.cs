
using Colorado.Common.Utilities;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGLWinForm.Enumerations;
using System;

namespace Colorado.OpenGLWinForm
{
    public class ViewCamera
    {
        #region Private fields

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

        private Transform cameraRotation;

        #endregion Private fields

        #region Constructor

        public ViewCamera()
        {
            CameraType = CameraType.Orthographic;
            cameraRotation = Transform.Identity();
            Origin = Point.ZeroPoint;
            Translate(new Vector(0, 0, 10), 10.0);

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

        public Quaternion Quaternion => cameraRotation.ToQuaternion();
        #endregion Only getter

        #region Axis

        /// <summary>
        /// Gets the view direction.
        /// </summary>
        /// <value>The view direction.</value>
        public Vector ViewDirection
        {
            get { return cameraRotation * Vector.ZAxis.Inverse; }
        }

        /// <summary>
        /// Gets the camera UP vector.
        /// </summary>
        /// <value>The camera UP vector.</value>
        public Vector UpVector
        {
            get { return cameraRotation * Vector.YAxis; }
        }

        /// <summary>
        /// Gets the reference X direction.
        /// </summary>
        /// <value>The X direction.</value>
        public Vector RightVector
        {
            get
            {
                return cameraRotation * Vector.XAxis;
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

        /// <summary>
        /// Zoom in and out at the given point by moving eye closer to or away
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <param name="fixedPoint">The fixed point in view coordinates.</param>
        public void ScaleAtPoint(double scale, Vector2D fixedPoint)
        {
            Vector v1 = UnifiedMapping(fixedPoint);
            Vector newCenter = v1 * (1.0 - 1.0 / scale);
            double focal = FocalLength;
            Vector2D imageSize = ImageSize;
            double dz = -(focal - focal / scale);
            double dx = imageSize.X / 2.0 * newCenter.X;
            double dy = imageSize.Y / 2.0 * newCenter.Y;
            Vector offset = cameraRotation * new Vector(dx, dy, dz);
            Translate(offset, focal / scale);
        }

        /// <summary>
        /// Zoom in and out at the center of the view by moving eye closer to or away from target
        /// </summary>
        /// <param name="scale">The scale.</param>
        public void ScaleAtTarget(double scale)
        {
            double focal = FocalLength;
            double dz = -(focal - focal / scale);
            Vector offset = cameraRotation * new Vector(0.0, 0.0, dz);
            Translate(offset, focal / scale);
        }

        public void Translate(Vector translationVector, double focalLength)
        {
            if (CameraType != CameraType.Orthographic && FocalLength < 0.001) //hard stop for focal length of 1mm.
            {
                FocalLength = 0.001;
                Origin = Target - 0.001 * ViewDirection;
            }
            else
            {
                FocalLength = focalLength;
                TranslateOrigin(translationVector);
            }
        }

        public void TranslateOrigin(Vector translationVector)
        {
            Origin = Origin + translationVector;
        }

        public void RotateAroundTarget(Vector2D direction)
        {
            RotateAroundTarget(ImageSize / 2, ImageSize / 2 + direction);
        }

        public void RotateAroundTarget(Vector2D from, Vector2D to)
        {
            var t = Target;
            //Vector v1 = UnifiedMapping(from);
            //Vector v2 = UnifiedMapping(to);
            //Vector y = UpVector;
            //double theta = (v2.X - v1.X) / 2.0 * Math.PI;
            //double phi = (v2.Y - v1.Y) / 2.0 * Math.PI;
            //double phi0 = Math.Acos(ViewDirection.UnitVector().DotProduct(y.UnitVector()));
            //if (phi0 + phi < 0.0)
            //    phi = -phi0;
            //else if (phi0 + phi > Math.PI)
            //    phi = Math.PI - phi0;
            //Transform rotate = (Transform.CreateFromAxisAngle(y, theta));
            //Vector x = rotate * RightVector;
            //Transform tilt = (Transform.CreateFromAxisAngle(x, -phi));
            //Transform newRotation = tilt * rotate;
            //Transform curRotation = cameraRotation;
            //cameraRotation = newRotation * curRotation;

            //Vector v1 = UnifiedMapping(from);
            //Vector v2 = UnifiedMapping(to);
            //Vector rotAxis = v2.CrossProduct(v1).UnitVector();
            //double rotAngle = Math.Acos(v2.UnitVector().DotProduct(v1.UnitVector()));
            //Transform newRotation = Transform.CreateFromAxisAngle(rotAxis, rotAngle);
            //Transform curRotation = cameraRotation;
            //cameraRotation = curRotation * newRotation;

            Vector v1 = TrackballMapping(from);
            Vector v2 = TrackballMapping(to);
            Vector rotAxis = v2.CrossProduct(v1).UnitVector();
            double rotAngle = Math.Acos(v2.UnitVector().DotProduct(v1.UnitVector()));
            Transform newRotation = Transform.CreateFromAxisAngle(rotAxis, rotAngle);
            Transform curRotation = cameraRotation;
            Point target = Target;
            double focal = FocalLength;
            cameraRotation = curRotation * newRotation;
            Origin = target - focal * ViewDirection;

            var b = Target;

            if (t.Equals(b))
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
