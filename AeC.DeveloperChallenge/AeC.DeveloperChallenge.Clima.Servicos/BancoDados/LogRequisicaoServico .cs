using AeC.DeveloperChallenge.Clima.Modelos.Entidades;
using AeC.DeveloperChallenge.Clima.Repositorios.BancoDados.Interfaces;

namespace AeC.DeveloperChallenge.Clima.Repositorios.BancoDados
{
    /// <summary>
    /// Contém as implementações referentes ao serviço de logs de requisições
    /// </summary>
    public class LogRequisicaoServico : ILogRequisicaoServico
    {
        #region Campos

        /// <summary>
        /// Repositório de logs de requisições
        /// </summary>
        private readonly ILogRequisicaoRepositorio _logRequisicaoRepositorio;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="logRequisicaoRepositorio">Injeção de dependência do repositório de logs de requisições</param>
        public LogRequisicaoServico(ILogRequisicaoRepositorio logRequisicaoRepositorio)
        {
            this._logRequisicaoRepositorio = logRequisicaoRepositorio;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Adiciona um novo log de requisição
        /// </summary>
        /// <param name="logRequisicao">Informações do log de requisição</param>
        public async Task AdicionaLogRequisicaoAsync(LogRequisicao logRequisicao)
        {
            await this._logRequisicaoRepositorio.AdicionaLogRequisicaoAsync(logRequisicao);
        }

        #endregion
    }
}