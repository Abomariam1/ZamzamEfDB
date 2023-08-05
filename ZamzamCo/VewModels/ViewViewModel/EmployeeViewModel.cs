using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Zamzam.Core;
using Zamzam.EF;
using Zamzam.EF.Services;
using ZamzamCo.Commands;
using ZamzamCo.Commands.CrudCommands;
using ZamzamCo.Dialogs;

namespace ZamzamCo.VewModels.ViewViewModel
{
    public class EmployeeViewModel : ViewModelBase
    {
        private static readonly DepartmentServices DepartmentService = new DepartmentServices(new ZamzamDbContextFactory());
        private readonly EmployeeDataServices EmployeeService = new(new ZamzamDbContextFactory());
        private IDialogServices _dialogServices = new DialogService();
        private string _empName;
        private string _phone;
        private string _address;
        private string _city;
        private string _region;
        private string _postalCode;
        private string _country;
        private string _notify;
        private long _nID;
        private Department _department;
        private string _title;
        private DateTime _hireDate = DateTime.Now;
        private DateTime _birthDate = DateTime.Now;
        private decimal _salary;
        private string _qualifications;
        private byte[] _photo;
        private ImageSource _imageSource;
        private EmployeeViewModel _employee;
        private DelegateCommand _showDepartment;
        public string EmpName { get { return _empName; } set { _empName = value; OnPropertyChanged(nameof(EmpName)); } }
        public string Phone { get { return _phone; } set { _phone = value; OnPropertyChanged(nameof(Phone)); } }
        public string Address { get { return _address; } set { _address = value; OnPropertyChanged(nameof(Address)); } }
        public string City { get { return _city; } set { _city = value; OnPropertyChanged(nameof(City)); } }
        public string Region { get { return _region; } set { _region = value; OnPropertyChanged(nameof(Region)); } }
        public string? PostalCode { get { return _postalCode; } set { _postalCode = value; OnPropertyChanged(nameof(PostalCode)); } }
        public string Country { get { return _country; } set { _country = value; OnPropertyChanged(nameof(Country)); } }
        public long NID { get { return _nID; } set { _nID = value; OnPropertyChanged(nameof(NID)); } }
        public string Titel { get { return _title; } set { _title = value; OnPropertyChanged(nameof(Titel)); } }
        public DateTime HireDate { get { return _hireDate; } set { _hireDate = value; OnPropertyChanged(nameof(HireDate)); } }
        public DateTime BirthDate { get { return _birthDate; } set { _birthDate = value; OnPropertyChanged(nameof(BirthDate)); } }
        public decimal Salary { get { return _salary; } set { _salary = value; OnPropertyChanged(nameof(Salary)); } }
        public string Qualifications { get { return _qualifications; } set { _qualifications = value; OnPropertyChanged(nameof(Qualifications)); } }
        public byte[]? Photo { get { return _photo; } set { _photo = value; OnPropertyChanged(nameof(Photo)); } }
        public string Notify { get { return _notify; } set { _notify = value; OnPropertyChanged(nameof(Notify)); } }
        public ImageSource Image { get { return _imageSource; } set { _imageSource = value; OnPropertyChanged(nameof(Image)); } }
        public INavigator SubNavigation { get; set; } = new NavigatDepartment();
        public Department SelectedDepartment { get { return _department; } set { _department = value; OnPropertyChanged(nameof(SelectedDepartment)); } }
        public static ObservableCollection<Department>? Departments => new(DepartmentService.GetDepartments());
        public EmployeeViewModel Employee { get { return _employee; } set { _employee = value; OnPropertyChanged(nameof(Employee)); } }
        public ICommand ShowEmployee => new IOCommand(Show);
        public ICommand AddEmployee => new CrudComand(AddNewEmployee);
        public ICommand EditEmployee { get; }
        public ICommand DeleteEmployee { get; }
        public ICommand AddDepartmentCommand => new DepartmentCommand(SubNavigation);
        public ICommand AddPhoto => new IOCommand(AddNewPhoto);
        public DelegateCommand ShowDepartment => _showDepartment ?? (_showDepartment = new DelegateCommand(ExecuteShowDepartment));


        public static ICommand? AddDepartment => MinDepartments.AddDeparmentCommand;

        public static MinDepartmentsViewModel MinDepartments => new();

        public EmployeeViewModel()
        {

        }
        private void Show()
        {
            if (Employee != null)
            {
                EmpName = Employee.EmpName;
                Phone = Employee.Phone;
                Address = Employee.Address;
                City = Employee.City;
                Region = Employee.Region;
                Country = Employee.Country;
                NID = Employee.NID;
                Titel = Employee.Titel;
                HireDate = Employee.HireDate;
                BirthDate = Employee.BirthDate;
                Salary = Employee.Salary;
                Qualifications = Employee.Qualifications;
                Photo = Employee.Photo;
            }
        }
        private void ExecuteShowDepartment()
        {
            _dialogServices.ShowDialog();
        }

        private void AddNewEmployee()
        {
            if (DepartmentService.IsEmpty())
            {
                Notify = "يجب ادخال قسم اولا";
                return;
            }


            Employee employee = new();
            employee.Name = EmpName;
            employee.Phone = Phone;
            employee.City = City;
            employee.Address = Address;
            employee.Region = Region;
            employee.PostalCode = PostalCode;
            employee.Country = Country;
            employee.NationalId = NID;
            employee.Titel = Titel;
            employee.HireDate = DateOnly.FromDateTime(HireDate);
            employee.BirthDate = DateOnly.FromDateTime(BirthDate);
            employee.Salary = Salary;
            employee.Qualification = Qualifications;
            employee.Photo = Photo;
            employee.DepartmentId = SelectedDepartment.Id;
            var emp = EmployeeService.Create(employee);
            if (emp != null)
            {
                Notify = "تمت العملية بنجاح";
            }
            else
            {
                Notify = "خطاء";
            }
        }

        private void AddNewPhoto()
        {
            OpenFileDialog openFile = new()
            {
                Title = "الصور",

                Filter = "Photo Jpeg|*.jpeg|Photo png|*.png"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string fname = openFile.FileName;
                FileStream fs = new(fname, FileMode.Open, FileAccess.ReadWrite);
                BinaryReader reader = new(fs);
                MemoryStream ms = new(reader.ReadBytes((int)fs.Length - 1));
                //Image = new BitmapImage(new Uri(fname));

                Photo = ms.ToArray();
                Image = ByteToImage(Photo);
            }
        }
        public ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new();
            MemoryStream ms = new(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }
    }
}

//_selectedEmployee = new()

