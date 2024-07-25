using AeC.DeveloperChallenge.Clima.Modelos.DTOs;

namespace AeC.DeveloperChallenge.Clima.Servicos.APIsExternas.Interfaces
{
    /// <summary>
    /// Contém os contratos referentes ao serviço de cidades
    /// </summary>
    public interface ICidadeServico
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