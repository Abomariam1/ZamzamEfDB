namespace ZamzamUiCompact.ViewModels.Pages;
public partial class BaseValidator: ObservableValidator
{
    private readonly DispatcherTimer _dispatcher = new()
    {
        Interval = TimeSpan.FromMilliseconds(100)
    };
    private static int counter = 0;

    [ObservableProperty] string _message;
    [ObservableProperty] bool _status = false;
    [ObservableProperty] InfoBarSeverity _saverty = InfoBarSeverity.Informational;
    [ObservableProperty] string _infoBarTitle = "رسالة";

    public void Validate(string message, string title, InfoBarSeverity saverty)
    {
        Message = message;
        InfoBarTitle = title;
        Status = true;
        Saverty = saverty;
        StartTimer();
    }
    private void StartTimer()
    {
        _dispatcher.Tick += _dispatcher_Tick;
        _dispatcher.Start();
    }
    private void _dispatcher_Tick(object? sender, EventArgs e)
    {
        counter++;
        if(counter == 15)
        {
            counter = 0;
            StopAutoProgress();
            _dispatcher.Tick -= _dispatcher_Tick;
        }
    }
    private void StopAutoProgress()
    {
        // Stop the timer when needed
        if(_dispatcher != null)
        {
            Status = false;
            _dispatcher.Stop();
        }
    }

}
