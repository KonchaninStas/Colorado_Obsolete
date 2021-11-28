using System;

namespace Colorado.GeometryDataStructures.Colors
{
    [Serializable]
    public class Material
    {
        public const string DefaultMaterialName = "Default";
        public const string BlackMaterialName = "Black";

        public Material()
        { }

        public Material(string name, RGB ambient, RGB diffuse, RGB specular,
           float shininess, RGB emission)
        {
            Name = name;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;

            if (shininess < 0 || shininess > 128)
            {
                ShininessRadius = 0;
            }
            ShininessRadius = shininess;
            Emission = emission;
        }

        public Material(string name, RGB ambient, RGB diffuse, RGB specular,
           float shininess) : this(name, ambient, diffuse, specular, shininess, Default.Emission) { }

        public Material(RGB ambient, RGB diffuse, RGB specular,
            float shininess, RGB emission) : this(string.Empty, ambient, diffuse, specular, shininess, emission) { }

        public string Name { get; set; }

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
                return new Material(DefaultMaterialName, new RGB(0.2f, 0.2f, 0.2f), new RGB(0.8f, 0.8f, 0.8f),
                   new RGB(0f, 0f, 0f), 0, new RGB(0.0f, 0.0f, 0.0f));
            }
        }

        public static Material Black
        {
            get
            {
                return new Material(BlackMaterialName, new RGB(0f, 0f, 0f), new RGB(0f, 0f, 0f),
                   new RGB(0f, 0f, 0f), 0, new RGB(0.0f, 0.0f, 0.0f));
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public Material GetCopy()
        {
            return new Material(Name, Ambient.GetCopy(), Diffuse.GetCopy(), Specular.GetCopy(), ShininessRadius, Emission.GetCopy());
        }
    }
}
