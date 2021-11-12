using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGL.Structures
{
    public class Light
    {
        public Light(LightType lightType, RGB ambient, RGB diffuse,
            RGB specular, Vector direction)
        {
            IsEnabled = true;
            LightType = lightType;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Direction = direction;
        }

        public bool IsEnabled { get; set; }

        public LightType LightType { get; }

        public RGB Ambient { get; }

        public RGB Diffuse { get; }

        public RGB Specular { get; }

        public Vector Direction { get; set; }

        public static Light GetDefault(LightType lightType)
        {
            return new Light(lightType, new RGB(0f, 0f, 0f),
                lightType == LightType.Light0 ? new RGB(1f, 1f, 1f) : new RGB(0f, 0f, 0f),
                lightType == LightType.Light0 ? new RGB(1f, 1f, 1f) : new RGB(0f, 0f, 0f),
                new Vector(0, 0, 1));
        }
    }
}
