using MediatR;
using Microsoft.Extensions.Logging;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Responses;
using Questao5.Domain.Builders;
using Questao5.Domain.Interfaces.DomainServices;
using Questao5.Domain.Interfaces.Repositories;
using Questao5.Domain.Querues;
using System.Net;
using static Dapper.SqlMapper;

namespace Questao5.Domain.Handlers
{
    public class SaldoContaHandler : IRequestHandler<SaldoContaQuery, SaldoContaResponse>
    {
        private readonly IContaRepository _contaRepository;
        private readonly IContaCorrenteService _contaCorrenteService;
        private readonly ILogger _logger;

        public SaldoContaHandler(IContaRepository contaRepository, IContaCorrenteService contaCorrenteService, ILoggerFactory logger)
        {
            _contaRepository = contaRepository;
            _contaCorrenteService = contaCorrenteService;
            _logger = logger.CreateLogger<SaldoContaHandler>();
        }

        public async Task<SaldoContaResponse> Handle(SaldoContaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await ValidarContaCadastrada(request.IdContaCorrente))
                    return CriarResponse(HttpStatusCode.BadRequest, "Conta corrente não localizada.", "INVALID_ACCOUNT");

                var saldo = await _contaCorrenteService.CalcularSaldoContaCorrente(request.IdContaCorrente);

                return CriarResponse(HttpStatusCode.OK, $"O saldo da conta é de R$ {saldo}", "MOVIMENTACAO_SUCESSO");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao Calcular o Saldo no SaldoContaHandler");
                throw ex;
            }
            
        }

        private async Task<bool> ValidarContaCadastrada(string idContaCorrente)
        {
            var exists = await _contaRepository.GetContaExists(idContaCorrente);
            return exists;
        }

        private SaldoContaResponse CriarResponse(HttpStatusCode statusCode, string mensagem, string tipoErro)
        {
            return new SaldoContaResponse
            {
                StatusCode = statusCode,
                Mensagem = mensagem,
                TipoErro = tipoErro
            };
        }
    }
}
