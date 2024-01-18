namespace ZamzamApp.Services.InterFaces;

public interface IWindowEvent
{
    Action Close { get; set; }
    bool CanClose();
    void CloseWindow();
}
