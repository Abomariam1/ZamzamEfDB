using System.Windows;
using TestUi.Models;
using TestUi.ViewModels;

namespace TestUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; }
        public MainWindow()
        {
            ViewModel = new MainViewModel(new MainModel()
            {
                Id = 1,
                photo = "0xFFD8FFE12AF44578696600004D4D002A00000008000B010F0002000000070000009201100002000000110000009A011200030000000100060000011A000500000001000000AC011B000500000001000000B4012800030000000100020000013100020000003C000000BC0132000200000014000000F802130003000000010001"
            });
            DataContext = ViewModel;
            InitializeComponent();
        }
    }
}