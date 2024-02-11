﻿using System.ComponentModel.DataAnnotations;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class AreaViewModel : ObservableValidator
{
    AreaService _areaService;

    [ObservableProperty] int _id;
    [Required]
    [MaxLength(100)]
    [MinLength(2)]
    [ObservableProperty] string _name = String.Empty;

    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    [ObservableProperty] string _station = String.Empty;

    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    [ObservableProperty] string _location = String.Empty;

    [Required]
    [ObservableProperty] EmployeeModel _employee;

    [ObservableProperty] ObservableCollection<EmployeeModel> _employees;

    [ObservableProperty] AreaModel _area;
    [ObservableProperty] ObservableCollection<AreaModel> _areas;

    [ObservableProperty] string _messages;
    public AreaViewModel(AreaService areaService)
    {
        _areaService = areaService;
        var tt = Task.Run(_areaService.GetEmployees).GetAwaiter().GetResult();
        Task.Run(GetAreas).Wait();
        Employees = new ObservableCollection<EmployeeModel>(tt);
    }

    [RelayCommand]
    public async Task AddArea()
    {
        bool isNull = string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Station) || Employee is null;
        if (isNull) return;
        AreaModel area = new()
        {
            AreaId = Id,
            AreaName = Name,
            Station = Station,
            Location = Location,
            CollectorId = Employee.EmployeeId
        };
        Result<AreaModel>? response = await _areaService.Add(area);
        if (response.Succeeded)
        {
            Area = response.Data;
            Areas.Add(Area);
            Messages = response.Message;
        }
        else
            Messages = response.Message;


    }

    [RelayCommand]
    public async Task UpdateArea()
    {
        if (Area is null)
        {
            Messages = "يجب اختيار منطقة للتعديل";
            return;
        }
        var toUpdate = new AreaModel
        {
            AreaId = Area.AreaId,
            AreaName = Name,
            Station = Station,
            Location = Location,
            CollectorId = Employee.EmployeeId
        };
        Result<AreaModel>? response = await _areaService.Update(toUpdate);
        if (response.Succeeded)
        {
            Area = response.Data;
            int index = Areas.IndexOf(Areas.SingleOrDefault(x => x.AreaId == Area.AreaId)!);
            if (index >= 0)
            {
                Areas[index] = Area;
            }
            Messages = response.Message;
        }
        else
        {
            Messages = response.Message;
        }
    }

    [RelayCommand]
    public async Task DeleteArea()
    {
        Result<int>? response = await _areaService.Delete(Area.AreaId);
        if (response.Succeeded)
        {
            Areas.Remove(Area);
        }
    }

    [RelayCommand]
    public void FillText()
    {
        if (Area is null) return;
        Name = Area.AreaName;
        Station = Area.Station;
        Location = Area.Location;
        Employee = Employees.FirstOrDefault(x => x.EmployeeId == Area.CollectorId)!;
    }

    public async Task GetAreas()
    {
        var areas = (List<AreaModel>)await _areaService.GetAreas();
        Areas = new ObservableCollection<AreaModel>(areas);
    }
}
