
using Colorado.Common.Utilities;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGLWinForm.Enumerations;
using System;

namespace Colorado.OpenGLWinForm
{
    public class ViewCamera
    {
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

        public ViewCamera()
        {
            CameraType = CameraType.Orthographic;
            CameraRotation = new Quaternion();
            Origin = new Point(0.0, 0.0, 10.0);
            _FocalLength = 10.0;
            VerticalFieldOfViewInDegrees = 45.0;
            _NearClip = 0.0;
            _FarClip = 0.0;
            _NearClipAssigned = _FarClipAssigned = false;
            _ObjectCenter = Point.ZeroPoint;
        }

        public Quaternion CameraRotation { get; set; }

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

        public CameraType CameraType { get; }

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

        public int Width { get; set; }

        public int Height { get; set; }

        public double FocalLength
        {
            get
            {
                return _FocalLength;
            }
            set
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
            set
            {
                _NearClip = value;
                _NearClipAssigned = true;
            }
        }

        /// <summary>
        /// Gets the view direction.
        /// </summary>
        /// <value>The view direction.</value>
        public Vector ViewDirection
        {
            get { return CameraRotation * new Vector(0.0, 0.0, -1.0); }
        }

        /// <summary>
        /// Gets the camera UP vector.
        /// </summary>
        /// <value>The camera UP vector.</value>
        public Vector UpVector
        {
            get { return CameraRotation * new Vector(0.0, 1.0, 0.0); }
        }

        /// <summary>
        /// Gets the reference X direction.
        /// </summary>
        /// <value>The X direction.</value>
        public Vector RightVector { get { return CameraRotation * new Vector(1.0, 0.0, 0.0); } }

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
            set
            {
                _FarClip = value;
                _FarClipAssigned = true;
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
            var offset = CameraRotation * new Vector(dx, dy, dz);
            Origin = Origin + offset;
            FocalLength = focal / scale;
        }

        /// <summary>
        /// Zoom in and out at the center of the view by moving eye closer to or away from target
        /// </summary>
        /// <param name="scale">The scale.</param>
        public void ScaleAtTarget(double scale)
        {
            double focal = FocalLength;
            double dz = -(focal - focal / scale);
            Vector offset = CameraRotation * new Vector(0.0, 0.0, dz);
            Origin = Origin + offset;
            FocalLength = focal / scale;
            if (CameraType != CameraType.Orthographic)
            {
                if (FocalLength < 0.001) //hard stop for focal length of 1mm.
                {
                    FocalLength = 0.001;
                    Origin = Target - 0.001 * ViewDirection;
                }
            }
        }

        public void RotateAroundTarget(Vector rotationAxis, double angleInDegrees)
        {
            Quaternion newRotation = new Quaternion(rotationAxis, MathUtilities.ConvertDegreesToRadians(angleInDegrees));
            Quaternion curRotation = CameraRotation;
            Point target = Target;
            CameraRotation = newRotation * curRotation;
            Origin = target - FocalLength * ViewDirection;
        }

        /// <summary>
        /// Map the view coordinate to unified values in the range of (-1, 1).
        /// </summary>
        /// <param name="screenPoint">The screen point.</param>
        /// <returns>Point on unified image plane in the range of [-1, 1]</returns>
        private Vector UnifiedMapping(Vector2D screenPoint)
        {
            return new Vector((2.0 * screenPoint.X - Width) / Width, (Height - 2.0 * screenPoint.Y) / Height, 0);
        }

    }
}
