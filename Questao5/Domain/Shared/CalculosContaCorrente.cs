namespace Questao5.Domain.Shared
{
    public static class CalculosContaCorrente
    {
        public static decimal CalcularSaldoConta(decimal credito, decimal debito) 
            => credito - debito;
    }
}
