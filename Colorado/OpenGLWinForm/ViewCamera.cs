
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

        private Transform cameraTransformation;

        #endregion Private fields

        #region Constructor

        public ViewCamera()
        {
            CameraType = CameraType.Orthographic;
            cameraTransformation = Transform.Identity();
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
        public Point Origin => cameraTransformation * new Point(1, 1, 1);

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

        public Quaternion Quaternion => cameraTransformation.ToQuaternion();
        #endregion Only getter

        #region Axis

        /// <summary>
        /// Gets the view direction.
        /// </summary>
        /// <value>The view direction.</value>
        public Vector ViewDirection
        {
            get { return cameraTransformation * Vector.ZAxis.Inverse; }
        }

        /// <summary>
        /// Gets the camera UP vector.
        /// </summary>
        /// <value>The camera UP vector.</value>
        public Vector UpVector
        {
            get { return cameraTransformation * Vector.YAxis; }
        }

        /// <summary>
        /// Gets the reference X direction.
        /// </summary>
        /// <value>The X direction.</value>
        public Vector RightVector
        {
            get
            {
                return cameraTransformation * Vector.XAxis;
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
            Vector offset = cameraTransformation * new Vector(dx, dy, dz);
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
            Vector offset = cameraTransformation * new Vector(0.0, 0.0, dz);
            Translate(offset, focal / scale);
        }

        public void Translate(Vector translationVector, double focalLength)
        {
            if (CameraType != CameraType.Orthographic && FocalLength < 0.001) //hard stop for focal length of 1mm.
            {
                FocalLength = 0.001;
                cameraTransformation.SetTranslation(Target - 0.001 * ViewDirection);
            }
            else
            {
                FocalLength = focalLength;
                cameraTransformation.Translate(translationVector);
            }
        }

        public void Translate(Vector translationVector)
        {
            cameraTransformation.Translate(translationVector);
        }



        public void RotateAroundTarget(Vector rotationAxis, double angleInDegrees)
        {
            //Quaternion newRotation = new Quaternion(rotationAxis, MathUtilities.ConvertDegreesToRadians(angleInDegrees));
            //Quaternion curRotation = CameraTransformation;
            //Point target = Target;
            //CameraTransformation = newRotation * curRotation;
            //Origin = target - FocalLength * ViewDirection;
        }

        #endregion Public fields

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
