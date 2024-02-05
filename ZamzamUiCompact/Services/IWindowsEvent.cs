namespace ZamzamUiCompact.Services;
public interface IWindowEvent
{
    Action Close { get; set; }
    bool CanClose();
    void CloseWindow();
}
