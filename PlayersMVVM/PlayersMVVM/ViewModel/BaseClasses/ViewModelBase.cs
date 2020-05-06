using System.ComponentModel;

namespace PlayersMVVM.ViewModel.BaseClasses
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged(params string[] namesOfProperties) //zmiana właściwości
        {
            if (PropertyChanged != null)
            {
                foreach (var prop in namesOfProperties)
                { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
            }
        }
    }
}
