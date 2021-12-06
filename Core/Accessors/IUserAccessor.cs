namespace Core.Accessors
{
    public interface IUserAccessor
    {
        Guid GetUserId();
        string GetUserEmail();
    }
}
