using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.IO;
using TestUi.Models;

namespace TestUi.ViewModels;

public partial class MainViewModel(MainModel model) : ObservableObject
{
    private MainModel _model = model;

    [ObservableProperty] byte[] _photo = [];

    [ObservableProperty] string _stringPhoto = "";

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
            var PathPhoto = openFile.FileName;
            Photo = File.ReadAllBytes(PathPhoto);
            _model.photo = Convert.ToBase64String(Photo ?? []);
        }
    }

    [RelayCommand]
    public void RemovePhoto()
    {
        Photo = [];
    }

    [RelayCommand]
    public void FillPhoto()
    {
        Photo = Convert.FromBase64String(_model.photo ?? "");
    }
}
