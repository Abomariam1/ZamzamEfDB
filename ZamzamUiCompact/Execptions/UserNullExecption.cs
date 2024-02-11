namespace ZamzamUiCompact.Execptions;

public class UserNullExecption : Exception
{
    public string[] Errors { get; set; } = [];
    public UserNullExecption() : base() { }
    public UserNullExecption(string message) : base(message) { }
    public UserNullExecption(string message, Exception innerException)
            : base(message, innerException) { }
    public UserNullExecption(string[] errors) : base("User Is Null.Pleaze attach user")
    {
        Errors = errors;
    }
}
