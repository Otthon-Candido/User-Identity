using System.Net;

namespace User.Infra.Exceptions
{
    public class InternalException : BaseException
    {
        public InternalException(string message) : base(message) { }
        public override HttpStatusCode StatusCode { get => HttpStatusCode.InternalServerError; }
    }
}