namespace ZamzamUiCompact.Services;

public class AreaService
{
    private readonly HttpClient _httpClient;
    private readonly IAuthenticatedUser _user;
    const string _areacontroller = "Areas";
    const string _employeeController = "Employee";
    public AreaService(HttpClient httpClient, IAuthenticatedUser user)
    {
        _httpClient = httpClient;
        _user = user;
        _httpClient.DefaultRequestHeaders.Add("Authorization", user.Token);
    }

    public async Task<Result<AreaModel>> Add(AreaModel area)
    {
        HttpResponseMessage? response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/{_areacontroller}", area);
        if (!response.IsSuccessStatusCode)
        {
            return new Result<AreaModel>()
            {
                Succeeded = false,
                Message = "فشل في اضافة منطقة"
            };
        }
        string? stringResult = await response.Content.ReadAsStringAsync();
        Result<AreaModel>? result = JsonSerializer.Deserialize<Result<AreaModel>>(stringResult, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return result;
    }

    public async Task<Result<AreaModel>> Update(AreaModel area)
    {
        HttpResponseMessage? response = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}/{_areacontroller}", area, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = false
        });
        if (!response.IsSuccessStatusCode)
        {
            return new Result<AreaModel>
            {
                Succeeded = false,
                Message = "فشل في تعديل المنطقة"
            };
        }
        string? stringResult = await response.Content.ReadAsStringAsync();
        Result<AreaModel>? result = JsonSerializer.Deserialize<Result<AreaModel>>(stringResult, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        if (result.Succeeded)
        {
            return result;
        }
        else
        {
            return new Result<AreaModel>
            {
                Succeeded = false,
                Message = "فشل في تعديل المنطقة"
            };
        }

    }

    public async Task<Result<int>> Delete(int id)
    {
        HttpResponseMessage? response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/{_areacontroller}/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return new Result<int> { Succeeded = false, Message = "فشل في حذف المنطقة" };
        }
        string? content = await response.Content.ReadAsStringAsync();
        Result<int>? result = JsonSerializer.Deserialize<Result<int>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return result;
    }

    public async Task<IList<EmployeeModel>> GetEmployees()
    {
        Result<List<EmployeeModel>>? response =
            await _httpClient.GetFromJsonAsync<Result<List<EmployeeModel>>>($"{_httpClient.BaseAddress}/{_employeeController}/getall");
        var employees = response.Data;
        return employees;
    }

    public async Task<IList<AreaModel>> GetAreas()
    {
        Result<List<AreaModel>>? response =
            await _httpClient.GetFromJsonAsync<Result<List<AreaModel>>>($"{_httpClient.BaseAddress}/{_areacontroller}");
        if (response == null) return new List<AreaModel>();
        List<AreaModel>? areas = response.Data;
        return areas;
    }
}
