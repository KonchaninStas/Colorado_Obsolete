using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.OpenGLWrappers.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace Colorado.OpenGLWinForm.Rendering.RenderableObjects
{
    public class GridPlane
    {
        private readonly double zValue;

        private int space;
        private double size;
        private Line[] lines;

        public GridPlane() : this(5, 100, 0) { }

        public GridPlane(int space, double size, double zValue)
        {
            this.space = space;
            this.size = size;
            this.zValue = zValue;
            Visible = true;
            Color = RGB.GridDefaultColor;
            Color.Changed += (s, e) => PrepareLines();
            PrepareLines();
        }

        public bool Visible { get; set; }

        public BoundingBox BoundingBox { get; private set; }

        public int Space
        {
            get
            {
                return space;
            }
            set
            {
                space = value;
                PrepareLines();
            }
        }

        public RGB Color { get; }

        public void Draw()
        {
            if (Visible)
            {
                OpenGLFastRenderer.DrawLinesRgb(lines);
            }
        }

        private void PrepareLines()
        {
            int updatedSize = ((int)(size / space)) * space;
            var linesList = new List<Line>();

            int numberOfLines = (int)(updatedSize / space);
            if (numberOfLines < 1)
            {
                numberOfLines = 1;
            }
            for (int x = 0; x < numberOfLines; x++)
            {
                linesList.Add(new Line(new Point(-updatedSize, x * space, zValue), new Point(updatedSize, x * space, zValue), Color));
                linesList.Add(new Line(new Point(-updatedSize, x * -space, zValue), new Point(updatedSize, x * -space, zValue), Color));

                linesList.Add(new Line(new Point(x * space, -updatedSize, zValue), new Point(x * space, updatedSize, zValue), Color));
                linesList.Add(new Line(new Point(x * -space, -updatedSize, zValue), new Point(x * -space, updatedSize, zValue), Color));
            };

            linesList.Add(new Line(new Point(-updatedSize, numberOfLines * space, zValue), new Point(updatedSize, numberOfLines * space, zValue), Color));
            linesList.Add(new Line(new Point(-updatedSize, numberOfLines * -space, zValue), new Point(updatedSize, numberOfLines * -space, zValue), Color));

            linesList.Add(new Line(new Point(numberOfLines * space, -updatedSize, zValue), new Point(numberOfLines * space, updatedSize, zValue), Color));
            linesList.Add(new Line(new Point(numberOfLines * -space, -updatedSize, zValue), new Point(numberOfLines * -space, updatedSize, zValue), Color));

            lines = linesList.ToArray();
            IEnumerable<Point> points = lines.Skip(numberOfLines * 4).Select(l => new Point[] { l.StartPoint, l.EndPoint }).SelectMany(l => l);
            BoundingBox = new BoundingBox(BoundingBox.GetPointWithMaxValues(points), BoundingBox.GetPointWithMinValues(points));
        }
    }
}
