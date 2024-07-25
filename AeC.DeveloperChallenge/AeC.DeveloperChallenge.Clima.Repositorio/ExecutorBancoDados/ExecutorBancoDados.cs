using AeC.DeveloperChallenge.Clima.Repositorios.ExecutorBancoDados.Interfaces;
using Dapper;
using System.Data;

namespace AeC.DeveloperChallenge.Clima.Repositorios.ExecutorBancoDados
{
    /// <summary>
    /// Contém as implementações referentes aos comandos assíncronos de banco de dados
    /// </summary>
    public class ExecutorBancoDados : IExecutorBancoDados
    {
        #region Campos

        /// <summary>
        /// Objeto de conexão com o banco de dados
        /// </summary>
        private readonly IDbConnection _conexao;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="conexao">Injeção de dependência do objeto de conexão com o banco de dados</param>
        public ExecutorBancoDados(IDbConnection conexao)
        {
            this._conexao = conexao;
        }

        #endregion

        /// <summary>
        /// Executa um comando SQL assíncrono no banco de dados
        /// </summary>
        /// <param name="comandoSQL">Comando SQL a ser executado</param>
        /// <param name="parametros">Parâmetros do comando SQL</param>
        /// <param name="transacao">Transação a ser usada para a execução do comando</param>
        /// <param name="timeout">Tempo limite para a execução do comando</param>
        /// <returns>Número de linhas afetadas</returns>
        public Task<int> ExecutaAsync(string comandoSQL, object parametros = null!, IDbTransaction transacao = null!, int? timeout = null)
        {
            return this._conexao.ExecuteAsync(comandoSQL, parametros, transacao, timeout);
        }

        /// <summary>
        /// Executa uma consulta SQL assíncrona e retorna um valor único
        /// </summary>
        /// <param name="comandoSQL">Consulta SQL a ser executada</param>
        /// <param name="parametros">Parâmetros da consulta SQL</param>
        /// <param name="transacao">Transação a ser usada para a execução da consulta</param>
        /// <param name="timeout">Tempo limite para a execução da consulta</param>
        /// <returns>Valor retornado</returns>
        public Task<T> ConsultaUnicoValorAsync<T>(string comandoSQL, object parametros = null!, IDbTransaction transacao = null!, int? timeout = null)
        {
            return this._conexao.QuerySingleOrDefaultAsync<T>(comandoSQL, parametros, transacao, timeout);
        }
    }
}