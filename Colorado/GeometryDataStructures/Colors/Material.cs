using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.Colors
{
    public class Material
    {
        public Material()
        {
            Ambient = new RGB(1, 0.5, 0.31);
            Diffuse = new RGB(1, 0.5, 0.31);
            Specular = new RGB(0.5, 0.5, 0.5);
            Shininess = 32;
        }

        public RGB Ambient { get; }

        public RGB Diffuse { get; }

        public RGB Specular { get; }

        public float Shininess { get; }
        public float Transparency { get; }
    }
}
