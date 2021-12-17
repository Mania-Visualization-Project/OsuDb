using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OsuDb.ReplayMasterUI.ViewModels
{
    internal class RenderModel : INotifyPropertyChanged
    {
        public string Title
        {
            get => title;
            set { title = value; NotifyPropertyChanged(); }
        }

        public int Progress
        {
            get => progress;
            set { progress = value; NotifyPropertyChanged(); }
        }

        public bool IsFailed
        {
            get => isFailed;
            set { isFailed = value; NotifyPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => errorMessage;
            set { errorMessage = value; NotifyPropertyChanged(); }
        }

        public string OutputFilePath
        {
            get => outputFilePath;
            set { outputFilePath = value; NotifyPropertyChanged(); }
        }

        public bool Finished
        {
            get => finished;
            set { finished = value; NotifyPropertyChanged(); }
        }

        private string title = string.Empty;
        private int progress;
        private bool isFailed;
        private string errorMessage = string.Empty;
        private string outputFilePath = string.Empty;
        private bool finished;

        public string StartTime { get; set; } = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
