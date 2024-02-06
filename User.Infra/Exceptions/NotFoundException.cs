using System.Net;

namespace User.Infra.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message) { }
        public override HttpStatusCode StatusCode { get => HttpStatusCode.NotFound; }
    }
}
