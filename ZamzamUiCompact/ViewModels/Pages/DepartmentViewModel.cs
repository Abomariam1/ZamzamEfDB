
using System.ComponentModel.DataAnnotations;
using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class DepartmentViewModel : ObservableValidator
{
    private readonly IUnitOfWork _unitOfWork;
    const string departmentControler = "Department";
    private readonly DispatcherTimer _dispatcher = new()
    {
        Interval = TimeSpan.FromMilliseconds(1000)
    };
    private static int counter = 0;
    [ObservableProperty] private int _id;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedDepartment))]
    [Required]
    private string _departmentName = string.Empty;

    [ObservableProperty] private string _createdBy = string.Empty;

    [ObservableProperty] private DateTime _createdOn = DateTime.Now;

    [ObservableProperty] private string _updatedBy = string.Empty;

    [ObservableProperty] private DateTime _updatedOn = DateTime.Now;

    [ObservableProperty] private ObservableCollection<string> _messages = [];

    [ObservableProperty] private string _message = string.Empty;

    [ObservableProperty] private bool _status = false;

    [ObservableProperty] private bool _enabled = true;

    [ObservableProperty] private InfoBarSeverity _saverty = InfoBarSeverity.Informational;
    [ObservableProperty] private string _infoBarTitle = "رسالة";

    [ObservableProperty]
    private ObservableCollection<DepartmentModel> _departments = [];

    [ObservableProperty]

    private DepartmentModel _selectedDepartment = new();

    public DepartmentViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [RelayCommand]
    public async Task<ObservableCollection<DepartmentModel>> GetAllDeparmentsAsync()
    {
        Result<List<DepartmentModel>>? Response = await _unitOfWork.Service<DepartmentModel>().GetAllAsync(departmentControler);
        if (Response.Succeeded)
            Departments = new ObservableCollection<DepartmentModel>(Response.Data);
        return new ObservableCollection<DepartmentModel>(Response.Data);
        return [];
    }

    [RelayCommand]
    public async Task Create()
    {

        if (!string.IsNullOrEmpty(DepartmentName))
        {
            Enabled = false;
            DepartmentModel addmodel = new() { DepartmentName = DepartmentName };
            Result<DepartmentModel>? responce = await _unitOfWork.Service<DepartmentModel>().AddAsync(departmentControler, addmodel);

            if (responce.Succeeded)
            {
                Id = responce.Data.DepartmentId;
                DepartmentName = responce.Data.DepartmentName;
                var model = responce.Data;
                Departments.Add(model);
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

        else
        {
            Validate("يجب اختيار قسم للاضافة", "Warning", InfoBarSeverity.Warning);
            return;
        }

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

        DepartmentModel request = new() { DepartmentId = SelectedDepartment.DepartmentId, DepartmentName = DepartmentName };


        Result<DepartmentModel>? response = await _unitOfWork.Service<DepartmentModel>().UpdateAsync(departmentControler, request);
        if (response.Succeeded)
        {
            DepartmentModel? model = response.Data;
            int index = Departments.IndexOf(Departments.FirstOrDefault(x => x.DepartmentId == model.DepartmentId)!);
            Departments[index] = model;
            OnPropertyChanged(nameof(Departments));
            Validate(response.Message, "Succeeded", InfoBarSeverity.Success);
        }
        else
        {
            Validate($"خطاء: {response.Message}", "Error", InfoBarSeverity.Error);
        }

        Enabled = true;
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
        Enabled = false;
        Result<int>? result = await _unitOfWork.Service<DepartmentModel>().DeleteAsync($"{departmentControler}/{SelectedDepartment!.DepartmentId}");
        if (result.Succeeded)
        {
            Validate(result.Message, "Succeeded", InfoBarSeverity.Success);
            Departments.Remove(SelectedDepartment);
        }
        else
        {
            Validate(result.Message, "Faild", InfoBarSeverity.Error);

        }
        Enabled = true;
    }

    [RelayCommand]
    public void FillSelectedDepartment()
    {
        DepartmentName = SelectedDepartment?.DepartmentName ?? "";
    }

    private void Validate(string message, string title, InfoBarSeverity saverty)
    {
        Message = message;
        Status = true;
        InfoBarTitle = title;
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
        if (counter == 50)
        {
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
            Status = false;
            _dispatcher.Stop();
        }
    }
}
