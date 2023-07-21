using System;
using System.Windows.Input;
using ZamzamCo.VewModels.ViewViewModel;

namespace ZamzamCo.VewModels
{
    public class UpdateViewModel : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private INavigator _navigation;
        public UpdateViewModel(INavigator navigator)
        {
            _navigation = navigator;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ViewType)
            {
                ViewType type = (ViewType)parameter;
                switch (type)
                {
                    case ViewType.Home:
                        _navigation.CurrentViewModel = new HomeViewModel();
                        break;
                    case ViewType.Items:
                        _navigation.CurrentViewModel = new ItemViewModel();
                        break;
                    case ViewType.Sales:
                        _navigation.CurrentViewModel = new SaleViewModel();
                        break;
                    case ViewType.Installments:
                        _navigation.CurrentViewModel = new InstallmentViewModel();
                        break;
                    case ViewType.Customers:
                        _navigation.CurrentViewModel = new CustomerViewModel();
                        break;
                    case ViewType.Employee:
                        _navigation.CurrentViewModel = new EmployeeViewModel();
                        break;
                    case ViewType.Maintenance:
                        _navigation.CurrentViewModel = new MaintenanceViewModel();
                        break;
                    case ViewType.Purchases:
                        _navigation.CurrentViewModel = new PurchasesViewModel();
                        break;
                    case ViewType.Supliers:
                        _navigation.CurrentViewModel = new SuplierViewModel();
                        break;
                    case ViewType.About:
                        _navigation.CurrentViewModel = new AboutViewModel();
                        break;
                }
            }
        }
    }
}
