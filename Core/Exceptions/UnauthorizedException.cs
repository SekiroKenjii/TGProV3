namespace Core.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base()
        {
            Errors = new List<string>();
        }
        public List<string> Errors { get; }
        public UnauthorizedException(params string[] errors)
            : this()
        {
            foreach (var error in errors)
            {
                Errors.Add(error);
            }
        }
    }
}
