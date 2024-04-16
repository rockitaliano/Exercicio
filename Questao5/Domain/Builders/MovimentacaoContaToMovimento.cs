using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace Questao5.Domain.Builders
{
    public static class MovimentacaoContaToMovimento
    {
        public static Movimento BuilderEntity(MovimentacaoContaCommand command)
        {
            return new Movimento(command.IdRequisicao.ToString(), command.IdContaCorrente, DateTime.Now, command.TipoMovimento, command.Valor);
            
        }
    }
}
