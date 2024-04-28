using System.Net;
using System.Text.Json.Serialization;

namespace Pokedex.Api.Responses;

public class BadRequestResponse : Response
{
    public BadRequestResponse()
    {
        Title = "One or more validation errors occurred!";
        Status = (int)HttpStatusCode.BadRequest;
    }

    public BadRequestResponse(List<string>? erros) : this()
    {
        Erros = erros ?? new List<string>();
    }

    [JsonPropertyOrder(order: 3)]
    public List<string>? Erros { get; private set; }
}