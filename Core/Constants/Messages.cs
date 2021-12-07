﻿namespace Core.Constants
{
    public static class Messages
    {
        public const string INCORRECT_EMAIL = "Email Is Incorrect";
        public const string INCORRECT_PASSWORD = "Password Is Incorrect";
        public const string REVOKED_TOKEN = "Token Has Been Revoked";
        public const string USER_NOT_ACTIVE = "User Has Not Activated";
        public const string LOCKED_USER = "User Has Been Locked";

        public const string ADD_FAILURE = "Failed To Add New Resource";
        public const string DELETE_FAILURE = "Failed To Delete New Resource";
        public const string UPDATE_FAILURE = "Failed To Update New Resource";

        public const string ADD_SUCCESS = "Add New Resource Successfully";
        public const string UPDATE_SUCCESS = "Update Requested Resource Successfully";
        public const string DELETE_SUCCESS = "Delete Requested Resource Successfully";

        public static string RESOURCE_NOTFOUND(string entity) => $"{entity} Not Found";
    }
}
