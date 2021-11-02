using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.OpenGLWrappers.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGLWinForm
{
    public class GridPlane
    {
        private readonly List<Line> lines;

        public GridPlane(double space, double size)
        {
            lines = new List<Line>();

            int numberOfLines = (int)(size / space);
            for (int x = 0; x < numberOfLines; x++)
            {
                lines.Add(new Line(new Point(-size, x * space, 0), new Point(size, x * space, 0)));
                lines.Add(new Line(new Point(-size, x * -space, 0), new Point(size, x * -space, 0)));

                lines.Add(new Line(new Point(x * space, -size, 0), new Point(x * space, size, 0)));
                lines.Add(new Line(new Point(x * -space, -size, 0), new Point(x * -space, size, 0)));
            };
        }

        public bool Visible { get; set; }

        public void Draw()
        {
            OpenGLFastRenderer.DrawLinesRgb(lines.ToArray());
        }
    }
}