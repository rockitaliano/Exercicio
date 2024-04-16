using MediatR;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentacaoContaCommand : IRequest<MovimentacaoContaResponse>
    {
        public string IdRequisicao { get; private set; }
        public string IdContaCorrente { get; set; }
        public decimal Valor { get; set; }
        public string TipoMovimento { get; set; }

        public void SetIdRequisicao()
        {
            IdRequisicao = Guid.NewGuid().ToString();
        }
    }
}
