// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using ZamzamUiCompact.Extensions;

namespace ZamzamUiCompact;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App: Application
{
    private static readonly IHost _host = Host
        .CreateDefaultBuilder()
        .ConfigureAppConfiguration(c =>
        {
            var dirName = Path.GetDirectoryName(Path.GetFullPath(Assembly.GetExecutingAssembly().Location))!;
            var filePath = FindProjectPath(dirName);
            c.SetBasePath(filePath);
            c.AddJsonFile("appsettings.json", false, true);
            c.AddUserSecrets(Assembly.GetExecutingAssembly(), false, true);
            //c.AddJsonFile("user.json", false, true);
        })
        .ConfigureServices((context, services) =>
        {
            services.AddAllServices(context.Configuration);
        })
        .Build();

    /// <summary>
    /// Gets registered service.
    /// </summary>
    /// <typeparam name="T">Type of the service to get.</typeparam>
    /// <returns>Instance of the service or <see langword="null"/>.</returns>
    public static T GetService<T>()
        where T : class
    {
        return _host.Services.GetService(typeof(T)) as T;
    }

    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private void OnStartup(object sender, StartupEventArgs e)
    {
        CultureInfo info = new("ar-EG");
        Thread.CurrentThread.CurrentCulture = info;
        Thread.CurrentThread.CurrentUICulture = info;

        var flowDirection = CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

        FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
            new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.Name)));

        FrameworkElement.FlowDirectionProperty.OverrideMetadata(typeof(FrameworkElement),
            new FrameworkPropertyMetadata(flowDirection));
        _host.Start();
    }

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();

        _host.Dispose();
    }

    /// <summary>
    /// Occurs when an exception is thrown by an application but not handled.
    /// </summary>
    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
        e.Handled = true;
    }

    public static string FindProjectPath(string filePath)
    {
        string directory = Path.GetDirectoryName(filePath);
        while(directory != null)
        {
            // Check if the directory contains a specific file indicating it's the root of the project
            if(File.Exists(Path.Combine(directory, "ZamzamUiCompact.csproj"))) // Change "project.config" to the name of the file indicating the root of the project
            {
                return directory;
            }

            // Move to the parent directory
            directory = Directory.GetParent(directory)?.FullName;
        }

        return null; // Project path not found
    }

}
