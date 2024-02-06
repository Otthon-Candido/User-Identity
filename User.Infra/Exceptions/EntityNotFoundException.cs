using System.Net;

namespace User.Infra.Exceptions
{
    public class EntityNotFoundException : BaseException
    {
        public EntityNotFoundException(string message) : base(message) { }
        public override HttpStatusCode StatusCode { get => HttpStatusCode.NotFound; }
    }
}
