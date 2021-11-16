using System;

namespace Colorado.GeometryDataStructures.Colors
{
    public class Material
    {
        public Material(RGB ambient, RGB diffuse, RGB specular,
            float shininess, RGB emission)
        {
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;

            if (shininess < 0 || shininess > 128)
            {
                throw new Exception();
            }
            ShininessRadius = 20;
            Emission = emission;
        }

        public RGB Ambient { get; set; }

        public RGB Diffuse { get; set; }

        public RGB Specular { get; set; }

        public float ShininessRadius { get; set; }

        public RGB Emission { get; set; }

        public float Transparency { get; set; }

        public static Material Default
        {
            get
            {
                return new Material(new RGB(0.2f, 0.2f, 0.2f), new RGB(0.8f, 0.8f, 0.8f),
                   new RGB(0f, 0f, 0f), 0, new RGB(0.0f, 0.0f, 0.0f));
            }
        }
    }
}
