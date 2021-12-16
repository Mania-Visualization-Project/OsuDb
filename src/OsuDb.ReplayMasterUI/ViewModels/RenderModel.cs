using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.ReplayMasterUI.ViewModels
{
    internal class RenderModel : DependencyObject
    {
        public int Progress
        {
            get { return (int)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        public bool IsFailed
        {
            get { return (bool)GetValue(IsFailedProperty); }
            set { SetValue(IsFailedProperty, value); }
        }

        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }

        public bool Finished
        {
            get { return (bool)GetValue(FinishedProperty); }
            set { SetValue(FinishedProperty, value); }
        }

        public string StartTime { get; set; } = string.Empty;

        public ReplayModel Replay { get; set; } = null!;

        // Using a DependencyProperty as the backing store for Finished.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FinishedProperty =
            DependencyProperty.Register("Finished", typeof(bool), typeof(RenderModel), new PropertyMetadata(false));

        // Using a DependencyProperty as the backing store for ErrorMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register("ErrorMessage", typeof(string), typeof(RenderModel), new PropertyMetadata(string.Empty));

        // Using a DependencyProperty as the backing store for IsFailed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFailedProperty =
            DependencyProperty.Register("IsFailed", typeof(bool), typeof(RenderModel), new PropertyMetadata(false));

        // Using a DependencyProperty as the backing store for Progress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.Register("Progress", typeof(int), typeof(RenderModel), new PropertyMetadata(0));
    }
}
