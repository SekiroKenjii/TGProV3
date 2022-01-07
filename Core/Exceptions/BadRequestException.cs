namespace Core.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException() : base()
        {
            Errors = new List<string>();
        }
        public List<string> Errors { get; }
        public BadRequestException(params string[] errors)
            : this()
        {
            foreach (var error in errors)
            {
                Errors.Add(error);
            }
        }
    }
}
