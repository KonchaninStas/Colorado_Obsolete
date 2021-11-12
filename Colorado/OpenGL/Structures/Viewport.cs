namespace Colorado.OpenGL.Structures
{
    public class Viewport
    {
        public Viewport(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            Array = new int[] { X, Y, Width, Height };
        }

        public int X { get; }

        public int Y { get; }

        public int Width { get; }

        public int Height { get; }

        public int[] Array { get; }
    }
}
