using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces.DomainServices;
using Questao5.Domain.Interfaces.Repositories;
using Questao5.Domain.Shared;

namespace Questao5.Domain.DomainServices
{
    public class ContaCorrenteService : IContaCorrenteService
    {
        private readonly IContaRepository _contaRepository;

        public ContaCorrenteService(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<decimal> CalcularSaldoContaCorrente(string idContaCorrente)
        {
            var totalCredito = await _contaRepository.GetTotalCredito(idContaCorrente);

            var totalDebito = await _contaRepository.GetTotalDebito(idContaCorrente);

            var saldo = CalculosContaCorrente.CalcularSaldoConta(totalCredito.Value, totalDebito.Value);

            return saldo;
        }
    }
}
