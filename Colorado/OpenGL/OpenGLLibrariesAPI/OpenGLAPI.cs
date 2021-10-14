﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.OpenGLLibrariesAPI
{
    internal class OpenGLAPI
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="red">0-1</param>
        /// <param name="green">0-1</param>
        /// <param name="blue">0-1</param>
        /// <param name="alpha">0-1</param>
        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glClearColor")]
        public static extern void ClearColor(float red, float green, float blue, float alpha);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glShadeModel")]
        public static extern void ShadeModel(int mode);
    }
}
