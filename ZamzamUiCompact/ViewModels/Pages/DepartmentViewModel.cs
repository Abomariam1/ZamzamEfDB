
using System.ComponentModel.DataAnnotations;
using Wpf.Ui.Extensions;
using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class DepartmentViewModel(IUnitOfWork unitOfWork, IContentDialogService dialogService): BaseValidator
{
    const string departmentControler = "Department";
    public event EventHandler? FormSubmissionCompleted;
    public event EventHandler? FormSubmissionFailed;
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

    [ObservableProperty] private bool _enabled = true;

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
            Message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            Validate(Message, "Error", InfoBarSeverity.Error);

        }

    }

    [RelayCommand]
    public async Task Update()
    {
        ValidateAllProperties();
        if(HasErrors)
        {
            Message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            Validate(Message, "Error", InfoBarSeverity.Error);
            return;
        }
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
        ValidateAllProperties();
        if(HasErrors)
        {
            Message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            Validate(Message, "Error", InfoBarSeverity.Error);
            return;
        }

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
            Validate(result.Message, "Error", InfoBarSeverity.Error);

        }
        Enabled = true;
    }

    [RelayCommand]
    public void FillSelectedDepartment()
    {
        DepartmentName = SelectedDepartment?.DepartmentName ?? "";
    }

}
