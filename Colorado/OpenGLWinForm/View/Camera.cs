using Colorado.Common.Utilities;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.Enumerations;
using Colorado.OpenGL.OpenGLWrappers.View;
using Colorado.OpenGLWinForm.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGLWinForm.View
{
    public class Camera
    {
        private Point objectCenter;              // this helps ortho projection
        private double objectRadius;               // this helps ortho projection

        private Point position;
        private double verticalFieldOfViewInDegrees;

        public Camera()
        {
            ResetToDefault();
        }

        public event EventHandler SettingsChanged;

        public double AspectRatio
        {
            get { return (Height == 0) ? 1.0 : Width / (double)Height; }
        }

        public Vector2D ImageSize
        {
            get
            {
                double imageY = 2.0 * FocalLength * Math.Tan(VerticalFieldOfViewInDegrees * Math.PI / 360);
                return new Vector2D(imageY * AspectRatio, imageY);
            }
        }

        public double FocalLength => Position.DistanceTo(TargetPoint);

        public CameraType CameraType { get; set; }

        public Point Position
        {
            get
            {
                return position;
            }
            private set
            {
                if (value.DistanceTo(TargetPoint) > 0.1)
                {
                    position = value;
                }
            }
        }


        public Point TargetPoint { get; private set; }

        public Vector DirectionVector => new Vector(Position, TargetPoint).UnitVector();

        public Vector RightVector => UpVector.CrossProduct(DirectionVector).UnitVector();

        public Vector UpVector { get; private set; }

        public double FarClip
        {
            get
            {

                double farClip = (objectCenter - Position).DotProduct(DirectionVector) + objectRadius;
                if (CameraType == CameraType.Orthographic)
                {
                    return (farClip > 0.0) ? farClip * 1.001 : farClip * 0.999;
                }
                else
                {
                    return (farClip > 0.01) ? farClip * 1.01 : 0.01;
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
                double nearClip = (objectCenter - Position).DotProduct(DirectionVector) - objectRadius;
                if (CameraType == CameraType.Orthographic)
                {
                    return (nearClip > 0.0) ? nearClip * 0.999 : nearClip * 1.001;
                }

                double minClip = 500 * 0.001;
                return (nearClip > minClip) ? nearClip * 0.999 : minClip;

            }
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public double VerticalFieldOfViewInDegrees
        {
            get
            {
                return verticalFieldOfViewInDegrees;
            }
            set
            {
                if (value > 0.0 && value < 180.0)
                {
                    verticalFieldOfViewInDegrees = value;
                }
            }
        }

        private void TranslatePositionAndTarget(Vector translationVector)
        {
            Position += translationVector;
            TargetPoint += translationVector;
        }

        public void SetViewportParameters(System.Drawing.Rectangle clientRectangle)
        {
            Width = clientRectangle.Width;
            Height = clientRectangle.Height;
        }

        public void SetObjectRange(BoundingBox boundingBox)
        {
            if (boundingBox.IsEmpty)
            {
                objectCenter = Point.ZeroPoint;
                objectRadius = 0.0;
            }
            else
            {
                objectCenter = boundingBox.Center;
                objectRadius = boundingBox.Diagonal / 2;
            }
        }

        public Transform GetViewMatrix()
        {
            return Transform.LookAt(Position, TargetPoint, UpVector);
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

            OpenGLMatrixOperationWrapper.LoadMatrix(GetViewMatrix());
            OpenGLMatrixOperationWrapper.TranslateCurrentMatrix(Position);
        }

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

        public void ResetToDefault()
        {
            TargetPoint = Point.ZeroPoint;
            Position = new Point(0, 0, -50);
            UpVector = Vector.YAxis;
            VerticalFieldOfViewInDegrees = 45;
            SettingsChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Transformation

        public void Move(MoveDirection moveDirection, double speed)
        {
            switch (moveDirection)
            {
                case MoveDirection.Forward:
                    {
                        Position += DirectionVector * speed;
                        break;
                    }
                case MoveDirection.Backward:
                    {
                        Position -= DirectionVector * speed;
                        break;
                    }
                case MoveDirection.Up:
                    {
                        Vector translationVector = UpVector.Inverse * speed;
                        TranslatePositionAndTarget(translationVector);
                        break;
                    }
                case MoveDirection.Down:
                    {
                        Vector translationVector = UpVector * speed;
                        TranslatePositionAndTarget(translationVector);
                        break;
                    }

                case MoveDirection.Right:
                    {
                        Vector translationVector = RightVector * speed;
                        TranslatePositionAndTarget(translationVector);
                        break;
                    }
                case MoveDirection.Left:
                    {
                        Vector translationVector = RightVector.Inverse * speed;
                        TranslatePositionAndTarget(translationVector);
                        break;
                    }
                default:
                    break;
            }
        }

        public void RotateAroundTarget(Vector2D from, Vector2D to)
        {
            double deltaX = to.X - from.X;
            double deltaY = to.Y - from.Y;
            if (deltaX != 0)
            {
                RotateAroundTarget(Vector.ZAxis, -deltaX / 5);
            }
            if (deltaY != 0)
            {
                RotateAroundTarget(RightVector, -deltaY / 5);
            }
        }

        public void RotateAroundTarget(RotationDirection rotationDirection, double angleInDegrees)
        {
            switch (rotationDirection)
            {
                case RotationDirection.Up:
                    {
                        RotateAroundTarget(RightVector, -angleInDegrees);
                        break;
                    }
                case RotationDirection.Down:
                    {
                        RotateAroundTarget(RightVector, +angleInDegrees);
                        break;
                    }
                case RotationDirection.Right:
                    {
                        RotateAroundTarget(Vector.ZAxis, -angleInDegrees);
                        break;
                    }
                case RotationDirection.Left:
                    {
                        RotateAroundTarget(Vector.ZAxis, angleInDegrees);
                        break;
                    }
            }
        }

        public void RotateAroundTarget(Vector rotationAxis, double angleInDegrees)
        {
            RotateAroundTarget(Quaternion.Create(rotationAxis, angleInDegrees));
        }

        public void RotateAroundTarget(Quaternion quaternion)
        {
            Vector newDirection = quaternion.ApplyToVector(DirectionVector);
            UpVector = quaternion.ApplyToVector(UpVector);
            Position = TargetPoint - newDirection * FocalLength;
        }

        #endregion Transformation
    }
}