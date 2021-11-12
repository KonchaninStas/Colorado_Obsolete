using Colorado.OpenGL.Enumerations;
using Colorado.OpenGL.OpenGLLibrariesAPI.Helpers;
using System.Collections.Generic;

namespace Colorado.OpenGL.OpenGLWrappers.Helpers
{
    internal static class OpenGLHelper
    {
        private static readonly IDictionary<OpenGLCapability, int> capabilityToValuesArraySizeMap;
        private static readonly IDictionary<ViewMatrixArrayType, int> viewMatrixArrayTypeToValuesArraySizeMap;

        static OpenGLHelper()
        {
            capabilityToValuesArraySizeMap = new Dictionary<OpenGLCapability, int>()
            {
                { OpenGLCapability.Viewport, 4 }
            };

            viewMatrixArrayTypeToValuesArraySizeMap = new Dictionary<ViewMatrixArrayType, int>()
            {
                { ViewMatrixArrayType.ModelView, 16 },
                { ViewMatrixArrayType.Projection, 16 },
            };
        }

        public static int[] GetParameterValuesArray(OpenGLCapability capability)
        {
            return GetParameterValuesArray(capability, capabilityToValuesArraySizeMap[capability]);
        }

        public static double[] GetParameterValues(ViewMatrixArrayType viewMatrixArrayType)
        {
            var assignedParameterValues = new double[
                viewMatrixArrayTypeToValuesArraySizeMap[viewMatrixArrayType]];
            OpenGLHelperAPI.GetDoublev((int)viewMatrixArrayType, assignedParameterValues);

            return assignedParameterValues;
        }

        public static int[] GetParameterValuesArray(OpenGLCapability capability,
            int valuesArraySize)
        {
            var size = new int[valuesArraySize];
            OpenGLHelperAPI.GetParameterValuesArray((uint)capability, size);
            return size;
        }
    }
}
