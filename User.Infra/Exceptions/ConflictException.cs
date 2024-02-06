using System.Net;

namespace User.Infra.Exceptions
{
    public class ConflictException : BaseException
    {
        public ConflictException(string message) : base(message) { }
        public override HttpStatusCode StatusCode { get => HttpStatusCode.Conflict; }
    }
}