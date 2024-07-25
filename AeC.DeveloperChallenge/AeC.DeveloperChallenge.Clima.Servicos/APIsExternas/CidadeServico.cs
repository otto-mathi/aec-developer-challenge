using AeC.DeveloperChallenge.Clima.Modelos.DTOs;
using AeC.DeveloperChallenge.Clima.Repositorios.APIsExternas.Interfaces;
using AeC.DeveloperChallenge.Clima.Servicos.APIsExternas.Interfaces;

namespace AeC.DeveloperChallenge.Clima.Servicos.APIsExternas
{
    /// <summary>
    /// Contém as implementações referentes ao serviço de cidades
    /// </summary>
    public class CidadeServico : ICidadeServico
    {
        #region Campos

        /// <summary>
        /// Repositório de cidades
        /// </summary>
        private readonly ICidadeRepositorio _cidadeRepositorio;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="cidadeRepositorio">Injeção de dependência do repositório de cidades</param>
        public CidadeServico(ICidadeRepositorio cidadeRepositorio)
        {
            this._cidadeRepositorio = cidadeRepositorio;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Busca cidades pelos seus nomes
        /// </summary>
        /// <param name="nomeCidade">Nome da cidade (permite nome parcial)</param>
        /// <returns>Coleção com informações das cidades</returns>
        public async Task<ICollection<CidadeDTO>> BuscaCidadesPorNomeAsync(string nomeCidade)
        {
            return await this._cidadeRepositorio.BuscaCidadesPorNomeAsync(nomeCidade);
        }

        #endregion
    }
}