using Colorado.Common.UI.Handlers;
using Colorado.Common.UI.Properties;
using Colorado.GeometryDataStructures.Colors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace Colorado.OpenGL.Managers.Materials
{
    public class DefaultMaterialsManager
    {
        #region Singelton implementation

        private static DefaultMaterialsManager instance;

        public static DefaultMaterialsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DefaultMaterialsManager();
                }
                return instance;
            }
        }

        #endregion Singelton implementation

        #region Constants

        private const string defaultMaterialsFileName = @"Managers\Materials\Content\DefaultMaterials.xml";
        public const string LastConfiguratedMaterialName = "Last configurated";

        #endregion Constants

        #region Private fields

        private Dictionary<string, Material> materialNameToMaterialMap;

        #endregion Private fields

        #region Constructor

        private DefaultMaterialsManager()
        {
            materialNameToMaterialMap = new Dictionary<string, Material>()
            {
                { Material.Default.Name, Material.Default}
            };

            LoadDefaultMaterials();
        }

        #endregion Constructor

        #region Properties

        public Material this[string materialName]
        {
            get
            {
                return materialNameToMaterialMap[materialName];
            }
        }

        public IEnumerable<Material> DefaultMaterials => materialNameToMaterialMap.Values;

        #endregion Properties

        #region Public logic

        public void SetLastConfiguratedMaterial(Material material)
        {
            material.Name = LastConfiguratedMaterialName;
            materialNameToMaterialMap[LastConfiguratedMaterialName] = material;
        }

        #endregion Public logic

        #region Private logic

        private void LoadDefaultMaterials()
        {
            string defaultMaterialsFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                defaultMaterialsFileName);

            if (File.Exists(defaultMaterialsFile))
            {
                try
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(Material[]));
                    using (var fs = new FileStream(defaultMaterialsFile, FileMode.OpenOrCreate))
                    {
                        Material[] materials = (Material[])formatter.Deserialize(fs);

                        foreach (Material material in materials)
                        {
                            materialNameToMaterialMap[material.Name] = material;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageViewHandler.ShowExceptionMessage(Resources.ViewerTitle, Resources.Error_DefaultMaterialsFileIsNotValid, ex);
                }
            }
            else
            {
                MessageViewHandler.ShowWarningMessage(Resources.ViewerTitle, Resources.Error_DefaultMaterialsFileDoesNotExist);
            }
        }

        #endregion Private logic
    }
}
