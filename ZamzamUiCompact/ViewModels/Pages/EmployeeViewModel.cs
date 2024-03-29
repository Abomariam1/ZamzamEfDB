﻿using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;
using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class EmployeeViewModel: BaseValidator
{
    #region Private fields
    const string employeeController = "Employee";
    const string departmentController = "Department";
    private readonly string _photoPath =
        $"{Directory.GetParent(path: AppDomain.CurrentDomain.BaseDirectory)!
            .Parent!.Parent!.Parent!.FullName}/Assets/zamzamlogo.png";
    private IUnitOfWork _unitOfWork;
    #endregion

    #region Public Properities
    [ObservableProperty]
    [Required]
    [MaxLength(150)]
    string _employeeName;

    [ObservableProperty]
    [Phone]
    string _phone = string.Empty;
    [Required]
    [ObservableProperty] string _address = string.Empty;

    [Required]
    [ObservableProperty] string _city = string.Empty;

    [Required]
    [ObservableProperty] string _region = string.Empty;

    [ObservableProperty] string _country = string.Empty;

    [ObservableProperty] string? _postalCode = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "يجب ادخال الرقم القومي")]
    [RegularExpression(@"^[0-9]*$", ErrorMessage = "لايسمح بالاحرف في الرقم القومي")]
    long _nationalId;
    [Required]
    [MaxLength(50, ErrorMessage = "الحد الاقصي للمسمى الوظيفي 50 حرف")]
    [ObservableProperty] string _titel = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    [ObservableProperty] private DateTime _hireDate = DateTime.Today;

    [Required]
    [DataType(DataType.Date)]
    [ObservableProperty] DateTime _birthDate = DateTime.Today;

    [Required]
    [RegularExpression(@" ^ [0 - 9] * $")]
    [ObservableProperty] decimal _salary;

    [Required]
    [ObservableProperty] string _qualification = string.Empty;

    [ObservableProperty] byte[]? _photo;

    [ObservableProperty] string _pathPhoto = $"{Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName}/Assets/zamzamlogo.png";

    [ObservableProperty] int _departmentId;

    [ObservableProperty]
    private DepartmentModel _selectedDepartment;

    [ObservableProperty]
    private ObservableCollection<DepartmentModel> _departments;

    [ObservableProperty] EmployeeModel _selectedEmployee;
    [ObservableProperty] ObservableCollection<EmployeeModel> _employees;

    [ObservableProperty] bool _enabled = true;

    #endregion

    #region Constructors

    public EmployeeViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Photo = PathToPhoto(PathPhoto);
    }
    #endregion

    #region Interface Methods
    [RelayCommand]
    public void NewEmployee()
    {
        EmployeeName = string.Empty;
        Phone = string.Empty;
        Address = string.Empty;
        City = string.Empty;
        Region = string.Empty;
        Country = string.Empty;
        PostalCode = string.Empty;
        NationalId = 0;
        Titel = string.Empty;
        HireDate = DateTime.Now;
        BirthDate = DateTime.Now;
        Salary = 0;
        Qualification = string.Empty;
        Photo = PathToPhoto(PathPhoto);
        SelectedDepartment = null;
    }

    [RelayCommand]
    public async Task AddAsync()
    {
        ValidateAllProperties();
        if(HasErrors)
        {
            Message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            Validate(Message, "Error", InfoBarSeverity.Error);
            return;
        }
        EmployeeModel? newEmployee = new()
        {
            EmployeeId = 0,
            EmployeeName = EmployeeName,
            Phone = Phone,
            Address = Address,
            City = City,
            Region = Region,
            PostalCode = PostalCode,
            Country = Country,
            NationalId = NationalId,
            Titel = Titel,
            HireDate = HireDate,
            BirthDate = BirthDate,
            Salary = Salary,
            Qualification = Qualification,
            Photo = Convert.ToBase64String(Photo ?? []),
            DepartmentId = SelectedDepartment.DepartmentId,
            Department = SelectedDepartment
        };
        Enabled = false;
        Result<EmployeeModel>? result = await _unitOfWork.Service<EmployeeModel>().AddAsync(employeeController, newEmployee);
        if(result.Succeeded)
        {
            EmployeeModel? employee = result.Data;
            var index = Departments.IndexOf(SelectedDepartment);
            if(index > -1)
                Departments[index].Employees.Add(employee);
            Validate(result.Message, "Succeeded", InfoBarSeverity.Success);
            //Message = result.Message;
        }
        Enabled = true;
    }

    [RelayCommand]
    public async Task UpdateAsync()
    {
        ValidateAllProperties();
        if(HasErrors)
        {
            Message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            Validate(Message, "Error", InfoBarSeverity.Error);
            return;
        }

        if(SelectedDepartment == null) { return; }
        EmployeeModel? updatedEmployee = new()
        {
            EmployeeId = SelectedEmployee.EmployeeId,
            EmployeeName = EmployeeName,
            Phone = Phone,
            Address = Address,
            City = City,
            Region = Region,
            PostalCode = PostalCode,
            Country = Country,
            NationalId = NationalId,
            Titel = Titel,
            HireDate = HireDate,
            BirthDate = BirthDate,
            Salary = Salary,
            Qualification = Qualification,
            Photo = Convert.ToBase64String(Photo ?? []),
            DepartmentId = SelectedDepartment.DepartmentId,
            Department = SelectedDepartment
        };
        Enabled = false;
        Result<EmployeeModel>? result = await _unitOfWork.Service<EmployeeModel>().UpdateAsync(employeeController, updatedEmployee);
        if(result.Succeeded)
        {
            //Departments = await _department.GetAllDeparmentsAsync();
            EmployeeModel? employee = result.Data;
            //DepartmentModel? newDep = result.Data.Department;

            DepartmentModel? oldDep = Departments.Where(x => x.Employees.Contains(SelectedEmployee)).FirstOrDefault();
            if(oldDep == null)
            {
                Enabled = true;
                Validate("خطاء في القسم", "Error", InfoBarSeverity.Error);
                return;
            }
            List<DepartmentModel>? newList = [.. Departments];
            if(oldDep.DepartmentId != employee.Department.DepartmentId)
            {

            }

            int oldDepIndex = Departments.IndexOf(oldDep);
            DepartmentModel? dd = newList.Where(x => x.DepartmentId == employee.Department.DepartmentId).FirstOrDefault();
            int newDepIndex = newList.IndexOf(dd);

            int delIndex = newList[oldDepIndex].Employees.IndexOf(SelectedEmployee);
            newList[oldDepIndex].Employees.RemoveAt(delIndex);
            newList[newDepIndex].Employees.Add(employee);
            Departments.Clear();
            Departments = new ObservableCollection<DepartmentModel>(newList);
            Validate(result.Message, "Succeeded", InfoBarSeverity.Success);
            Enabled = true;
            NewEmployee();
        }
    }

    [RelayCommand]
    public async Task DeleteAsync()
    {
        ValidateAllProperties();
        if(HasErrors)
        {
            Message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            Validate(Message, "Error", InfoBarSeverity.Error);
            return;
        }
        if(SelectedDepartment == null && SelectedEmployee.EmployeeId == 0) return;
        Enabled = false;
        Result<int>? result = await _unitOfWork.Service<EmployeeModel>().DeleteAsync($"{employeeController}/{SelectedEmployee.EmployeeId} ");
        if(result.Succeeded)
        {
            var index = Departments.IndexOf(SelectedDepartment);
            var empindex = Departments[index].Employees.IndexOf(SelectedEmployee);
            Departments[index].Employees.RemoveAt(empindex);
            Validate(result.Message, "Succeeded", InfoBarSeverity.Success);
        }
        Enabled = true;
    }

    [RelayCommand]
    public void AddPhoto()
    {
        OpenFileDialog openFile = new()
        {
            Title = "الصور",

            Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp"
        };
        bool success = (bool)openFile.ShowDialog()!;
        if(success)
        {
            PathPhoto = openFile.FileName;
            Photo = File.ReadAllBytes(PathPhoto);
            //Photo = PathToPhoto(PathPhoto);
        }
    }

    [RelayCommand]
    public async Task GetAllEmployeesAsync()
    {
        Result<List<DepartmentModel>>? deps = await _unitOfWork.Service<DepartmentModel>().GetAllAsync(departmentController);
        Departments = new ObservableCollection<DepartmentModel>(deps.Data);
        Result<List<EmployeeModel>>? result = await _unitOfWork.Service<EmployeeModel>().GetAllAsync($"{employeeController}/getall");
        var employees = result.Data;
        Employees = new ObservableCollection<EmployeeModel>(employees);
    }

    [RelayCommand]
    public void TextChangedAutoBox()
    {

    }
    #endregion

    #region Private Methods
    public void FillProperities()
    {

        EmployeeName = SelectedEmployee.EmployeeName;
        Phone = SelectedEmployee.Phone;
        Address = SelectedEmployee.Address;
        City = SelectedEmployee.City;
        Region = SelectedEmployee.Region;
        Country = SelectedEmployee.Country;
        PostalCode = SelectedEmployee.PostalCode;
        NationalId = SelectedEmployee.NationalId;
        Titel = SelectedEmployee.Titel;
        HireDate = SelectedEmployee.HireDate;
        BirthDate = SelectedEmployee.BirthDate;
        Salary = SelectedEmployee.Salary;
        Qualification = SelectedEmployee.Qualification ?? "";
        Photo = Convert.FromBase64String(SelectedEmployee.Photo ?? "");

        var emp = Departments.Where(x => x.DepartmentId == SelectedEmployee.DepartmentId)
            .FirstOrDefault()!;
        SelectedDepartment = emp;
        foreach(DepartmentModel department in Departments)
        {
            if(department.Employees.Contains(SelectedEmployee))
                SelectedDepartment = department;
        }
    }
    private byte[] PathToPhoto(string path)
    {
        FileStream fs = new(path, FileMode.Open, FileAccess.Read);
        BinaryReader reader = new(fs);
        byte[]? phto = reader.ReadBytes((int)fs.Length - 1);
        return phto;
    }
    private static string ValidateBase64EncodedString(string inputText)
    {
        string stringToValidate = inputText;
        stringToValidate = stringToValidate.Replace('-', '+'); // 62nd char of encoding
        stringToValidate = stringToValidate.Replace('_', '/'); // 63rd char of encoding
        switch(stringToValidate.Length % 4) // Pad with trailing '='s
        {
            case 0: break; // No pad chars in this case
            case 2: stringToValidate += "=="; break; // Two pad chars
            case 3: stringToValidate += "="; break; // One pad char
            default:
                throw new System.Exception(
         "Illegal base64url string!");
        }

        return stringToValidate;
    }

    #endregion
}
