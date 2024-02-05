namespace ZamzamUiCompact.ViewModels.Windows;

public partial class StartUpWindowViewModel : ObservableObject, IWindowEvent
{
    private const string _userPath = "user.json";
    private readonly HttpClient _httpClient;
    private readonly IServiceProvider _provider;
    private IAuthenticatedUser _user;
    private readonly MainWindowViewModel _mainWindow;
    private DispatcherTimer _timer = new()
    {
        Interval = TimeSpan.FromMilliseconds(20)
    };

    [ObservableProperty]
    private int _progress;
    public Action Close { get; set; }
    public StartUpWindowViewModel(
        HttpClient httpClient,
        IServiceProvider provider,
        IAuthenticatedUser user,
        MainWindowViewModel mainWindow
    )
    {
        _provider = provider;
        _mainWindow = mainWindow;
        _httpClient = httpClient;
        _user = user.GetUser();
        _timer.Tick += Timer_Tick;
        _timer.Start();
    }
    private void StopAutoProgress()
    {
        // Stop the timer when needed
        if (_timer != null)
        {
            _timer.Stop();
        }
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        // Update the progress at each tick
        Progress += 10; // Example: Increase the progress by 10 units

        // Stop the timer when the progress reaches the maximum value (optional)
        if (Progress >= 2)
        {
            StopAutoProgress();
            UserValidation();
        }
    }

    [RelayCommand]
    public void UserValidation()
    {
        if (_user != null)
        {
            if (_user.IsAutoLogging)
            {
                try
                {
                    _httpClient.DefaultRequestHeaders.Add("Authorization", _user.Token);
                    var respons = _httpClient.GetAsync(_httpClient.BaseAddress + "/getuser").Result;
                    if (respons.IsSuccessStatusCode)
                    {
                        //_mainWindow.User = User;
                        ShowWindow<MainWindow>(_provider);
                        CloseWindow();
                        return;
                    }
                }
                catch (Exception e)
                {
                    string? ms = e.Message;
                }
            }
        }
        ShowWindow<LoginWindow>(_provider);
        CloseWindow();

    }
    public bool CanClose() => true;
    [RelayCommand]
    public void CloseWindow() => Close?.Invoke();
    private void ShowWindow<T>(IServiceProvider serviceProvider) where T : FluentWindow
    {
        var window = serviceProvider.GetRequiredService<T>();
        if (window is MainWindow main)
        {
            main.ViewModel.User = _user;
            window = (T)(FluentWindow)main;
        }
        window.Show();
        StopAutoProgress();
    }

}
