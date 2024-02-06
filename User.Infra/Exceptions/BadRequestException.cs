using System.Net;

namespace User.Infra.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(message) { }
        public override HttpStatusCode StatusCode { get => HttpStatusCode.BadRequest; }
    }
}
