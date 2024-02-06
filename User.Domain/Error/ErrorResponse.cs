namespace User.Domain.Model
{
    public class ErrorResponse : DefaultResultResponse
    {
        public ErrorResponse()
        {
            Success = false;
        }
    }
}
