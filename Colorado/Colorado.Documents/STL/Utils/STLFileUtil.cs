﻿using System.IO;
using System.Linq;

namespace Colorado.Documents.STL.Utils
{
    public static class STLFileUtil
    {
        private const string stlFileExtension = ".stl";

        public static bool IsStlFile(string pathToStlFile)
        {
            return Path.GetExtension(pathToStlFile).ToLower() == stlFileExtension;
        }

        internal static STLFileType GetStlFileType(string filePath)
        {
            int lineCount = File.ReadLines(filePath).Count(); // number of lines in the file

            string firstLine = File.ReadLines(filePath).First();

            string endLines = File.ReadLines(filePath).Skip(lineCount - 1).Take(1).First() +
                              File.ReadLines(filePath).Skip(lineCount - 2).Take(1).First();

            /* check the file is ascii or not */
            if ((firstLine.IndexOf("solid") != -1) & (endLines.IndexOf("endsolid") != -1))
            {
                return STLFileType.ASCII;
            }
            else
            {
                return STLFileType.Binary;
            }
        }
    }
}
