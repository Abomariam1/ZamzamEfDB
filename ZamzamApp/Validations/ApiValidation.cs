using System.Text.Json;

namespace ZamzamApp.Validations;

public class ApiValidation
{
    public static Dictionary<string, List<string>> Validate(string body)
    {
        Dictionary<string, List<string>>? respons = new Dictionary<string, List<string>>();
        JsonElement jsonElemnt = JsonSerializer.Deserialize<JsonElement>(body);
        switch (jsonElemnt.ValueKind)
        {
            case JsonValueKind.Object:
                JsonElement errorJsonElemnt = jsonElemnt.GetProperty("errors");
                foreach (JsonProperty elemen in errorJsonElemnt.EnumerateObject())
                {
                    string? field = elemen.Name;
                    List<string>? errors = new List<string>();
                    foreach (JsonElement errorKind in elemen.Value.EnumerateArray())
                    {
                        errors.Add(errorKind.GetString()!);
                    }
                    respons.Add(field, errors);
                }
                break;
            case JsonValueKind.String:
                string? errorJsonstring = jsonElemnt.GetString();
                List<string>? stringError = new List<string>();
                stringError.Add(errorJsonstring);
                respons.Add("Error", stringError);
                break;
            default:
                break;
        }
        return respons;
    }
}
