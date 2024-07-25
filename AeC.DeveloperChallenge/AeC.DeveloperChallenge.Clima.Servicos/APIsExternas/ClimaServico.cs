using AeC.DeveloperChallenge.Clima.Modelos.DTOs;
using AeC.DeveloperChallenge.Clima.Repositorios.APIsExternas.Interfaces;
using AeC.DeveloperChallenge.Clima.Repositorios.BancoDados.Interfaces;
using AeC.DeveloperChallenge.Clima.Servicos.APIsExternas.Interfaces;

namespace AeC.DeveloperChallenge.Clima.Servicos.APIsExternas
{
    /// <summary>
    /// Contém as implementações referentes ao serviço de climas
    /// </summary>
    public class ClimaServico : IClimaServico
    {
        #region Campos

        /// <summary>
        /// Serviço de cidades
        /// </summary>
        private readonly ICidadeServico _cidadeServico;

        /// <summary>
        /// Serviço de requisições
        /// </summary>
        private readonly IRequisicaoServico _requisicaoServico;

        /// <summary>
        /// Repositório de climas
        /// </summary>
        private readonly IClimaRepositorio _climaRepositorio;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="cidadeServico">Injeção de dependência do serviço de cidades</param>
        /// <param name="requisicaoServico">Injeção de dependência do serviço de requisições</param>
        /// <param name="climaRepositorio">Injeção de dependência do repositório de climas</param>
        public ClimaServico(ICidadeServico cidadeServico, IRequisicaoServico requisicaoServico, IClimaRepositorio climaRepositorio)
        {
            this._cidadeServico = cidadeServico;
            this._requisicaoServico = requisicaoServico;
            this._climaRepositorio = climaRepositorio;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Recupera as informações de clima pelo nome da cidade
        /// </summary>
        /// <param name="nomeCidade">Nome da cidade</param>
        /// <returns>Informações do clima</returns>
        public async Task<ClimaCidadeDTO> BuscaClimaPorNomeCidadeAsync(string nomeCidade)
        {
            var cidades = await BuscaClimasPorNomeCidadesAsync(nomeCidade);

            return cidades.FirstOrDefault(c => c.Cidade.ToLower() == nomeCidade.ToLower());
        }

        /// <summary>
        /// Busca as informações de climas pelos nomes de cidades
        /// </summary>
        /// <param name="nomeCidade">Nome da cidade (aceita parte do nome)</param>
        /// <returns>Coleção de informações de climas</returns>
        public async Task<ICollection<ClimaCidadeDTO>> BuscaClimasPorNomeCidadesAsync(string nomeCidade)
        {
            var cidades = await _cidadeServico.BuscaCidadesPorNomeAsync(nomeCidade);

            if (cidades != null)
            {
                var tarefasClima = cidades.Select(cidade => this._climaRepositorio.BuscaClimaPorCodigoCidadeAsync(cidade.Codigo));
                var climas = await Task.WhenAll(tarefasClima);

                return climas.ToList();
            }

            return new List<ClimaCidadeDTO>();
        }

        /// <summary>
        /// Recupera as informações de clima pelo ICAO do aeroporto
        /// </summary>
        /// <param name="icao">ICAO do aeroporto (Ex.: SBGR - Aeroporto Internacional de São Paulo/Guarulhos)</param>
        /// <returns>Informações do clima</returns>
        public async Task<ClimaAeroportoDTO> BuscaClimaPorIcaoAeroportoAsync(string icao)
        {
            return await this._climaRepositorio.BuscaClimaPorIcaoAeroportoAsync(icao);
        }

        #endregion
    }
}