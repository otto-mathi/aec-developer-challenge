using AeC.DeveloperChallenge.Clima.Modelos.DTOs;

namespace AeC.DeveloperChallenge.Clima.Servicos.APIsExternas.Interfaces
{
    /// <summary>
    /// Contém os contratos referentes ao serviço de climas
    /// </summary>
    public interface IClimaServico
    {
        #region Metodos

        /// <summary>
        /// Recupera as informações de clima pelo nome da cidade
        /// </summary>
        /// <param name="nomeCidade">Nome da cidade</param>
        /// <returns>Informações do clima</returns>
        Task<ClimaCidadeDTO> BuscaClimaPorNomeCidadeAsync(string nomeCidade);

        /// <summary>
        /// Busca as informações de climas pelos nomes de cidades
        /// </summary>
        /// <param name="nomeCidade">Nome da cidade (aceita parte do nome)</param>
        /// <returns>Coleção de informações de climas</returns>
        Task<ICollection<ClimaCidadeDTO>> BuscaClimasPorNomeCidadesAsync(string nomeCidade);

        /// <summary>
        /// Recupera as informações de clima pelo ICAO do aeroporto
        /// </summary>
        /// <param name="icao">ICAO do aeroporto (Ex.: SBGR - Aeroporto Internacional de São Paulo/Guarulhos)</param>
        /// <returns>Informações do clima</returns>
        Task<ClimaAeroportoDTO> BuscaClimaPorIcaoAeroportoAsync(string icao);

        #endregion
    }
}