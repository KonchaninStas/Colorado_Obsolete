using Colorado.Common.Extensions;
using Colorado.Common.UI.Commands;
using Colorado.Common.UI.Enumerations;
using Colorado.Common.UI.ViewModels.Base;
using System;
using System.Windows;
using System.Windows.Input;

namespace Colorado.Common.UI.ViewModels.WindowViewModels
{
    internal class MessageViewModel : ViewModelBase
    {
        #region Private fields

        private readonly MessageViewType messageViewType;

        #endregion Private fields

        #region Constructors

        public MessageViewModel(string title, string message, MessageViewType messageViewType)
        {
            Title = title;
            Message = message;
            this.messageViewType = messageViewType;
            MessageViewResult = MessageViewResult.Cancel;
        }

        #endregion Constructors

        #region Properties

        public string Title { get; }

        public string Message { get; }

        public Visibility OkayBtnVisibility
        {
            get
            {
                return (messageViewType != MessageViewType.Confirmation).ToVisibility();
            }
        }

        public Visibility ConfirmationBtnVisibility
        {
            get
            {
                return (messageViewType == MessageViewType.Confirmation).ToVisibility();
            }
        }

        public Visibility WarningIconVisibility
        {
            get
            {
                return (messageViewType == MessageViewType.Warning).ToVisibility();
            }
        }

        public MessageViewResult MessageViewResult { get; private set; }

        public Action CloseAction { get; internal set; }

        #endregion Properties

        #region Commands

        public ICommand CloseWindowCommand
        {
            get { return new CommandHandler(CloseWindow); }
        }

        public ICommand YesBtnCommand
        {
            get { return new CommandHandler(YesBtnAction); }
        }

        public ICommand NoBtnCommand
        {
            get { return new CommandHandler(NoBtnAction); }
        }

        #endregion Commands

        #region Private logic

        /// <summary>
        /// Sets <see cref="MessageViewResult"/> to the <see cref="MessageViewResult.Yes"/> 
        /// and closes the message window.
        /// </summary>
        private void YesBtnAction()
        {
            MessageViewResult = MessageViewResult.Yes;
            CloseAction();
        }

        /// <summary>
        /// Sets <see cref="MessageViewResult"/> to the <see cref="MessageViewResult.No"/> 
        /// and closes the message window.
        /// </summary>
        private void NoBtnAction()
        {
            MessageViewResult = MessageViewResult.No;
            CloseAction();
        }

        /// <summary>
        /// Calls the <see cref="CloseAction"/> method to close the message window.
        /// </summary>
        private void CloseWindow()
        {
            CloseAction();
        }

        #endregion Private logic
    }
}