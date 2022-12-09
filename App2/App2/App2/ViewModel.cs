using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace App2
{
    public class ViewModel : ObservableObject
    {
        private static HtmlWebViewSource htmlSourceName = new HtmlWebViewSource();
        private static HtmlWebViewSource htmlSourceExplanation = new HtmlWebViewSource();
        private HtmlWebViewSource _en;
        private HtmlWebViewSource _cz;
        private static double _size = 17;

        private string _style = "<style>.text{ font-size:" + _size + "px !important; }</style>";

        public HtmlWebViewSource Cz
        {
            get { return _cz; }
            set
            {
                _cz = value;

                NotifyPropertyChanged(nameof(Cz));
            }
        }

        public HtmlWebViewSource En
        {
            get { return _en; }
            set
            {
                _en = value;

                NotifyPropertyChanged(nameof(En));
            }
        }

        public ViewModel()
        {
            SetNameAndGrammarDetail("test1","testst2");
            SetExplanationDetail("test3","testst4");
        }

        private void SetNameAndGrammarDetail(string text, string text2)
        {
            htmlSourceName.Html = _style;

            htmlSourceName.Html += "<div class=\"text\"><div>" + text + "</div><div>" + text2 + "</div></div>";

            _en = htmlSourceName;
            NotifyPropertyChanged("En");
        }

        private void SetExplanationDetail(string text, string text2)
        {
            htmlSourceExplanation.Html = _style + "<div class=\"text\"><div>" + text + "</div><div>" + text2 + "</div></div>";
            _cz = htmlSourceExplanation;
            NotifyPropertyChanged("Cz");

        }

    }
      public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableObject()
        {
      
        }
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void NotifyAllPropertiesChanged()
        {
            NotifyPropertyChanged(null);
        }
        bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        protected bool SetProperty<T>(
            ref T backingStore,
            T value,
            [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            NotifyPropertyChanged(propertyName);

            return true;
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}