namespace ZamzamApp.Core;

public class ObservableObjectBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
            handler!.Invoke(sender: this, e: new PropertyChangedEventArgs(propertyName));
    }
    protected virtual void Dispose() => this.Dispose();
}
