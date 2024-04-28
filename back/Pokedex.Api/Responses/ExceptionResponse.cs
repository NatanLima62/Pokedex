using System.Net;

namespace Pokedex.Api.Responses;

public class ExceptionResponse : Response
{
    protected ExceptionResponse()
    {
        Title = "Ops, a server error occurred";
        Status = (int)HttpStatusCode.InternalServerError;
    }

    protected ExceptionResponse(string title) : this()
    {
        Title = title;
    }
}