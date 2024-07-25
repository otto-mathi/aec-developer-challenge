using AeC.DeveloperChallenge.Clima.Modelos.Entidades;

namespace AeC.DeveloperChallenge.Clima.Repositorios.BancoDados.Interfaces
{
    /// <summary>
    /// Contém os contratos referentes ao serviço de logs de requisições
    /// </summary>
    public interface ILogRequisicaoServico
    {
        #region Metodos

        /// <summary>
        /// Adiciona um novo log de requisição
        /// </summary>
        /// <param name="requisicao">Informações do log de requisição</param>
        Task AdicionaLogRequisicaoAsync(LogRequisicao logRequisicao);

        #endregion
    }
}