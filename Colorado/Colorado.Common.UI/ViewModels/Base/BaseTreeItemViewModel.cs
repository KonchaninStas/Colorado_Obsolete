using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.Common.UI.ViewModels.Base
{
    public class BaseTreeItemViewModel : ViewModelBase
    {
        public BaseTreeItemViewModel(string name)
        {
            Name = name;
            Children = new ObservableCollection<BaseTreeItemViewModel>();
        }

        public string Name { get; }

        public ObservableCollection<BaseTreeItemViewModel> Children { get; }
    }
}
