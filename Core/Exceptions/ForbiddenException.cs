namespace Core.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base()
        {
            Errors = new List<string>();
        }
        public List<string> Errors { get; }
        public ForbiddenException(params string[] errors)
            : this()
        {
            foreach (var error in errors)
            {
                Errors.Add(error);
            }
        }
    }
}
