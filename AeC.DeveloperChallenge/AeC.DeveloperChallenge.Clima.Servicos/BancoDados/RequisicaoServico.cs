using AeC.DeveloperChallenge.Clima.Modelos.Entidades;
using AeC.DeveloperChallenge.Clima.Repositorios.BancoDados.Interfaces;

namespace AeC.DeveloperChallenge.Clima.Repositorios.BancoDados
{
    /// <summary>
    /// Contém as implementações referentes ao serviço de requisições
    /// </summary>
    public class RequisicaoServico : IRequisicaoServico
    {
        #region Campos

        /// <summary>
        /// Repositório de requisições
        /// </summary>
        private readonly IRequisicaoRepositorio _requisicaoRepositorio;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="requisicaoRepositorio">Injeção de dependência do repositório de requisições</param>
        public RequisicaoServico(IRequisicaoRepositorio requisicaoRepositorio)
        {
            this._requisicaoRepositorio = requisicaoRepositorio;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Adiciona uma nova requisição
        /// </summary>
        /// <param name="requisicao">Informações da requisição</param>
        /// <returns>Código da requisição criada</returns>
        public async Task<long> AdicionaRequisicaoAsync(Requisicao requisicao)
        {
            return await this._requisicaoRepositorio.AdicionaRequisicaoAsync(requisicao);
        }

        #endregion
    }
}