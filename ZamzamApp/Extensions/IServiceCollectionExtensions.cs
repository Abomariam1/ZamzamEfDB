namespace ZamzamApp.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpFactory(configuration);
            services.AddViewModel();
        }

        private static void AddHttpFactory(this IServiceCollection services, IConfiguration configuration)
        {
            string baseAdress = configuration.GetSection("ServerUrl").Value!;
            services.AddHttpClient();
            services.AddHttpClient("", options =>
            {
                options.BaseAddress = new Uri($"{baseAdress}");
                options.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            //services.AddHttpClient("departments", options =>
            //{
            //    options.BaseAddress = new Uri($"{baseAdress}/Department");
            //    //options.DefaultRequestHeaders.add
            //});
        }

        private static void AddViewModel(this IServiceCollection services)
        {
            services.AddSingleton<IApiHelper, ApiHelper>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<Func<Type, ViewModelBase>>(
                    provider => viewModelType => (ViewModelBase)provider.GetRequiredService(viewModelType));
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<SignInViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<StartUpViewModel>();
            services.AddSingleton(provder => new MainWindow
            {
                DataContext = provder.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton(provider => new StartUpWindow
            {
                DataContext = provider.GetRequiredService<StartUpViewModel>(),

            });
        }
    }
}
