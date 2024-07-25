using AeC.DeveloperChallenge.Clima.Modelos.DTOs;

namespace AeC.DeveloperChallenge.Clima.Repositorios.APIsExternas.Interfaces
{
    /// <summary>
    /// Contém os contratos referentes ao repositório de climas
    /// </summary>
    public interface IClimaRepositorio
    {
        #region Metodos

        /// <summary>
        /// Recupera as informações de clima pelo código da cidade
        /// </summary>
        /// <param name="codigoCidade">Código da cidade</param>
        /// <returns>Informações do clima</returns>
        Task<ClimaCidadeDTO> BuscaClimaPorCodigoCidadeAsync(int codigoCidade);

        /// <summary>
        /// Recupera as informações de clima pelo ICAO do aeroporto
        /// </summary>
        /// <param name="icao">ICAO do aeroporto (Ex.: SBGR - Aeroporto Internacional de São Paulo/Guarulhos)</param>
        /// <returns>Informações do clima</returns>
        Task<ClimaAeroportoDTO> BuscaClimaPorIcaoAeroportoAsync(string icao);

        #endregion
    }
}