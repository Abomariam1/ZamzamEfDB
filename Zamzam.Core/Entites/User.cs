namespace Zamzam.Core.Entites
{
    public class User : BaseEntity
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; } = string.Empty;
        public required string Phone { get; set; } = string.Empty;

    }
}
