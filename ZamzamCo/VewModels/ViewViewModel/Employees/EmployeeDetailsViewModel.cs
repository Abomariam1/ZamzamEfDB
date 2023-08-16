using Prism.Commands;
using System;
using System.Collections.Generic;
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
using ZamzamCo.Commands.NavigationCommands;
using ZamzamCo.Dialogs;
using ZamzamCo.Navigations;
using ZamzamCo.Stores;

namespace ZamzamCo.VewModels.ViewViewModel
{
    public class EmployeeDetailsViewModel : ViewModelBase
    {
        private static readonly DepartmentServices DepartmentService = new(new ZamzamDbContextFactory());
        private readonly EmployeeDataServices EmployeeService = new(new ZamzamDbContextFactory());
        private ObservableCollection<Department> _departments;
        private IDialogServices _dialogServices = new DialogService();

        private int _id;
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
        private DateOnly _hireDate;
        private DateOnly _birthDate;
        private decimal _salary;
        private string _qualifications;
        private byte[] _photo;
        private ImageSource _imageSource;
        private DelegateCommand _showDepartment;
        private SelectedEmployeeStore _selectedEmployeeStore;

        public int Id { get { return _id; } set { _id = value; OnPropertyChanged(nameof(Id)); } }
        public string EmpName { get { return _empName; } set { _empName = value; OnPropertyChanged(nameof(EmpName)); } }
        public string Phone { get { return _phone; } set { _phone = value; OnPropertyChanged(nameof(Phone)); } }
        public string Address { get { return _address; } set { _address = value; OnPropertyChanged(nameof(Address)); } }
        public string City { get { return _city; } set { _city = value; OnPropertyChanged(nameof(City)); } }
        public string Region { get { return _region; } set { _region = value; OnPropertyChanged(nameof(Region)); } }
        public string? PostalCode { get { return _postalCode; } set { _postalCode = value; OnPropertyChanged(nameof(PostalCode)); } }
        public string Country { get { return _country; } set { _country = value; OnPropertyChanged(nameof(Country)); } }
        public long NID { get { return _nID; } set { _nID = value; OnPropertyChanged(nameof(NID)); } }
        public string Titel { get { return _title; } set { _title = value; OnPropertyChanged(nameof(Titel)); } }
        public DateOnly HireDate { get { return _hireDate; } set { _hireDate = value; OnPropertyChanged(nameof(HireDate)); } }
        public DateOnly BirthDate { get { return _birthDate; } set { _birthDate = value; OnPropertyChanged(nameof(BirthDate)); } }
        public decimal Salary { get { return _salary; } set { _salary = value; OnPropertyChanged(nameof(Salary)); } }
        public string Qualifications { get { return _qualifications; } set { _qualifications = value; OnPropertyChanged(nameof(Qualifications)); } }
        public byte[]? Photo { get { return _photo; } set { _photo = value; OnPropertyChanged(nameof(Photo)); } }
        public string Notify { get { return _notify; } set { _notify = value; OnPropertyChanged(nameof(Notify)); } }
        public ImageSource Image { get { return _imageSource; } set { _imageSource = value; OnPropertyChanged(nameof(Image)); } }
        public INavigator DepartmentNavigation => new SubNavigator();
        public ViewModelBase DepartmentViewModel
        {
            get { return DepartmentNavigation.CurrentViewModel; }
            set { OnPropertyChanged(nameof(DepartmentViewModel)); }
        } //;
        public bool IsModelOpen
        {
            get { return DepartmentNavigation.IsOpend; }
            set { OnPropertyChanged(nameof(IsModelOpen)); }
        }
        public IEnumerable<Department>? Departments
        {
            get { return _departments; }
            set
            {
                _departments = new ObservableCollection<Department>(collection: value);
                OnPropertyChanged(nameof(Departments));
            }
        }
        public Department SelectedDepartment { get { return _department; } set { _department = value; OnPropertyChanged(nameof(SelectedDepartment)); } }
        public bool IsSelected => SelectedEmployee != null;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployeeStore.SelectedEmployee; }
            set
            {
                _selectedEmployeeStore.SelectedEmployee = value;
                SelectedDepartment = SelectedEmployee.Department;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        //_selectedEmployeeStore.SelectedEmployee;


        #region Commands
        public ICommand? OpenDepartment => new UpdateSubViewModelCommand(DepartmentNavigation);
        public ICommand AddEmployee => new CrudComand(AddNewEmployee);
        public ICommand EditEmployee => new CrudComand(UpdateEmployee);
        public ICommand DeleteEmployee { get; }
        public ICommand AddPhoto => new IOCommand(AddNewPhoto);
        public DelegateCommand ShowDepartment => _showDepartment ?? (_showDepartment = new DelegateCommand(ExecuteShowDepartment));

        #endregion

        public EmployeeDetailsViewModel(SelectedEmployeeStore selectedEmployeeStore)
        {
            _selectedEmployeeStore = selectedEmployeeStore;
            _selectedEmployeeStore.SelectedEmployeeChanged += _selectedEmployeeStore_SelectedEmployeeChanged;
            Departments = AllDepartments();
        }


        #region Methods
        private ObservableCollection<Department> AllDepartments()
        {
            var deps = new ObservableCollection<Department>(DepartmentService.GetAll());
            return deps;
        }
        private void AddNewEmployee()
        {
            if (DepartmentService.IsEmpty())
            {
                Notify = "يجب ادخال قسم اولا";
                return;
            }

            Employee employee = FillEmployeeObject();
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
        private void _selectedEmployeeStore_SelectedEmployeeChanged()
        {
            FillProperties();
        }
        private void FillProperties()
        {
            Id = SelectedEmployee.Id;
            EmpName = SelectedEmployee.Name;
            Phone = SelectedEmployee.Phone;
            Address = SelectedEmployee.Address;
            City = SelectedEmployee.City;
            Region = SelectedEmployee.Region;
            PostalCode = SelectedEmployee.PostalCode;
            Country = SelectedEmployee.Country;
            NID = SelectedEmployee.NationalId;
            Titel = SelectedEmployee.Titel;
            HireDate = SelectedEmployee.HireDate;
            BirthDate = SelectedEmployee.BirthDate;
            Salary = SelectedEmployee.Salary;
            Qualifications = SelectedEmployee.Qualification;
            Photo = SelectedEmployee.Photo;
            SelectedDepartment = SelectedEmployee.Department;
        }
        private Employee FillEmployeeObject()
        {
            var employee = new Employee()
            {
                Name = EmpName,
                Phone = this.Phone,
                Address = Address,
                City = City,
                Region = Region,
                Country = Country,
                PostalCode = PostalCode,
                NationalId = NID,
                Titel = Titel,
                HireDate = HireDate,
                BirthDate = BirthDate,
                Salary = Salary,
                Qualification = Qualifications,
                Photo = Photo,
                DepartmentId = SelectedDepartment.Id
            };
            return employee;
        }
        protected override void Dispose()
        {
            _selectedEmployeeStore.SelectedEmployeeChanged -= _selectedEmployeeStore_SelectedEmployeeChanged;
            base.Dispose();
        }
        private void ExecuteShowDepartment()
        {
            _dialogServices.ShowDialog<MinDepartmentsViewModel>(result =>
            {
                var test = result;
            });
        }
        private void UpdateEmployee()
        {
            if (SelectedEmployee != null && SelectedDepartment != null)
            {
                var emp = FillEmployeeObject();
                //emp.Id = SelectedEmployee.Id;
                var up = EmployeeService.Update(SelectedEmployee.Id, emp);
                if (up != null)
                {
                    Notify = $"Employee {emp.Name} Updated";
                }
                else
                {
                    Notify = "Error";
                }
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
                Photo = reader.ReadBytes((int)fs.Length - 1);

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
        #endregion
    }
}
