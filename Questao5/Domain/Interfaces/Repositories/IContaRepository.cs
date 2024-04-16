using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces.Repositories
{
    public interface IContaRepository
    {
        Task<ContaCorrente> GetById(Guid id);
        Task<int> Save(Movimento movimento);
        Task<bool> GetContaAtiva(string idConta);
        Task<bool> GetContaExists(string idConta);
        Task<decimal?> GetSaldo(string idConta);
        Task<ContaCorrente> GetContaCorrente(string idConta);
        Task<decimal?> GetTotalCredito(string idConta);
        Task<decimal?> GetTotalDebito(string idConta);
    }
}
