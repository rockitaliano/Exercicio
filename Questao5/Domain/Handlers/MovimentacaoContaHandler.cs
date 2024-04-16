using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Builders;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Handlers;
using Questao5.Domain.Interfaces.DomainServices;
using Questao5.Domain.Interfaces.Repositories;
using Questao5.Domain.Shared;
using Questao5.Domain.ValueObjects;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using static Dapper.SqlMapper;

namespace Questao5.Application.Handlers
{
    public class MovimentacaoContaHandler : IRequestHandler<MovimentacaoContaCommand, MovimentacaoContaResponse>
    {
        private readonly IContaRepository _contaRepository;
        private readonly IContaCorrenteService _contaCorrenteService;
        private readonly ILogger _logger;

        public MovimentacaoContaHandler(IContaRepository contaRepository, IContaCorrenteService contaCorrenteService, ILoggerFactory logger)
        {
            _contaRepository = contaRepository;
            _contaCorrenteService = contaCorrenteService;
            _logger = logger.CreateLogger<MovimentacaoContaHandler>();
        }

        public async Task<MovimentacaoContaResponse> Handle(MovimentacaoContaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await ValidarContaCadastrada(request.IdContaCorrente))
                    return CriarResponseFail(HttpStatusCode.BadRequest, "Apenas contas correntes cadastradas podem receber movimentação.", "INVALID_ACCOUNT");

                if (!await ValidarContaAtiva(request.IdContaCorrente.ToString()))
                    return CriarResponseFail(HttpStatusCode.BadRequest, "Apenas contas correntes ativas podem receber movimentação.", "INACTIVE_ACCOUNT");

                if (!ValidarValorPositivo(request.Valor))
                    return CriarResponseFail(HttpStatusCode.BadRequest, "Apenas valores positivos podem ser recebidos.", "INVALID_VALUE");

                if (!ValidarTipoMovimento(request.TipoMovimento))
                    return CriarResponseFail(HttpStatusCode.BadRequest, "Apenas os tipos 'débito' ou 'crédito' podem ser aceitos.", "INVALID_TYPE");

                var entity = MovimentacaoContaToMovimento.BuilderEntity(request);

                await _contaRepository.Save(entity);

                var conta = await _contaRepository.GetContaCorrente(entity.IdContaCorrente);

                var saldo = await _contaCorrenteService.CalcularSaldoContaCorrente(entity.IdContaCorrente);

                return CriarResponse(HttpStatusCode.OK, "Movimentação da conta corrente realizada com sucesso.", "MOVIMENTACAO_SUCESSO", conta, saldo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu no processo de movimentação de contana no MovimentacaoContaResponse");
                throw ex;
            }
            
        }

        private async Task<bool> ValidarContaCadastrada(string idContaCorrente)
        {
            var exists = await _contaRepository.GetContaExists(idContaCorrente);
            return exists;
        }

        private async Task<bool> ValidarContaAtiva(string idContaCorrente)
        {
            var isActive = await _contaRepository.GetContaAtiva(idContaCorrente);
            return isActive;
        }

        private bool ValidarValorPositivo(decimal valor)
            => valor > 0;

        private bool ValidarTipoMovimento(string tipoMovimento)
        {
            if (tipoMovimento != "C" && tipoMovimento != "D")
                return false;

            return true;
        }
           

        private MovimentacaoContaResponse CriarResponseFail(HttpStatusCode statusCode, string mensagem, string tipoErro)
        {
            return new MovimentacaoContaResponse
            {
                StatusCode = statusCode,
                Mensagem = mensagem,
                TipoErro = tipoErro,
            };
        }

        private MovimentacaoContaResponse CriarResponse(HttpStatusCode statusCode, string mensagem, string tipoErro, ContaCorrente contaCorrente = null, decimal? saldo = null)     
        {
            return new MovimentacaoContaResponse
            {
                StatusCode = statusCode,
                Mensagem = mensagem,
                TipoErro = tipoErro,
                DataConsulta = DateTime.Now,
                Nome = contaCorrente.Nome,
                NumeroConta = contaCorrente.Numero,
                SaltoAtual = saldo.Value
            };
        }

       
    }
}
