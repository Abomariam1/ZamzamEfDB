using ZamzamUiCompact.Models.InterFaces;

namespace ZamzamUiCompact.Models
{
    public class Model : IModel
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
    }
}
