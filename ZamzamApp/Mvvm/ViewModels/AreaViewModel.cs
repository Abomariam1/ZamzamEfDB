namespace ZamzamApp.Mvvm.ViewModels;
public class AreaViewModel : ObservableObject
{
    private readonly HttpClient _httpClient;
    private string _name;
    private string _station;
    private string _location;
    private string _employeeId;

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
    public string Station
    {
        get => _station;
        set => SetProperty(ref _station, value);
    }
    public string Location
    {
        get => _location;
        set => SetProperty(ref _location, value);
    }
    public string EmployeeId
    {
        get => _employeeId;
        set => SetProperty(ref _employeeId, value);
    }
    public AreaViewModel(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient.CreateClient();
    }
    public async Task<Area> GetAreaAsync(Area area, CancellationToken cancellationToken)
    {
        Area? respons = await _httpClient.GetFromJsonAsync<Area>("/ArAreas", cancellationToken);
        return respons;
    }


}
