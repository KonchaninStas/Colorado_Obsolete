using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Colorado.Common.UI.ViewModels.Base
{
    /// <summary>
    /// Represents the base class for the view model class.
    /// Implements the <see cref="INotifyPropertyChanged"/> interface to update the view model property.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Public logic

        /// <summary>
        /// Updates the binding values for all properties.
        /// </summary>
        public virtual void UpdateView() { }

        #endregion Public logic

        #region Protected logic

        protected void OnPropertyChanged(string name)
        {
            VerifyPropertyName(name);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Verifies that the <paramref name="propertyName"/> property is present in the ViewModel.
        /// </summary>
        /// <param name="propertyName">Property name. This property will be checked.</param>
        [Conditional("DEBUG")]
        protected void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
                throw new Exception("Invalid property name: " + propertyName);
        }

        #endregion Protected logic
    }
}
