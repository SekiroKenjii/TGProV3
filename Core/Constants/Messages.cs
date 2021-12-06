namespace Core.Constants
{
    public static class Messages
    {
        public const string INCORRECT_EMAIL = "Email Is Incorrect";
        public const string INCORRECT_PASSWORD = "Password Is Incorrect";
        public const string REVOKED_TOKEN = "Token Has Been Revoked";
        public const string USER_NOT_ACTIVE = "User Has Not Activated";
        public const string LOCKED_USER = "User Has Been Locked";
        public static string RESOURCE_NOTFOUND(string entity) => $"{entity} Not Found";
    }
}
