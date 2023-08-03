using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Zamzam.Core;
using Zamzam.EF;
using ZamzamCo.Commands;
using ZamzamCo.Commands.CrudCommands;

namespace ZamzamCo.VewModels.ViewViewModel
{
    public class EmployeeViewModel : ViewModelBase
    {
        private static readonly IDataService<Department> DepartmentService = new GenericDataServices<Department>(new ZamzamDbContextFactory());
        private static readonly IDataService<Employee> EmployeeService = new GenericDataServices<Employee>(new ZamzamDbContextFactory());

        private string _empName;
        private string _phone;
        private string _address;
        private string _city;
        private string _region;
        private string _postalCode;
        private string _country;
        private long _nID;
        private Department _department;
        private string _title;
        private DateOnly _hireDate;
        private DateOnly _birthDate;
        private decimal _salary;
        private string _qualifications;
        private byte[] _photo;
        private ImageSource _imageSource;


        public static IEnumerable<Department> Departments { get; set; } = DepartmentService.GetAll();
        public string EmpName { get { return _empName; } set { _empName = value; OnPropertyChanged(nameof(EmpName)); } }
        public string Phone { get { return _phone; } set { _phone = value; OnPropertyChanged(nameof(Phone)); } }
        public string Address { get { return _address; } set { _address = value; OnPropertyChanged(nameof(Address)); } }
        public string City { get { return _city; } set { _city = value; OnPropertyChanged(nameof(City)); } }
        public string Region { get { return _region; } set { _region = value; OnPropertyChanged(nameof(Region)); } }
        public string? PostalCode { get { return _postalCode; } set { _postalCode = value; OnPropertyChanged(nameof(PostalCode)); } }
        public string Country { get { return _country; } set { _country = value; OnPropertyChanged(nameof(Country)); } }
        public long NID { get { return _nID; } set { _nID = value; OnPropertyChanged(nameof(NID)); } }
        public Department SelectedDepartment { get { return _department; } set { _department = value; OnPropertyChanged(nameof(SelectedDepartment)); } }
        public string Titel { get { return _title; } set { _title = value; OnPropertyChanged(nameof(Titel)); } }
        public DateOnly HireDate { get { return _hireDate; } set { _hireDate = value; OnPropertyChanged(nameof(HireDate)); } }
        public DateOnly BirthDate { get { return _birthDate; } set { _birthDate = value; OnPropertyChanged(nameof(BirthDate)); } }
        public decimal Salary { get { return _salary; } set { _salary = value; OnPropertyChanged(nameof(Salary)); } }
        public string Qualifications { get { return _qualifications; } set { _qualifications = value; OnPropertyChanged(nameof(Qualifications)); } }
        public byte[]? Photo { get { return _photo; } set { _photo = value; OnPropertyChanged(nameof(Photo)); } }
        public ImageSource Image { get { return _imageSource; } set { _imageSource = value; OnPropertyChanged(nameof(Image)); } }
        public INavigator SubNavigation { get; set; } = new NavigatDepartment();
        public ICommand AddEmployee => new CrudComand(AddNewEmployee);
        public ICommand EditEmployee { get; }
        public ICommand DeleteEmployee { get; }
        public ICommand AddDepartmentCommand => new DepartmentCommand(SubNavigation);
        public ICommand AddPhoto => new IOCommand(AddNewPhoto);
        public static ICommand? AddDepartment => MinDepartments.AddDeparmentCommand;

        public static MinDepartmentsViewModel MinDepartments => new();

        //public static EmployeeViewModel SelectedEmployee = new EmployeeViewModel()
        //{
        //    EmpName = "Mosataf",
        //    Phone = "01066686997",
        //    City = "Kafr Sakr",
        //    Region = "Asharkia",
        //    Country = "Misr",
        //    BirthDate = new DateOnly(1985, 09, 28),
        //    HireDate = new DateOnly(2023, 7, 1),
        //    NID = 28509281301179,
        //    PostalCode = "44755",
        //    Salary = 60000,
        //    Titel = "Account Manager"
        //};
        public EmployeeViewModel()
        {

        }

        private void AddNewEmployee()
        {
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
            employee.HireDate = HireDate;
            employee.BirthDate = BirthDate;
            employee.Salary = Salary;
            employee.Qualification = Qualifications;
            employee.Photo = Photo;
            employee.DepartmentId = SelectedDepartment.Id;
            var emp = EmployeeService.Create(employee);
        }

        private void AddNewPhoto()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "الصور";

            openFile.Filter = "Photo Jpeg|*.jpeg|Photo png|*.png";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string fname = openFile.FileName;
                FileStream fs = new FileStream(fname, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new(fs);
                MemoryStream ms = new MemoryStream(reader.ReadBytes((int)fs.Length - 1));
                Image = new BitmapImage(new Uri(fname));
                Photo = reader.ReadBytes((int)fs.Length);
            }
        }
        public ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }
    }
}

//_selectedEmployee = new()

