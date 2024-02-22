
using System.ComponentModel.DataAnnotations;
using Wpf.Ui.Extensions;
using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class DepartmentViewModel(IUnitOfWork unitOfWork, IContentDialogService dialogService): ObservableValidator
{
    const string departmentControler = "Department";
    public event EventHandler? FormSubmissionCompleted;
    public event EventHandler? FormSubmissionFailed;
    private readonly DispatcherTimer _dispatcher = new()
    {
        Interval = TimeSpan.FromMilliseconds(100)
    };
    private static int counter = 0;
    [ObservableProperty] private int _id;
    //[NotifyPropertyChangedFor(nameof(SelectedDepartment))]
    [ObservableProperty]
    [Required(ErrorMessage = "يجب ادحال اسم القسم!!!")]
    [RegularExpression(@"^[ا-ي]*$", ErrorMessage = "غير مسموح الا باللغة العربية")]
    [MaxLength(20, ErrorMessage = "لا يزيد اسم القسم عن 20 حرف")]
    private string _departmentName = string.Empty;

    [ObservableProperty] private string _createdBy = string.Empty;

    [ObservableProperty] private DateTime _createdOn = DateTime.Now;

    [ObservableProperty] private string _updatedBy = string.Empty;

    [ObservableProperty] private DateTime _updatedOn = DateTime.Now;

    [ObservableProperty]
    private string _message = string.Empty;

    [ObservableProperty] private bool _isInfoBarOpen = false;

    [ObservableProperty] private bool _enabled = true;

    [ObservableProperty] private InfoBarSeverity _saverty = InfoBarSeverity.Informational;
    [ObservableProperty] private string _infoBarTitle = "رسالة";

    [ObservableProperty]
    private ObservableCollection<DepartmentModel> _departments = [];

    [ObservableProperty]

    private DepartmentModel _selectedDepartment = new();

    [RelayCommand]
    public async Task<ObservableCollection<DepartmentModel>> GetAllDeparmentsAsync()
    {
        Result<List<DepartmentModel>>? Response = await unitOfWork.Service<DepartmentModel>().GetAllAsync(departmentControler);
        if(Response.Succeeded)
            Departments = new ObservableCollection<DepartmentModel>(Response.Data);
        return new ObservableCollection<DepartmentModel>(Response.Data);
    }

    [RelayCommand]
    public async Task Create()
    {
        ValidateAllProperties();
        if(!HasErrors)
        {
            ContentDialogResult cmd = await dialogService.ShowSimpleDialogAsync(new SimpleContentDialogCreateOptions
            {
                Content = "هل تريد اضافة قسم جديد؟",
                Title = "اضافة قسم",
                PrimaryButtonText = "نعم",
                CloseButtonText = "الغاء",
            });
            if(cmd.HasFlag(ContentDialogResult.Primary))
            {
                DepartmentModel addmodel = new() { DepartmentName = DepartmentName };
                Result<DepartmentModel>? responce = await unitOfWork.Service<DepartmentModel>().AddAsync(departmentControler, addmodel);

                if(responce.Succeeded)
                {
                    Id = responce.Data.DepartmentId;
                    DepartmentName = responce.Data.DepartmentName;
                    var model = responce.Data;
                    Departments.Add(model);
                    Validate("تم اضافة القسم بنجاح", "Succeeded", InfoBarSeverity.Success);
                    FormSubmissionCompleted?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        else
        {
            FormSubmissionFailed?.Invoke(this, EventArgs.Empty);
        }

    }

    [RelayCommand]
    public async Task Update()
    {
        if(SelectedDepartment == null)
        {
            Validate("يجب اختيار قسم للتعديل", "Warning", InfoBarSeverity.Warning);
            return;
        }

        if(SelectedDepartment.DepartmentId == 0)
        {
            Validate("يجب اختيار قسم للتعديل", "Warning", InfoBarSeverity.Warning);
            return;
        }
        Enabled = false;

        DepartmentModel request = new() { DepartmentId = SelectedDepartment.DepartmentId, DepartmentName = DepartmentName };


        Result<DepartmentModel>? response = await unitOfWork.Service<DepartmentModel>().UpdateAsync(departmentControler, request);
        if(response.Succeeded)
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
        if(SelectedDepartment == null)
        {
            Validate("يجب اخيار قسم للحذف", "Warning", InfoBarSeverity.Warning);
            return;
        }

        if(SelectedDepartment?.DepartmentId == 0)
        {
            Validate("يجب اخيار قسم للحذف", "Warning", InfoBarSeverity.Warning);
            return;
        }
        Enabled = false;
        Result<int>? result = await unitOfWork.Service<DepartmentModel>().DeleteAsync($"{departmentControler}/{SelectedDepartment!.DepartmentId}");
        if(result.Succeeded)
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

    [RelayCommand]
    private void ShowErrors()
    {
        Message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
        IsInfoBarOpen = true;
        Validate(Message, "خطأ", InfoBarSeverity.Error);

        //_ = await dialogService.ShowAlertAsync("تاكد من صحة البيانات", Message, "Ok");
    }

    [RelayCommand]
    private async Task ShowSuccsess()
    {
        ContentDialogResult cmd = await dialogService.ShowSimpleDialogAsync(new SimpleContentDialogCreateOptions
        {
            CloseButtonText = "تم",
            Content = "تمت العملية بنجاج",
            Title = "عملية ناجحة",
            PrimaryButtonText = "OK",
        });
        if(cmd.HasFlag(ContentDialogResult.Primary))
        {

        }
    }
    private void Validate(string message, string title, InfoBarSeverity saverty)
    {
        Message = message;
        IsInfoBarOpen = true;
        InfoBarTitle = title;
        Saverty = saverty;
        StartTimer();
    }
    private void StartTimer()
    {
        _dispatcher.Tick += Dispatcher_Tick;
        _dispatcher.Start();
    }
    private void Dispatcher_Tick(object? sender, EventArgs e)
    {
        counter++;
        if(counter == 15)
        {
            counter = 0;
            _dispatcher.Tick -= Dispatcher_Tick;
            StopAutoProgress();
        }
    }
    private void StopAutoProgress()
    {
        // Stop the timer when needed
        if(_dispatcher != null)
        {
            IsInfoBarOpen = false;
            _dispatcher.Stop();
        }
    }
}
