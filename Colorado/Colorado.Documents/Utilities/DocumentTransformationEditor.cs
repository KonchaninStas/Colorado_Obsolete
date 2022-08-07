using Colorado.Common.Enumerations;
using Colorado.Common.Tools.Keyboard;
using Colorado.GeometryDataStructures.Primitives;
using System.Windows.Forms;

namespace Colorado.Documents.Utilities
{
    public class DocumentTransformationEditor : KeyboardToolHandler
    {
        private const double speed = 1.5;

        private readonly DocumentTransformation documentTransformation;

        public DocumentTransformationEditor(DocumentTransformation documentTransformation)
        {
            this.documentTransformation = documentTransformation;
        }

        public override string Name => nameof(DocumentTransformationEditor);

        public override void PreviewKeyDownCallback(PreviewKeyDownEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    {
                        args.IsInputKey = true;
                        break;
                    }
            }
        }

        public override void KeyDownCallback(KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Space)
            {
                args.Handled = true;
                return;
            }
            if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.Space))
            {
                return;
            }
            switch (args.KeyCode)
            {
                case Keys.W:
                    {
                        documentTransformation.Move(MoveDirection.Forward, speed);
                        args.Handled = true;
                        break;
                    }
                case Keys.S:
                    {
                        documentTransformation.Move(MoveDirection.Backward, speed);
                        args.Handled = true;
                        break;
                    }
                case Keys.A:
                    {
                        documentTransformation.Move(MoveDirection.Left, speed);
                        args.Handled = true;
                        break;
                    }
                case Keys.D:
                    {
                        documentTransformation.Move(MoveDirection.Right, speed);
                        args.Handled = true;
                        break;
                    }
                case Keys.ShiftKey:
                    {
                        documentTransformation.Move(MoveDirection.Up, speed);
                        args.Handled = true;
                        break;
                    }
                case Keys.ControlKey:
                    {
                        documentTransformation.Move(MoveDirection.Down, speed);
                        args.Handled = true;
                        break;
                    }
                case Keys.Q:
                    {
                        documentTransformation.RotateAroundCenter(Vector.ZAxis, 1);
                        args.Handled = true;
                        break;
                    }
                case Keys.E:
                    {
                        documentTransformation.RotateAroundCenter(Vector.ZAxis, -1);
                        args.Handled = true;
                        break;
                    }
                case Keys.Up:
                    {
                        documentTransformation.RotateAroundCenter(Vector.YAxis, 1);
                        args.Handled = true;
                        break;
                    }
                case Keys.Down:
                    {
                        documentTransformation.RotateAroundCenter(Vector.YAxis, -1);
                        args.Handled = true;
                        break;
                    }
                case Keys.Left:
                    {
                        documentTransformation.RotateAroundCenter(Vector.XAxis, 1);
                        args.Handled = true;
                        break;
                    }
                case Keys.Right:
                    {
                        documentTransformation.RotateAroundCenter(Vector.XAxis, -1);
                        args.Handled = true;
                        break;
                    }
                case Keys.Oemplus:
                case Keys.Add:
                    {
                        documentTransformation.Scale(1.5);
                        args.Handled = true;
                        break;
                    }
                case Keys.OemMinus:
                case Keys.Subtract:
                    {
                        documentTransformation.Scale(0.5);
                        args.Handled = true;
                        break;
                    }
                case Keys.Space:
                    {
                        args.Handled = true;
                        break;
                    }
            }
        }
    }
}
