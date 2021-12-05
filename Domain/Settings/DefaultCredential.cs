namespace Domain.Settings
{
    public class DefaultCredential
    {
        public Guid AdminId { get; set; }
        public Guid AdminRoleId { get; set; }
        public string? DefaultPassword { get; set; }
    }
}
