using MediatR;
using Questao5.Application.Responses;

namespace Questao5.Domain.Querues
{
    public class SaldoContaQuery : IRequest<SaldoContaResponse>
    {
        public string IdContaCorrente { get; set; }
    }
}
