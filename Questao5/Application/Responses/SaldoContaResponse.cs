using System.Net;

namespace Questao5.Application.Responses
{
    public class SaldoContaResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Mensagem { get; set; }
        public string TipoErro { get; set; }
    }
}
