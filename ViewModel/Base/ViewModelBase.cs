using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModel.Base
{
    public class ViewModelBase : INotifyPropertyChanged, INotifyCollectionChanged
    {
        

        public event PropertyChangedEventHandler? PropertyChanged;
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        

        

        protected virtual void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void RaiseCollectionChanged(NotifyCollectionChangedAction action)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action));
        }

        

    }
}