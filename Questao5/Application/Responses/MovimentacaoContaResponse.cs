using Questao5.Domain.ValueObjects;
using System.Net;

public class MovimentacaoContaResponse : DadosConta
{
    public HttpStatusCode StatusCode { get; set; }
    public string Mensagem { get; set; }
    public string TipoErro { get; set; }
}