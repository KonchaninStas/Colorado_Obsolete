using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.Enumerations;

namespace Colorado.OpenGL.Structures
{
    public class Light
    {
        private double azimuthAngleInDegrees;
        private double altitudeAngleInDegrees;

        public Light(LightType lightType, RGB ambient, RGB diffuse,
            RGB specular, double azimuthAngleInDegrees, double altitudeAngleInDegrees)
        {
            IsEnabled = false;
            LightType = lightType;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            AzimuthAngleInDegrees = azimuthAngleInDegrees;
            AltitudeAngleInDegrees = altitudeAngleInDegrees;

            CalculateDirection();
        }

        public bool IsEnabled { get; set; }

        public LightType LightType { get; }

        public RGB Ambient { get; set; }

        public RGB Diffuse { get; set; }

        public RGB Specular { get; set; }

        public Vector Direction { get; private set; }

        public double AzimuthAngleInDegrees
        {
            get
            {
                return azimuthAngleInDegrees;
            }
            set
            {
                azimuthAngleInDegrees = value;
                CalculateDirection();
            }
        }

        public double AltitudeAngleInDegrees
        {
            get
            {
                return altitudeAngleInDegrees;
            }
            set
            {
                altitudeAngleInDegrees = value;
                CalculateDirection();
            }
        }


        public static Light GetDefault(LightType lightType)
        {
            return new Light(lightType, new RGB(0f, 0f, 0f),
                lightType == LightType.Light0 ? new RGB(1f, 1f, 1f) : new RGB(0f, 0f, 0f),
                lightType == LightType.Light0 ? new RGB(1f, 1f, 1f) : new RGB(0f, 0f, 0f),
                0, 90);
        }

        public override string ToString()
        {
            return LightType.ToString();
        }

        private void CalculateDirection()
        {
            Direction = Quaternion.Create(Vector.ZAxis, AzimuthAngleInDegrees).ApplyToVector(Vector.YAxis);
            Direction = Quaternion.Create(Direction.CrossProduct(Vector.ZAxis), AltitudeAngleInDegrees).ApplyToVector(Direction);

        }
    }
}
