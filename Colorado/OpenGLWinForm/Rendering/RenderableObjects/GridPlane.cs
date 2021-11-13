using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.OpenGLWrappers.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGLWinForm.Rendering.RenderableObjects
{
    internal class GridPlane
    {
        private readonly Line[] lines;

        public GridPlane() : this(5, 100, 0) { }
        public GridPlane(double space, double size, double zValue)
        {
            size = ((int)(size / space)) * space;
            var linesList = new List<Line>();

            int numberOfLines = (int)(size / space);
            for (int x = 0; x < numberOfLines; x++)
            {
                linesList.Add(new Line(new Point(-size, x * space, zValue), new Point(size, x * space, zValue)));
                linesList.Add(new Line(new Point(-size, x * -space, zValue), new Point(size, x * -space, zValue)));

                linesList.Add(new Line(new Point(x * space, -size, zValue), new Point(x * space, size, zValue)));
                linesList.Add(new Line(new Point(x * -space, -size, zValue), new Point(x * -space, size, zValue)));
            };

            linesList.Add(new Line(new Point(-size, numberOfLines * space, zValue), new Point(size, numberOfLines * space, zValue)));
            linesList.Add(new Line(new Point(-size, numberOfLines * -space, zValue), new Point(size, numberOfLines * -space, zValue)));

            linesList.Add(new Line(new Point(numberOfLines * space, -size, zValue), new Point(numberOfLines * space, size, zValue)));
            linesList.Add(new Line(new Point(numberOfLines * -space, -size, zValue), new Point(numberOfLines * -space, size, zValue)));

            lines = linesList.ToArray();
            IEnumerable<Point> points = lines.Skip(numberOfLines * 4).Select(l => new Point[] { l.StartPoint, l.EndPoint }).SelectMany(l => l);
            BoundingBox = new BoundingBox(BoundingBox.GetPointWithMaxValues(points), BoundingBox.GetPointWithMinValues(points));
        }

        public bool Visible { get; set; }

        public BoundingBox BoundingBox { get; }

        public void Draw()
        {
            OpenGLFastRenderer.DrawLinesRgb(lines);
        }
    }
}
