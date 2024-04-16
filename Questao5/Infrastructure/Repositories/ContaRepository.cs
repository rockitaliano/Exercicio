using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces.Repositories;
using Questao5.Infrastructure.Database;

namespace Questao5.Infrastructure.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly DataContext _context;

        public ContaRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ContaCorrente> GetById(Guid id)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var result = await sqlConn.QueryFirstOrDefaultAsync<ContaCorrente>(
                    @"  SELECT 
	                        L.ID AS Id,
	                        C.NOME AS CustomerName,
	                        F.NOME AS MovieName,
	                        L.DT_LOCACAO AS LocationDate,
	                        L.DT_DEVOLUCAO AS DevolutionDate
                        FROM tb_Locacoes L
                        INNER JOIN tb_Cliente C
	                        ON L.ID_CLIENTE = C.ID
                        INNER JOIN tb_Filme F
	                        ON L.ID_FILME = F.ID
                        WHERE L.ID = @id",
                        new { @id = id });

                    return await Task.FromResult(result);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> Save(Movimento movimento)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var id = await sqlConn.ExecuteScalarAsync(
                    @"INSERT INTO [dbo].[movimento] VALUES (@idmovimento, @idcontacorrente, @datamovimento, @tipomovimento, @valor)
                    SELECT SCOPE_IDENTITY()",
                        new
                        {
                            @idmovimento = movimento.IdMovimento,
                            @idcontacorrente = movimento.IdContaCorrente,
                            @datamovimento = movimento.DataMovimento,
                            @tipomovimento = movimento.TipoMovimento,
                            @valor = movimento.Valor
                        });

                    return Convert.ToInt32(id);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> GetContaAtiva(string idConta)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var result = await sqlConn.ExecuteScalarAsync<bool>(
                    @"SELECT 1 FROM [dbo].[contacorrente] WHERE @idConta = idcontacorrente AND ativo = 1",
                        new { @idConta = idConta });

                    return await Task.FromResult(result);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> GetContaExists(string idConta)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var result = await sqlConn.ExecuteScalarAsync<bool>(
                    @"SELECT 1 FROM [dbo].[contacorrente] WHERE @idConta = idcontacorrente",
                        new { @idConta = idConta });

                    return await Task.FromResult(result);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<decimal?> GetSaldo(string idConta)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var result = await sqlConn.QueryFirstOrDefaultAsync<decimal?>(
                    @"SELECT (
	                    SELECT CONVERT(DECIMAL(15,2),SUM(valor)) 
	                    FROM [dbo].[movimento]
	                    WHERE idcontacorrente = @idConta
	                    AND tipomovimento = 'C'
	                    GROUP BY idcontacorrente ) 
	                    - 
	                    (SELECT CONVERT(DECIMAL(15,2),SUM(valor)) 
	                    FROM [dbo].[movimento]
	                    WHERE idcontacorrente = @idConta
	                    AND tipomovimento = 'D'
	                    GROUP BY idcontacorrente) 
                    AS Saldo",
                        new { @idConta = idConta });

                    return await Task.FromResult(result);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ContaCorrente> GetContaCorrente(string idConta)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var result = await sqlConn.QueryFirstOrDefaultAsync<ContaCorrente>(
                    @"SELECT *
                        FROM [dbo].[contacorrente]
                        WHERE idcontacorrente = @idConta",
                        new { @idConta = idConta });

                    return await Task.FromResult(result);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<decimal?> GetTotalCredito(string idConta)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var result = await sqlConn.QueryFirstOrDefaultAsync<decimal?>(
                    @"SELECT ISNULL(CONVERT(DECIMAL(15,2),SUM(valor)), 0)  
	                    FROM [dbo].[movimento]
	                    WHERE idcontacorrente = '382D323D-7067-ED11-8866-7D5DFA4A16C9'
	                    AND tipomovimento = 'C'
	                    GROUP BY idcontacorrente",
                        new { @idConta = idConta });

                    return await Task.FromResult(result is null ? 0 : result);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<decimal?> GetTotalDebito(string idConta)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var result = await sqlConn.QueryFirstOrDefaultAsync<decimal?>(
                    @"SELECT ISNULL(CONVERT(DECIMAL(15,2),SUM(valor)), 0)  
	                    FROM [dbo].[movimento]
	                    WHERE idcontacorrente = '382D323D-7067-ED11-8866-7D5DFA4A16C9'
	                    AND tipomovimento = 'D'
	                    GROUP BY idcontacorrente",
                        new { @idConta = idConta });

                    return await Task.FromResult(result is null ? 0 : result);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
