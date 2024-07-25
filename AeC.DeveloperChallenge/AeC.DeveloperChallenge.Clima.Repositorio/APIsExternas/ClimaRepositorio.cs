using AeC.DeveloperChallenge.Clima.Modelos.DTOs;
using AeC.DeveloperChallenge.Clima.Repositorios.APIsExternas.Interfaces;
using System.Text.Json;

namespace AeC.DeveloperChallenge.Clima.Repositorios.APIsExternas
{
    /// <summary>
    /// Contém as implementações referentes ao repositório de climas
    /// </summary>
    public class ClimaRepositorio : IClimaRepositorio
    {
        #region Campos

        /// <summary>
        /// Objeto de requisições HTTP
        /// </summary>
        private readonly HttpClient _httpClient;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="httpClient">Injeção de dependência do objeto de requisições HTTP</param>
        public ClimaRepositorio(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Recupera as informações de clima pelo código da cidade
        /// </summary>
        /// <param name="codigoCidade">Código da cidade</param>
        /// <returns>Informações do clima</returns>
        public async Task<ClimaCidadeDTO> BuscaClimaPorCodigoCidadeAsync(int codigoCidade)
        {
            var resposta = await this._httpClient.GetAsync($"clima/previsao/{codigoCidade}");

            resposta.EnsureSuccessStatusCode();

            var conteudo = await resposta.Content.ReadAsStringAsync();
            var climaDTO = JsonSerializer.Deserialize<ClimaCidadeDTO>(conteudo);

            return climaDTO;
        }

        /// <summary>
        /// Busca as informações de clima pelo ICAO de um aeroporto
        /// </summary>
        /// <param name="icao">ICAO do aeroporto (Ex.: SBGR - Aeroporto Internacional de São Paulo/Guarulhos)</param>
        /// <returns>Informações do clima</returns>
        public async Task<ClimaAeroportoDTO> BuscaClimaPorIcaoAeroportoAsync(string icao)
        {
            var resposta = await this._httpClient.GetAsync($"clima/aeroporto/{icao}");

            resposta.EnsureSuccessStatusCode();

            var conteudo = await resposta.Content.ReadAsStringAsync();
            var climaDTO = JsonSerializer.Deserialize<ClimaAeroportoDTO>(conteudo);

            return climaDTO;
        }

        #endregion
    }
}