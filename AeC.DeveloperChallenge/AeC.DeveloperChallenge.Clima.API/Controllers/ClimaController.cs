using AeC.DeveloperChallenge.Clima.Modelos.DTOs;
using AeC.DeveloperChallenge.Clima.Modelos.Entidades;
using AeC.DeveloperChallenge.Clima.Repositorios.BancoDados.Interfaces;
using AeC.DeveloperChallenge.Clima.Servicos.APIsExternas.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AeC.DeveloperChallenge.Clima.API.Controllers
{
    /// <summary>
    /// Cont�m os endpoints da API de clima
    /// </summary>
    [ApiController]
    [Route("api/clima")]
    public class ClimaController : ControllerBase
    {
        #region Constantes

        private const string TEXTO_LOG_GET_CLIMA_POR_CIDADE = "Rota GetClimaPorNomeCidadeAsync chamada - nomeCidade: {nomeCidade} - Resposta: {climaDTO}";
        private const string TEXTO_LOG_GET_CLIMA_POR_CIDADE_NAO_ENCONTRADO = "Rota GetClimaPorNomeCidadeAsync chamada - nomeCidade: {nomeCidade} - Resposta: N�o encontrado!";
        private const string TEXTO_LOG_GET_CLIMA_POR_CIDADE_ERRO = "Rota GetClimaPorNomeCidadeAsync chamada - nomeCidade: {nomeCidade} - Resposta: Erro : {excecao}";
        private const string TEXTO_LOG_GET_CLIMA_POR_AEROPORTO = "Rota GetClimaPorIcaoAeroportoAsync chamada - icao: {icao} - Resposta: {climaDTO}";
        private const string TEXTO_LOG_GET_CLIMA_POR_AEROPORTO_NAO_ENCONTRADO = "Rota GetClimaPorIcaoAeroportoAsync chamada - icao: {icao} - Resposta: N�o encontrado!";
        private const string TEXTO_LOG_GET_CLIMA_POR_AEROPORTO_ERRO = "Rota GetClimaPorIcaoAeroportoAsync chamada - icao: {icao} - Resposta: {excecao}";
        private const string NOME_CONTROLLER = "ClimaController";
        private const string NOME_METODO_GET_CLIMA_POR_NOME_CIDADE = "GetClimaPorNomeCidadeAsync";
        private const string NOME_METODO_GET_CLIMA_POR_ICAO_AEROPORTO = "GetClimaPorIcaoAeroportoAsync";

        #endregion

        #region Campos

        /// <summary>
        /// Interface de log
        /// </summary>
        private readonly ILogger<ClimaController> _logger;

        /// <summary>
        /// Servi�o de climas
        /// </summary>
        private readonly IClimaServico _climaServico;

        /// <summary>
        /// Servi�o de requisi��es
        /// </summary>
        private readonly IRequisicaoServico _requisicaoServico;

        /// <summary>
        /// Servi�o de logs de requisi��es
        /// </summary>
        private readonly ILogRequisicaoServico _logRequisicaoServico;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="logger">Inje��o de depend�ncia da interface de log</param>
        /// <param name="climaServico">Inje��o de depend�ncia do servi�o de climas</param>
        /// <param name="requisicaoServico">Inje��o de depend�ncia de servi�o de requis��es</param>
        /// <param name="logRequisicaoServico">Inje��o de depend�ncia de servi�o de logs de requis��es</param>
        public ClimaController(ILogger<ClimaController> logger, IClimaServico climaServico, IRequisicaoServico requisicaoServico, ILogRequisicaoServico logRequisicaoServico)
        {
            this._logger = logger;
            this._climaServico = climaServico;
            this._requisicaoServico = requisicaoServico;
            this._logRequisicaoServico = logRequisicaoServico;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Recupera a previs�o do clima de uma cidade
        /// </summary>
        /// <param name="nomeCidade">Nome da cidade</param>
        /// <returns>Informa��es da previs�o do clima</returns>
        [HttpGet("cidade/{nomeCidade}")]
        public async Task<ActionResult<ClimaCidadeDTO>> GetClimaPorNomeCidadeAsync(string nomeCidade)
        {            
            try
            {
                var climaDTO = await this._climaServico.BuscaClimaPorNomeCidadeAsync(nomeCidade);

                if (climaDTO == null)
                {
                    this._logger.LogInformation(TEXTO_LOG_GET_CLIMA_POR_CIDADE_NAO_ENCONTRADO, nomeCidade);

                    return NotFound();
                }

                this._logger.LogInformation(TEXTO_LOG_GET_CLIMA_POR_CIDADE, nomeCidade, this.SerializaRetorno(climaDTO, true));

                await this.AdicionaRequisicao(NOME_CONTROLLER, NOME_METODO_GET_CLIMA_POR_NOME_CIDADE, climaDTO, ("nomeCidade: " + nomeCidade));

                return Ok(climaDTO);
            }
            catch (Exception excecao)
            {
                this._logger.LogError(TEXTO_LOG_GET_CLIMA_POR_CIDADE_ERRO, nomeCidade, excecao.Message);

                var codigoRequisicao = await this.AdicionaRequisicao(NOME_CONTROLLER, NOME_METODO_GET_CLIMA_POR_NOME_CIDADE, excecao.Message, ("nomeCidade: " + nomeCidade));
                await AdicionaLogRequisicao(codigoRequisicao, excecao.Message);

                return StatusCode(500, excecao.Message);
            }
        }

        /// <summary>
        /// Recupera a previs�o do clima de um aeroporto
        /// </summary>
        /// <returns>Informa��es da previs�o do clima</returns>
        [HttpGet("aeroporto/{icao}")]
        public async Task<ActionResult<ClimaAeroportoDTO>> GetClimaPorIcaoAeroportoAsync(string icao)
        {
            try
            {
                var climaDTO = await this._climaServico.BuscaClimaPorIcaoAeroportoAsync(icao);

                if (climaDTO == null)
                {
                    this._logger.LogInformation(TEXTO_LOG_GET_CLIMA_POR_AEROPORTO_NAO_ENCONTRADO, icao);

                    return NotFound();
                }

                this._logger.LogInformation(TEXTO_LOG_GET_CLIMA_POR_AEROPORTO, icao, this.SerializaRetorno(climaDTO, true));

                await this._requisicaoServico.AdicionaRequisicaoAsync(new Requisicao
                {
                    ControllerOrigem = NOME_CONTROLLER,
                    MetodoOrigem = NOME_METODO_GET_CLIMA_POR_ICAO_AEROPORTO,
                    Data = DateTime.Now,
                    Parametros = "icao: " + icao,
                    Retorno = this.SerializaRetorno(climaDTO)
                });

                await this.AdicionaRequisicao(NOME_CONTROLLER, NOME_METODO_GET_CLIMA_POR_ICAO_AEROPORTO, climaDTO, ("icao: " + icao));

                return Ok(climaDTO);
            }
            catch (Exception excecao)
            {
                this._logger.LogError(TEXTO_LOG_GET_CLIMA_POR_AEROPORTO_ERRO, icao, excecao.Message);

                var codigoRequisicao = await this.AdicionaRequisicao(NOME_CONTROLLER, NOME_METODO_GET_CLIMA_POR_ICAO_AEROPORTO, excecao.Message, ("icao: " + icao));
                await AdicionaLogRequisicao(codigoRequisicao, excecao.Message);

                return StatusCode(500, excecao.Message);
            }
        }

        /// <summary>
        /// Serializa um objeto de retoro de requisi��o
        /// </summary>
        /// <param name="conteudoRetorno">Objeto de retorno de requisi��o</param>
        /// <param name="deveIndentar">Indica se a serializa��o deve ser indentada</param>
        /// <returns>Objet serializado</returns>
        private string SerializaRetorno(object conteudoRetorno, bool deveIndentar = false)
        {
            return JsonSerializer.Serialize(conteudoRetorno, new JsonSerializerOptions { WriteIndented = deveIndentar });
        }

        /// <summary>
        /// Adiciona uma requisi��o
        /// </summary>
        /// <param name="nomeController">Nome da controller</param>
        /// <param name="nomeMetodo">Nome do m�todo</param>
        /// <param name="conteudoRetorno">Conte�do do retorno da requisi��o</param>
        /// <param name="parametro">Par�metro da requisi��o</param>
        /// <returns>C�digo da requisi��o criada</returns>
        private async Task<long> AdicionaRequisicao(string nomeController, string nomeMetodo, object conteudoRetorno, string parametro)
        {
            var codigo = 0L;

            codigo = await this._requisicaoServico.AdicionaRequisicaoAsync(
                new Requisicao 
                {
                    ControllerOrigem = nomeController,
                    MetodoOrigem = nomeMetodo,
                    MetodoHTTP = "GET",
                    Data = DateTime.Now,
                    Parametros = parametro,
                    Retorno = this.SerializaRetorno(conteudoRetorno)
                }
            );

            return codigo;
        }

        /// <summary>
        /// Adiciona um novo log de requisi��o
        /// </summary>
        /// <param name="codigoRequisicao">C�digo da requisi��o</param>
        /// <param name="mensagem">Mensagem</param>
        private async Task AdicionaLogRequisicao(long codigoRequisicao, string mensagem)
        {
            await this._logRequisicaoServico.AdicionaLogRequisicaoAsync(
                new LogRequisicao
                { 
                    CodigoRequisicao = codigoRequisicao,
                    Tipo = "Erro",
                    Mensagem = mensagem
                }
            );
        }

        #endregion
    }
}