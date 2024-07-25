using System.Data;

namespace AeC.DeveloperChallenge.Clima.Repositorios.ExecutorBancoDados.Interfaces
{
    /// <summary>
    /// Contém os contratos referentes aos comandos assíncronos de banco de dados
    /// </summary>
    public interface IExecutorBancoDados
    {
        #region Metodos

        /// <summary>
        /// Executa um comando SQL assíncrono no banco de dados
        /// </summary>
        /// <param name="comandoSQL">Comando SQL a ser executado</param>
        /// <param name="parametros">Parâmetros do comando SQL</param>
        /// <param name="transacao">Transação a ser usada para a execução do comando</param>
        /// <param name="timeout">Tempo limite para a execução do comando</param>
        /// <returns>Número de linhas afetadas</returns>
        Task<int> ExecutaAsync(string comandoSQL, object parametros = null!, IDbTransaction transacao = null!, int? timeout = null);

        /// <summary>
        /// Executa uma consulta SQL assíncrona e retorna um valor único
        /// </summary>
        /// <param name="comandoSQL">Consulta SQL a ser executada</param>
        /// <param name="parametros">Parâmetros da consulta SQL</param>
        /// <param name="transacao">Transação a ser usada para a execução da consulta</param>
        /// <param name="timeout">Tempo limite para a execução da consulta</param>
        /// <returns>Valor retornado</returns>
        Task<T> ConsultaUnicoValorAsync<T>(string comandoSQL, object parametros = null!, IDbTransaction transacao = null!, int? timeout = null);

        #endregion
    }
}