namespace Zamzam.Shared.Interfaces
{
    public interface IResult<T>
    {
        string Message { get; set; }
        bool Succeeded { get; set; }
        T Data { get; set; }
        Exception Exception { get; set; }
        int Code { get; set; }
    }
}
