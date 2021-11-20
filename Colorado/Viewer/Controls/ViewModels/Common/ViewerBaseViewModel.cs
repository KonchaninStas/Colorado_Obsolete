using Colorado.Common.UI.Commands;
using Colorado.Common.UI.ViewModels.Base;
using Colorado.OpenGLWinForm;
using System.Windows.Input;

namespace Colorado.Viewer.Controls.ViewModels.Common
{
    public class ViewerBaseViewModel : ViewModelBase
    {
        #region Fields

        protected IRenderingControl renderingControl;

        #endregion Fields

        #region Constructor

        public ViewerBaseViewModel(IRenderingControl renderingControl)
        {
            this.renderingControl = renderingControl;
        }

        #endregion Constructor

        #region Commands

        public ICommand RefreshViewCommand
        {
            get { return new CommandHandler(() => renderingControl.RefreshView()); }
        }

        #endregion Commands
    }
}
