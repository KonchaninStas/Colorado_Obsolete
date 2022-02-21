using Colorado.Documents.STL;
using Colorado.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Colorado.Viewer.Utilities
{
    internal class SampleFilesManager
    {
        private const string pathToSampleFiles = @"Content\STLModels";

        private readonly Application application;

        public SampleFilesManager(Application application)
        {
            this.application = application;
            string sampleFilesFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                pathToSampleFiles);

            if (Directory.Exists(sampleFilesFolder))
            {
                SampleFiles = Directory.GetFiles(sampleFilesFolder).Select(f => new FileInfo(f));
            }
        }

        public bool IsAnyFilePresent => SampleFiles != null;

        public IEnumerable<FileInfo> SampleFiles { get; }

        public void OpenFile(string pathToFile)
        {
            application.DocumentsManager.AddDocument(
                new STLDocument(application.KeyboardToolsManager, pathToFile));
            application.Refresh();
        }
    }
}
