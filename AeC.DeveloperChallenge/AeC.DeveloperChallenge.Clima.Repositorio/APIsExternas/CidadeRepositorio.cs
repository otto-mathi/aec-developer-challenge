using AeC.DeveloperChallenge.Clima.Modelos.DTOs;
using AeC.DeveloperChallenge.Clima.Repositorios.APIsExternas.Interfaces;
using System.Net;
using System.Text.Json;

namespace AeC.DeveloperChallenge.Clima.Repositorios.APIsExternas
{
    /// <summary>
    /// Contém as implementações referentes ao repositório de cidades
    /// </summary>
    public class CidadeRepositorio : ICidadeRepositorio
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
        public CidadeRepositorio(HttpClient httpClient)
        {
            this._httpClient = httpClient;
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
            var resposta = await this._httpClient.GetAsync($"cidade/{nomeCidade}");

            if (resposta != null)
            {
                if (resposta.IsSuccessStatusCode)
                {
                    var conteudo = await resposta.Content.ReadAsStringAsync();

                    if (conteudo != null)
                    {
                        try
                        {
                            return JsonSerializer.Deserialize<ICollection<CidadeDTO>>(conteudo);
                        }
                        catch
                        {
                            throw new Exception("Não encontrado!");
                        }
                    }
                }
                else if (resposta.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Não encontrado!");
                }
                else
                {
                    resposta.EnsureSuccessStatusCode();
                }
            }

            throw new Exception("Não encontrado!");
        }

        #endregion
    }
}