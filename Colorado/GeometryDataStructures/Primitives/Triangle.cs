using Colorado.Common.Helpers;
using Colorado.GeometryDataStructures.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.Primitives
{
    public class Triangle
    {
        public Triangle(Vertex firstVertex, Vertex secondVertex, Vertex thirdVertex, Vector normal)
        {
            Color = RGBA.RedColor;
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;
            ThirdVertex = thirdVertex;

            Normal = normal;
            Center = (FirstVertex.Position + SecondVertex.Position + ThirdVertex.Position) / 3;
            VerticesValuesArray = ArrayHelper.MergeArrays(new[]{ FirstVertex.VerticesValuesArray,
                SecondVertex.VerticesValuesArray, ThirdVertex.VerticesValuesArray }, FirstVertex.VerticesValuesArray.Length);

            RGBColorsValuesArray = new byte[]
            {
                Color.Red, Color.Green, Color.Blue,
                Color.Red, Color.Green, Color.Blue,
                Color.Red, Color.Green, Color.Blue,
                  Color.Red, Color.Green, Color.Blue,
                Color.Red, Color.Green, Color.Blue,
                Color.Red, Color.Green, Color.Blue,
                  Color.Red, Color.Green, Color.Blue,
                Color.Red, Color.Green, Color.Blue,
                Color.Red, Color.Green, Color.Blue
            };
        }

        public RGBA Color { get; }

        public double[] VerticesValuesArray { get; }

        public byte[] RGBColorsValuesArray { get; }

        public Vertex FirstVertex { get; }

        public Vertex SecondVertex { get; }

        public Vertex ThirdVertex { get; }

        public Vector Normal { get; }

        public Point Center { get; }

        internal Triangle GetTransformed(Transform transform)
        {
            return new Triangle(FirstVertex.GetTransformed(transform), SecondVertex.GetTransformed(transform),
                ThirdVertex.GetTransformed(transform), transform.ApplyToVector(Normal));
        }
    }
}
