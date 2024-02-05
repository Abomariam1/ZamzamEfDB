
using System.ComponentModel.DataAnnotations;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class DepartmentViewModel : ObservableValidator
{
    private const string _userPath = "user.json";

    private readonly HttpClient _httpClient;
    private readonly DepartmentModel _model;
    private readonly IAuthenticatedUser _user;
    private readonly DispatcherTimer _dispatcher = new()
    {
        Interval = TimeSpan.FromMicroseconds(200)
    };
    private static int counter = 0;

    [ObservableProperty] private int _id;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedDepartment))]
    [Required]
    private string _departmentName;

    [ObservableProperty] private string _createdBy;

    [ObservableProperty] private DateTime _createdOn;

    [ObservableProperty] private string _updatedBy;

    [ObservableProperty] private DateTime _updatedOn;

    [ObservableProperty] private ObservableCollection<string> _messages = [];

    [ObservableProperty] private string _message;

    [ObservableProperty] private bool _status;

    [ObservableProperty] private bool _enabled = true;

    [ObservableProperty] private InfoBarSeverity _saverty;
    [ObservableProperty] private string _infoBarTitle;

    [ObservableProperty]
    private ObservableCollection<DepartmentModel> _departments = [];

    [ObservableProperty]

    private DepartmentModel _selectedDepartment;

    public DepartmentViewModel(HttpClient httpClient, DepartmentModel model, IAuthenticatedUser user)
    {
        _httpClient = httpClient;
        _model = model;
        _user = user;
        _user = JsonSerializer.Deserialize<AuthenticatedUser>(File.ReadAllText(_userPath))!;
        _httpClient.DefaultRequestHeaders.Add("Authorization", _user.Token);
        Departments = Task.Run(GetAllDeparmentsAsync).GetAwaiter().GetResult();
        //Departments = Task.Run(GetAllDeparments).ContinueWith((x) => x.Result).Result;
        //Departments = Task.Run(GetAllDeparments).ConfigureAwait(false).GetAwaiter().GetResult();
        //Departments = Task.Run(GetAllDeparments).WaitAsync(new CancellationToken()).Result;
    }

    [RelayCommand]
    public async Task<ObservableCollection<DepartmentModel>> GetAllDeparmentsAsync()
    {
        Result<List<DepartmentModel>>? Response = await _httpClient.GetFromJsonAsync<Result<List<DepartmentModel>>>
            (_httpClient.BaseAddress);
        if (Response.Succeeded)
            return new ObservableCollection<DepartmentModel>(Response.Data);
        return [];
    }

    [RelayCommand]
    public async Task Create()
    {

        if (!string.IsNullOrEmpty(DepartmentName))
        {
            Enabled = false;
            _model.DepartmentName = DepartmentName;
            var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress, _model);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                Result<DepartmentModel>? responce = await response.Content.ReadFromJsonAsync<Result<DepartmentModel>>();
                if (responce.Succeeded)
                {

                    Id = responce.Data.DepartmentId;
                    DepartmentName = responce.Data.DepartmentName;
                    var model = responce.Data;
                    _departments.Add(model);
                    Validate("تم اضافة القسم بنجاح", "Succeeded", InfoBarSeverity.Success);
                    Enabled = true;
                    return;
                }
                else
                {
                    Validate("فشل في انشاء القسم", "Error", InfoBarSeverity.Error);
                    Enabled = true;
                    return;
                }
            }
        }
        else
        {
            Validate("يجب اختيار قسم للاضافة", "Warning", InfoBarSeverity.Warning);
            return;
        }

    }

    private void StartTimer()
    {
        _dispatcher.Tick += _dispatcher_Tick;
        _dispatcher.Start();
    }

    [RelayCommand]
    public async Task Update()
    {
        if (SelectedDepartment == null)
        {
            Validate("يجب اختيار قسم للتعديل", "Warning", InfoBarSeverity.Warning);
            return;
        }

        if (SelectedDepartment.DepartmentId == 0)
        {
            Validate("يجب اختيار قسم للتعديل", "Warning", InfoBarSeverity.Warning);
            return;
        }
        Enabled = false;

        _model.DepartmentId = SelectedDepartment.DepartmentId;
        _model.DepartmentName = DepartmentName;


        var response = await _httpClient.PutAsJsonAsync(_httpClient.BaseAddress, _model);
        if (response.IsSuccessStatusCode)
        {
            Result<int>? result = await response.Content.ReadFromJsonAsync<Result<int>>();
            if (result.Succeeded)
            {
                Id = result.Data;
                var model = new DepartmentModel { DepartmentId = Id, DepartmentName = DepartmentName };
                var index = Departments.IndexOf(Departments.FirstOrDefault(x => x.DepartmentId == Id));
                Departments[index] = model;
                OnPropertyChanged(nameof(Departments));
                Validate(result.Message[0], "Succeeded", InfoBarSeverity.Success);
            }
            else
            {
                Validate($"خطاء: {result.Message}", "Error", InfoBarSeverity.Error);
            }
        }
        Enabled = true;
    }

    private void Validate(string message, string title, InfoBarSeverity saverty)
    {
        StartTimer();
        Message = message;
        Status = true;
        Saverty = saverty;
    }

    [RelayCommand]
    public async Task Delete()
    {
        if (SelectedDepartment == null)
        {
            Validate("يجب اخيار قسم للحذف", "Warning", InfoBarSeverity.Warning);
            return;
        }

        if (SelectedDepartment?.DepartmentId == 0)
        {
            Validate("يجب اخيار قسم للحذف", "Warning", InfoBarSeverity.Warning);
            return;
        }
        //var json = JsonSerializer.Serialize(SelectedDepartment.DepartmentId);
        //var content = new StringContent(json, Encoding.UTF8, "application/json");
        //var request = new HttpRequestMessage();
        //request.Method = HttpMethod.Delete;
        //request.RequestUri = _httpClient.BaseAddress;
        //request.Content = content;
        //var response = await _httpClient.SendAsync(request);
        Enabled = false;
        var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/{SelectedDepartment.DepartmentId}");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            Result<int>? result = JsonSerializer.Deserialize<Result<int>>(json, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
            Validate(result.Message[0], "Succeeded", InfoBarSeverity.Success);
            Departments.Remove(SelectedDepartment);
        }
        Enabled = true;
    }

    [RelayCommand]
    public async Task FillSelectedDepartment()
    {
        DepartmentName = SelectedDepartment?.DepartmentName ?? "";
    }

    private void _dispatcher_Tick(object? sender, EventArgs e)
    {
        Status = true;
        counter++;
        if (counter == 20)
        {
            Status = false;
            counter = 0;
            StopAutoProgress();
            _dispatcher.Tick -= _dispatcher_Tick;
        }
    }
    private void StopAutoProgress()
    {
        // Stop the timer when needed
        if (_dispatcher != null)
        {
            _dispatcher.Stop();
        }
    }
}
