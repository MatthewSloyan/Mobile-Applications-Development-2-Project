using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace G00348036
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //move to single location and call, create a baseView Class and use.
        protected void OnPropertyChanged([CallerMemberName] string thisProperty = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(thisProperty));
        }

        //need this to work with any type, so use generic type T
        // by default params are passed by value, so need a by ref call instead
        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            //if (backingField == value) return;

            if (EqualityComparer<T>.Default.Equals(backingField, value)) return;
            backingField = value;
            OnPropertyChanged(propertyName);
        }
    }
}
