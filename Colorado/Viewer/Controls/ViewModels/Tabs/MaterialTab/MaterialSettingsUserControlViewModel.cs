using Colorado.Common.UI.ViewModels.Controls;
using Colorado.GeometryDataStructures.Colors;
using Colorado.OpenGL.Managers.Materials;
using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.ViewModels.Common;
using Colorado.Viewer.Properties;
using System.Collections.Generic;

namespace Colorado.Viewer.Controls.ViewModels.Tabs.MaterialTab
{
    public class MaterialSettingsUserControlViewModel : ViewerBaseViewModel
    {
        #region Private fields

        private Material selectedMaterial;

        #endregion Private fields

        #region Constructor

        public MaterialSettingsUserControlViewModel(IRenderingControl renderingControl) : base(renderingControl)
        {
            Material defaultMaterial = Material.Default;

            AmbientColorViewModel = new RGBColorPickerUserControlViewModel(Resources.MaterialAmbientColor,
                renderingControl.GeometryRenderer.GlobalMaterialRenderingSettings.GlobalMaterial.Ambient, defaultMaterial.Ambient);
            AmbientColorViewModel.ColorChanged += (s, a) => renderingControl.RefreshView();

            DiffuseColorViewModel = new RGBColorPickerUserControlViewModel(Resources.MaterialDiffuseColor,
                renderingControl.GeometryRenderer.GlobalMaterialRenderingSettings.GlobalMaterial.Diffuse, defaultMaterial.Diffuse);
            DiffuseColorViewModel.ColorChanged += (s, a) => renderingControl.RefreshView();

            SpecularColorViewModel = new RGBColorPickerUserControlViewModel(Resources.MaterialSpecularColor,
                renderingControl.GeometryRenderer.GlobalMaterialRenderingSettings.GlobalMaterial.Specular, defaultMaterial.Specular);
            SpecularColorViewModel.ColorChanged += (s, a) => renderingControl.RefreshView();

            SelectedMaterial = DefaultMaterialsManager.Instance[Material.DefaultMaterialName];
        }

        #endregion Constructor

        #region Properties

        public bool UseGlobalMaterial
        {
            get
            {
                return renderingControl.GeometryRenderer.GlobalMaterialRenderingSettings.UseGlobalMaterial;
            }
            set
            {
                renderingControl.GeometryRenderer.GlobalMaterialRenderingSettings.UseGlobalMaterial = value;

                OnPropertyChanged(nameof(UseGlobalMaterial));
                renderingControl.RefreshView();
            }
        }

        public Material SelectedMaterial
        {
            get
            {
                return selectedMaterial;
            }
            set
            {
                if (value.Name == DefaultMaterialsManager.LastConfiguratedMaterialName)
                {
                    value = DefaultMaterialsManager.Instance[DefaultMaterialsManager.LastConfiguratedMaterialName];
                }
                selectedMaterial = value;

                DefaultMaterialsManager.Instance.SetLastConfiguratedMaterial(renderingControl.GeometryRenderer.GlobalMaterialRenderingSettings.GlobalMaterial.GetCopy());
                SetActiveMaterialParameters(value);

                OnPropertyChanged(nameof(DefaultMaterials));
                OnPropertyChanged(nameof(SelectedMaterial));
                OnPropertyChanged(nameof(AmbientColorViewModel));
                OnPropertyChanged(nameof(DiffuseColorViewModel));
                OnPropertyChanged(nameof(SpecularColorViewModel));
                OnPropertyChanged(nameof(ShininessRadius));
            }
        }

        public IEnumerable<Material> DefaultMaterials => DefaultMaterialsManager.Instance.DefaultMaterials;

        public RGBColorPickerUserControlViewModel AmbientColorViewModel { get; }

        public RGBColorPickerUserControlViewModel DiffuseColorViewModel { get; }

        public RGBColorPickerUserControlViewModel SpecularColorViewModel { get; }

        public float ShininessRadius
        {
            get
            {
                return renderingControl.GeometryRenderer.GlobalMaterialRenderingSettings.GlobalMaterial.ShininessRadius;
            }
            set
            {
                renderingControl.GeometryRenderer.GlobalMaterialRenderingSettings.GlobalMaterial.ShininessRadius = value;
                OnPropertyChanged(nameof(ShininessRadius));
            }
        }

        #endregion Properties

        #region Private logic

        private void SetActiveMaterialParameters(Material newMaterial)
        {
            AmbientColorViewModel.RedColorValue = newMaterial.Ambient.Red;
            AmbientColorViewModel.GreenColorValue = newMaterial.Ambient.Green;
            AmbientColorViewModel.BlueColorValue = newMaterial.Ambient.Blue;

            DiffuseColorViewModel.RedColorValue = newMaterial.Diffuse.Red;
            DiffuseColorViewModel.GreenColorValue = newMaterial.Diffuse.Green;
            DiffuseColorViewModel.BlueColorValue = newMaterial.Diffuse.Blue;

            SpecularColorViewModel.RedColorValue = newMaterial.Specular.Red;
            SpecularColorViewModel.GreenColorValue = newMaterial.Specular.Green;
            SpecularColorViewModel.BlueColorValue = newMaterial.Specular.Blue;

            ShininessRadius = newMaterial.ShininessRadius;
        }

        #endregion Private logic
    }
}
