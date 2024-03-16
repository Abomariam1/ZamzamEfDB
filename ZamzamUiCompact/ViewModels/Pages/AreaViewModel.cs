using System.ComponentModel.DataAnnotations;
using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class AreaViewModel(IUnitOfWork unitOfWork): ObservableValidator
{
    const string areaController = "Areas";
    const string employeeController = "Employee";
    private readonly DispatcherTimer _dispatcher = new()
    {
        Interval = TimeSpan.FromMilliseconds(100)
    };
    private static int counter = 0;

    [ObservableProperty] int _id;
    [Required]
    [MaxLength(100)]
    [MinLength(2)]
    [ObservableProperty] string _name = String.Empty;

    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    [ObservableProperty] string _station = String.Empty;

    [MaxLength(50)]
    [ObservableProperty] string _location = String.Empty;

    [Required]
    [ObservableProperty] EmployeeModel _employee;

    [ObservableProperty] ObservableCollection<EmployeeModel> _employees;

    [ObservableProperty] AreaModel _area;
    [ObservableProperty] ObservableCollection<AreaModel> _areas;
    [ObservableProperty] string _message;
    [ObservableProperty] bool _status = false;
    [ObservableProperty] InfoBarSeverity _saverty = InfoBarSeverity.Informational;
    [ObservableProperty] string _infoBarTitle = "رسالة";




    [RelayCommand]
    public async Task AddArea()
    {
        //bool isNull = string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Station) || Employee is null;
        //if(isNull) return;
        ValidateAllProperties();
        if(HasErrors)
        {
            Message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            Validate(Message, "Error", InfoBarSeverity.Error);
            return;
        }

        AreaModel area = new()
        {
            AreaId = Id,
            AreaName = Name,
            Station = Station,
            Location = Location,
            CollectorId = Employee.EmployeeId
        };
        Result<AreaModel>? response = await unitOfWork.Service<AreaModel>().AddAsync(areaController, area);
        if(response.Succeeded)
        {
            Area = response.Data;
            Areas.Add(Area);
            Validate(response.Message, "Succeeded", InfoBarSeverity.Success);
        }
        else
            Validate(response.Message, "Error", InfoBarSeverity.Error);



    }

    [RelayCommand]
    public async Task UpdateArea()
    {
        ValidateAllProperties();
        if(HasErrors)
        {
            Message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            Validate(Message, "Error", InfoBarSeverity.Error);
            return;
        }

        if(Area is null)
        {
            Message = "يجب اختيار منطقة للتعديل";
            return;
        }
        AreaModel? toUpdate = new()
        {
            AreaId = Area.AreaId,
            AreaName = Name,
            Station = Station,
            Location = Location,
            CollectorId = Employee.EmployeeId
        };
        Result<AreaModel>? response = await unitOfWork.Service<AreaModel>().UpdateAsync(areaController, toUpdate);
        if(response.Succeeded)
        {
            Area = response.Data;
            int index = Areas.IndexOf(Areas.SingleOrDefault(x => x.AreaId == Area.AreaId)!);
            if(index >= 0)
            {
                Areas[index] = Area;
            }
            Validate(response.Message, "Succeeded", InfoBarSeverity.Success);
        }
        else
        {
            Validate(response.Message, "Error", InfoBarSeverity.Error);
        }
    }

    [RelayCommand]
    public async Task DeleteArea()
    {

        if(Area.AreaId > 0)
        {
            Result<int>? response = await unitOfWork.Service<AreaModel>().DeleteAsync($"{areaController}/{Area.AreaId}");
            if(response.Succeeded)
            {
                Areas.Remove(Area);
                Validate(response.Message, "Succeeded", InfoBarSeverity.Success);
            }
            else
                Validate(response.Message, "Error", InfoBarSeverity.Error);
        }
        else
            Validate("يجب اختيار المنطقة اولا", "Error", InfoBarSeverity.Error);
    }

    [RelayCommand]
    public void FillText()
    {
        if(Area is null) return;
        Name = Area.AreaName;
        Station = Area.Station;
        Location = Area.Location;
        Employee = Employees.FirstOrDefault(x => x.EmployeeId == Area.CollectorId)!;
    }

    [RelayCommand]
    public async Task Startup()
    {
        Result<List<AreaModel>>? areas = await unitOfWork.Service<AreaModel>().GetAllAsync(areaController);
        Result<List<EmployeeModel>>? employees = await unitOfWork.Service<EmployeeModel>().GetAllAsync($"{employeeController}/getall");
        Areas = new ObservableCollection<AreaModel>(areas.Data);
        Employees = new ObservableCollection<EmployeeModel>(employees.Data);
    }

    private void Validate(string message, string title, InfoBarSeverity saverty)
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
