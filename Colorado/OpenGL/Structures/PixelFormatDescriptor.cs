using OpenGL.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public class PixelFormatDescriptor
    {
        public ushort Size = 40;
        public ushort Version = 1;
        public uint dwFlags = (uint)(PixelBufferFlags.DRAW_TO_WINDOW | PixelBufferFlags.SUPPORT_OPENGL | PixelBufferFlags.DOUBLEBUFFER);
        public byte PixelType = (byte)PixelTypes.TYPE_RGBA;
        public byte ColorBits = 32;
        public byte RedBits;
        public byte RedShift;
        public byte GreenBits;
        public byte GreenShift;
        public byte BlueBits;
        public byte BlueShift;
        public byte AlphaBits = 8;
        public byte AlphaShift;
        public byte AccumBits;
        public byte AccumRedBits;
        public byte AccumGreenBits;
        public byte AccumBlueBits;
        public byte AccumAlphaBits;
        public byte DepthBits = 24;
        public byte StencilBits = 8;
        public byte AuxBuffers;
        public byte LayerType;
        public byte Reserved;
        public uint dwLayerMask;
        public uint dwVisibleMask;
        public uint dwDamageMask;
    }
}
