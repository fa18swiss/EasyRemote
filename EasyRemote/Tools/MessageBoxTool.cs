using System;
using System.Windows;

namespace EasyRemote.Tools
{
    internal static class MessageBoxTool
    {
        /// <summary>
        /// Ask Yes / No / Cancel and performe action
        /// </summary>
        /// <param name="window">Window source</param>
        /// <param name="message">Message</param>
        /// <param name="caption">Capiton</param>
        /// <param name="yesAction">Action run if choise is yes</param>
        /// <param name="yesOrNoAction">Action run if choise is yes or no</param>
        public static void Ask(this Window window, string message, string caption, Action yesAction,
            Action yesOrNoAction)
        {
            var result = MessageBox.Show(window, message, caption, MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes && yesAction != null)
            {
                yesAction();
            }
            if ((result == MessageBoxResult.Yes || result == MessageBoxResult.No) && yesOrNoAction != null)
            {
                yesOrNoAction();
            }
        }

        /// <summary>
        /// Ask Yes / No / Cancel with save before message and performe action
        /// </summary>
        /// <param name="window">Window source</param>
        /// <param name="yesAction">Action run if choise is yes</param>
        /// <param name="yesOrNoAction">Action run if choise is yes or no</param>
        public static void AskSaveBefore(this Window window, Action yesAction, Action yesOrNoAction)
        {
            Ask(window, "Save before ?", "Question", yesAction, yesOrNoAction);
        }
    }
}