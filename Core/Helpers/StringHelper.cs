namespace Core.Helpers
{
    public static class StringHelper
    {
        public static string GeneratePhotoCaption(this string productName, int sortOrder)
        {
            return productName.ToLower().Replace(" ", "-") + "-photo-" + sortOrder;
        }
    }
}
