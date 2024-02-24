using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Windows;

public partial class StartUpWindowViewModel(
    IUnitOfWork unitOfWork,
    IOptionsSnapshot<SignInSettingsOptions> settings,
    IServiceProvider provider): ObservableObject, IWindowEvent
{
    private const string account = "Account/getuser";
    private DispatcherTimer _timer = new()
    {
        Interval = TimeSpan.FromMilliseconds(20)
    };

    [ObservableProperty]
    private int _progress;
    public Action Close { get; set; } = () => { };

    [RelayCommand]
    public async Task UserValidation()
    {
        _timer.Tick += _Timer_Tick;
        _timer.Start();
        SignInSettingsOptions? opt = settings.Value;
        if(opt.IsAutoLogging)
        {
            try
            {
                Result<AuthenticatedUser>? result = await unitOfWork.Service<AuthenticatedUser>().GetByIdAsync(account);
                if(result.Succeeded)
                {
                    ShowWindow<MainWindow>(provider);
                    CloseWindow();
                    return;
                }
            }
            catch(Exception e)
            {
                string? ms = e.Message;
            }

        }
        ShowWindow<LoginWindow>(provider);
        CloseWindow();

    }

    private void _Timer_Tick(object? sender, EventArgs e)
    {
        // Update the progress at each tick
        Progress += 10; // Example: Increase the progress by 10 units

        // Stop the timer when the progress reaches the maximum value (optional)
        if(Progress >= 2)
        {
            StopAutoProgress();
        }

    }

    private void StopAutoProgress()
    {
        // Stop the timer when needed
        if(_timer != null)
        {
            _timer.Stop();
        }
    }

    public bool CanClose() => true;
    [RelayCommand]
    public void CloseWindow() => Close?.Invoke();
    private void ShowWindow<T>(IServiceProvider serviceProvider) where T : FluentWindow
    {
        var window = serviceProvider.GetRequiredService<T>();
        if(window is MainWindow main)
        {
            window = (T)(FluentWindow)main;
        }
        window.Show();
        StopAutoProgress();
    }

}
