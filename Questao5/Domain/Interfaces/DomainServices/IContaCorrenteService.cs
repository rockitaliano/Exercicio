namespace Questao5.Domain.Interfaces.DomainServices
{
    public interface IContaCorrenteService
    {
        Task<decimal> CalcularSaldoContaCorrente(string idContaCorrente);
    }
}
