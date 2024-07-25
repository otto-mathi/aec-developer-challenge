using AeC.DeveloperChallenge.Clima.Modelos.DTOs;

namespace AeC.DeveloperChallenge.Clima.Repositorios.APIsExternas.Interfaces
{
    /// <summary>
    /// Contém os contratos referentes ao repositório de cidades
    /// </summary>
    public interface ICidadeRepositorio
    {
        #region Metodos

        /// <summary>
        /// Busca cidades pelos seus nomes
        /// </summary>
        /// <param name="nomeCidade">Nome da cidade (permite nome parcial)</param>
        /// <returns>Coleção com informações das cidades</returns>
        Task<ICollection<CidadeDTO>> BuscaCidadesPorNomeAsync(string nomeCidade);

        #endregion
    }
}