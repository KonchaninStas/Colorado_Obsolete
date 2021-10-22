using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.Enumerations;
using Colorado.OpenGL.OpenGLLibrariesAPI;
using Colorado.OpenGL.WindowsAPI;
using Colorado.OpenGL.WindowsAPI.WindowsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGL.OpenGLWrappers
{
    public static class OpenGLWrapper
    {
        private const int projectionMatrixSize = 16;
        private const int modelViewMatrixSize = 16;
        private const int viewportSizeWidthIndex = 2;
        private const int viewportSizeHeightIndex = 3;

        private static readonly IDictionary<OpenGLCapability, int> capabilityToValuesArraySizeMap;

        static OpenGLWrapper()
        {
            capabilityToValuesArraySizeMap = new Dictionary<OpenGLCapability, int>()
            {
                { OpenGLCapability.Viewport, 4 }
            };
        }


        public static IntPtr LoadOpenGLLibrary()
        {
            return Kernel32LibraryAPI.LoadLibrary(OpenGLLibraryNames.OpenGLLibraryName);
        }

        public static void ClearColor(RGBA rgbaColor)
        {
            OpenGLAPI.ClearColor(rgbaColor.Red / (float)byte.MaxValue, rgbaColor.Green / (float)byte.MaxValue,
                rgbaColor.Blue / (float)byte.MaxValue, rgbaColor.Alpha / (float)byte.MaxValue);
        }

        public static void SetShadingMode(ShadingModel shadingModel)
        {
            OpenGLAPI.ShadeModel((int)shadingModel);
        }

        public static void SetViewport(int x, int y, int width, int height)
        {
            OpenGLAPI.Viewport(x, y, width, height);
        }

        public static void SetOrthographicViewSettings(double left, double right, double bottom, double top, double zNear, double zFar)
        {
            OpenGLAPI.Ortho(left, right, bottom, top, zNear, zFar);
        }

        public static void EnableCapability(OpenGLCapability capability)
        {
            OpenGLAPI.Enable((int)capability);
        }

        public static void DisableCapability(OpenGLCapability capability)
        {
            OpenGLAPI.Disable((int)capability);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newDepthValue">Should be in range [0,1].</param>
        public static void ClearDepthBufferValue(double newDepthValue)
        {
            OpenGLAPI.ClearDepth(newDepthValue);
        }

        public static void ClearDepthBufferValue()
        {
            OpenGLAPI.ClearDepth(1);
        }

        public static void Flush()
        {
            OpenGLAPI.Flush();
        }

        public static void ClearBuffers(params OpenGLBufferType[] bufferTypes)
        {
            foreach (OpenGLBufferType bufferType in bufferTypes)
            {
                ClearBuffer(bufferType);
            }
        }

        public static void ClearBuffer(OpenGLBufferType bufferType)
        {
            OpenGLAPI.Clear((int)bufferType);
        }

        public static void SetActiveMatrixType(MatrixType matrixType)
        {
            OpenGLAPI.MatrixMode((int)matrixType);
        }

        public static void MakeActiveMatrixIdentity()
        {
            OpenGLAPI.LoadIdentity();
        }

        public static Transform GetModelViewMatrix()
        {
            return new Transform(GetParameterValues((int)ViewMatrixArrayType.ModelView, modelViewMatrixSize));
        }

        public static Transform GetProjectionMatrix()
        {
            return new Transform(GetParameterValues((int)ViewMatrixArrayType.Projection, projectionMatrixSize));
        }

        public static void TranslateCurrentMatrix(Point point)
        {
            OpenGLAPI.Translated(point.X, point.Y, point.Z);
        }

        public static void TranslateCurrentMatrix(Vector vector)
        {
            OpenGLAPI.Translated(vector.X, vector.Y, vector.Z);
        }

        public static void MultiplyWithCurrentMatrix(Transform transform)
        {
            OpenGLAPI.MultMatrixd(transform.Array);
        }

        public static void RotateCurrentMatrix(double rotationAngleInDegrees, Vector rotationVector)
        {
            OpenGLAPI.Rotated(rotationAngleInDegrees, rotationVector.X, rotationVector.Y, rotationVector.Z);
        }

        private static double[] GetParameterValues(int parameterName, int parameterValuesArraySize)
        {
            var assignedParameterValues = new double[parameterValuesArraySize];
            OpenGLAPI.GetDoublev(parameterName, assignedParameterValues);

            return assignedParameterValues;
        }

        public static void EnableLight(LightType lightType)
        {
            OpenGLAPI.Enable((int)lightType);
        }

        public static void SetLightParameter(LightType lightType, LightParameter lightParameter, float lightValue)
        {
            OpenGLAPI.Lightf((int)lightType, (int)lightParameter, lightValue);
        }

        public static void SetLightParameter(LightType lightType, LightParameter lightParameter, float[] lightValues)
        {
            OpenGLAPI.Lightfv((int)lightType, (int)lightParameter, lightValues);
        }

        public static void SetLightParameter(LightType lightType, LightColorType lightColorType, float[] lightValues)
        {
            OpenGLAPI.Lightfv((int)lightType, (int)lightColorType, lightValues);
        }

        public static int[] GetParameterValuesArray(OpenGLCapability capability, int valuesArraySize)
        {
            var size = new int[valuesArraySize];
            OpenGLAPI.GetParameterValuesArray((uint)capability, size);
            return size;
        }

        public static int GetViewportWidth()
        {
            return GetParameterValuesArray(OpenGLCapability.Viewport,
                capabilityToValuesArraySizeMap[OpenGLCapability.Viewport])[viewportSizeWidthIndex];
        }
        public static int GetViewportHeight()
        {
            return GetParameterValuesArray(OpenGLCapability.Viewport,
                capabilityToValuesArraySizeMap[OpenGLCapability.Viewport])[viewportSizeHeightIndex];
        }

        public static int[] GetViewport()
        {
            return GetParameterValuesArray(OpenGLCapability.Viewport,
                capabilityToValuesArraySizeMap[OpenGLCapability.Viewport]);
        }
    }
}
