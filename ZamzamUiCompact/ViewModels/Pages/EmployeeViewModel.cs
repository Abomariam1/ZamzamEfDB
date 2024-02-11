using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class EmployeeViewModel : ObservableValidator
{
    #region Private fields
    private const string _userPath = "user.json";
    private readonly IAuthenticatedUser _user;
    private readonly HttpClient _httpClient;

    private readonly string _photoPath = $"{Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName}/Assets/zamzamlogo.png";

    private readonly DispatcherTimer _dispatcher = new()
    {
        Interval = TimeSpan.FromMicroseconds(200)
    };

    private readonly DepartmentViewModel _department;

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

    [ObservableProperty] long _nationalId;
    [Required]
    [ObservableProperty] string _titel = string.Empty;

    [Required]
    [ObservableProperty] private DateTime _hireDate = DateTime.Today;

    [Required]
    [ObservableProperty] DateTime _birthDate = DateTime.Today;

    [Required]
    [ObservableProperty] decimal _salary;

    [Required]
    [ObservableProperty] string _qualification = string.Empty;

    [ObservableProperty] byte[]? _photo;

    [ObservableProperty] string _pathPhoto = $"{Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName}/Assets/zamzamlogo.png";

    [ObservableProperty] int _departmentId;

    [ObservableProperty]
    private DepartmentModel _selectedDepartment;

    [ObservableProperty]
    [Required]
    private ObservableCollection<DepartmentModel> _departments;

    [ObservableProperty] private EmployeeModel _selectedEmployee;
    [ObservableProperty]
    private ObservableCollection<EmployeeModel> _employees;

    #endregion

    #region Constructors

    public EmployeeViewModel(HttpClient httpClient, DepartmentViewModel department, IAuthenticatedUser user)
    {
        _httpClient = httpClient;
        _user = user.GetUser();
        _httpClient.DefaultRequestHeaders.Add("Authorization", _user.Token);
        _department = department;
        Departments = _department.Departments;
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
        //var ph = Encoding.UTF8.GetBytes(SelectedEmployee.Photo ?? "");
        //var ph = 
        Photo = PathToPhoto(PathPhoto);
        SelectedDepartment = null;
    }

    [RelayCommand]
    public async Task AddEmployee()
    {
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
        };

        var request = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress, newEmployee);
        if (request.IsSuccessStatusCode)
        {
            var result = await request.Content.ReadAsStringAsync();
            Result<EmployeeModel>? emp = JsonSerializer.Deserialize<Result<EmployeeModel>>(result, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
            EmployeeModel? employee = emp.Data;

            if (emp != null)
            {
                Departments = await _department.GetAllDeparmentsAsync();

                //var index = Departments.IndexOf(SelectedDepartment);
                //if (index > -1)
                //    Departments[index].Employees.Add(employee);
                //else
                //    Departments[index].Employees.Add(employee);
            }
        }
    }

    [RelayCommand]
    public async Task UpdateEmployee()
    {
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
            DepartmentId = SelectedDepartment.DepartmentId
        };
        var response = await _httpClient.PutAsJsonAsync(_httpClient.BaseAddress, updatedEmployee, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = false
        });
        var json = response.Content.ReadAsStringAsync().Result;
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            Result<EmployeeModel>? data = JsonSerializer.Deserialize<Result<EmployeeModel>>(result, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
            EmployeeModel? employee = data.Data;

            if (employee != null)
            {
                Departments = await _department.GetAllDeparmentsAsync();
                //foreach (var department in Departments)
                //{
                //    if (department.Employees.Count > 0)
                //    {
                //        foreach (var emp in department.Employees)
                //        {
                //            var ph = emp.Phone;
                //            if (ph != null)
                //            {
                //                var p = Convert.TryFromBase64Chars
                //            }
                //        }
                //    }
                //}
            }
        }

    }

    [RelayCommand]
    public async Task RemoveEmployee()
    {
        if (SelectedDepartment == null && SelectedEmployee.EmployeeId == 0) return;

        var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/{SelectedEmployee.EmployeeId}");
        if (response.IsSuccessStatusCode)
        {
            Departments = await _department.GetAllDeparmentsAsync();
        }
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
        if (success)
        {
            PathPhoto = openFile.FileName;
            Photo = File.ReadAllBytes(PathPhoto);
            //Photo = PathToPhoto(PathPhoto);
        }
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
        Qualification = SelectedEmployee.Qualification;
        //var ph = Encoding.UTF8.GetBytes(SelectedEmployee.Photo ?? "");
        //var ph = 
        Photo = Convert.FromBase64String(SelectedEmployee.Photo);
        var departmentModel = new DepartmentModel();
        foreach (DepartmentModel department in Departments)
        {
            if (department.Employees.Contains(SelectedEmployee))
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
        switch (stringToValidate.Length % 4) // Pad with trailing '='s
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
