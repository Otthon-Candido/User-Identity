using System.Net;

namespace User.Infra.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string message) : base(message) { }

        public abstract HttpStatusCode StatusCode { get; }
    }
}
