using AeC.DeveloperChallenge.Clima.API.Controllers;
using AeC.DeveloperChallenge.Clima.Modelos.DTOs;
using AeC.DeveloperChallenge.Clima.Repositorios.BancoDados.Interfaces;
using AeC.DeveloperChallenge.Clima.Servicos.APIsExternas.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AeC.DeveloperChallenge.Clima.API.Testes.Controllers
{
    /// <summary>
    /// Contém os testes de endpoints da API de clima
    /// </summary>
    public class ClimaControllerTestes
    {
        private Mock<IClimaServico> _climaServicoMock;
        private Mock<IRequisicaoServico> _requisicaoServicoMock;
        private Mock<ILogRequisicaoServico> _logRequisicaoServicoMock;
        private Mock<ILogger<ClimaController>> _loggerMock;
        private ClimaController _climaController;

        [Fact]
        public async Task GetClimaPorNomeCidadeAsync_DeveRetornarOk_QuandoClimaForEncontrado()
        {
            // Arrange
            var nomeCidade = "Cidade Teste";
            var climaMock = new ClimaCidadeDTO
            {
                Cidade = "Cidade Teste",
                UF = "SP",
                DataAtualizacao = DateTime.Now,
                Detalhes = new List<DetalhesClimaCidadeDTO>
                {
                    new DetalhesClimaCidadeDTO
                    {
                        Data = DateTime.Now,
                        CodigoCondicao = "01",
                        DescricaoCondicao = "Ensolarado",
                        TemperaturaMinima = 20,
                        TemperaturaMaxima = 30,
                        IndiceUV = 5
                    }
                }
            };

            this._climaServicoMock = new Mock<IClimaServico>();
            this._climaServicoMock
                .Setup(s => s.BuscaClimaPorNomeCidadeAsync(nomeCidade))
                .ReturnsAsync(climaMock);

            this._requisicaoServicoMock = new Mock<IRequisicaoServico>();
            this._logRequisicaoServicoMock = new Mock<ILogRequisicaoServico>();
            this._loggerMock = new Mock<ILogger<ClimaController>>();

            this._climaController = new ClimaController(
                this._loggerMock.Object,
                this._climaServicoMock.Object,
                this._requisicaoServicoMock.Object,
                this._logRequisicaoServicoMock.Object
            );

            // Act
            var resultado = await this._climaController.GetClimaPorNomeCidadeAsync(nomeCidade);

            // Assert
            resultado.Should().BeOfType<ActionResult<ClimaCidadeDTO>>();

            var actionResult = resultado as ActionResult<ClimaCidadeDTO>;
            actionResult.Result.Should().BeOfType<OkObjectResult>();

            var resultadoOk = actionResult.Result as OkObjectResult;
            resultadoOk.Value.Should().Be(climaMock);
        }

        [Fact]
        public async Task GetClimaPorNomeCidadeAsync_DeveRetornarNotFound_QuandoClimaNaoForEncontrado()
        {
            // Arrange
            var nomeCidade = "Cidade Teste";

            this._climaServicoMock = new Mock<IClimaServico>();
            this._climaServicoMock
                .Setup(s => s.BuscaClimaPorNomeCidadeAsync(nomeCidade))
                .ReturnsAsync((ClimaCidadeDTO)null);

            this._requisicaoServicoMock = new Mock<IRequisicaoServico>();
            this._logRequisicaoServicoMock = new Mock<ILogRequisicaoServico>();
            this._loggerMock = new Mock<ILogger<ClimaController>>();

            this._climaController = new ClimaController(
                this._loggerMock.Object,
                this._climaServicoMock.Object,
                this._requisicaoServicoMock.Object,
                this._logRequisicaoServicoMock.Object
            );

            // Act
            var resultado = await this._climaController.GetClimaPorNomeCidadeAsync(nomeCidade);

            // Assert
            resultado.Should().BeOfType<ActionResult<ClimaCidadeDTO>>();
            
            var actionResult = resultado as ActionResult<ClimaCidadeDTO>;
            actionResult.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetClimaPorNomeCidadeAsync_DeveRetornarStatus500_QuandoExcecaoForLançada()
        {
            // Arrange
            var nomeCidade = "Cidade Teste";
            var excecao = new Exception("Erro inesperado");

            this._climaServicoMock = new Mock<IClimaServico>();
            this._climaServicoMock
                .Setup(s => s.BuscaClimaPorNomeCidadeAsync(nomeCidade))
                .ThrowsAsync(excecao);

            this._requisicaoServicoMock = new Mock<IRequisicaoServico>();
            this._logRequisicaoServicoMock = new Mock<ILogRequisicaoServico>();
            this._loggerMock = new Mock<ILogger<ClimaController>>();

            this._climaController = new ClimaController(
                this._loggerMock.Object,
                this._climaServicoMock.Object,
                this._requisicaoServicoMock.Object,
                this._logRequisicaoServicoMock.Object
            );

            // Act
            var resultado = await this._climaController.GetClimaPorNomeCidadeAsync(nomeCidade);

            // Assert
            resultado.Result.Should().BeOfType<ObjectResult>();

            var objectResult = resultado.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be(excecao.Message);
        }

        [Fact]
        public async Task GetClimaPorIcaoAeroportoAsync_DeveRetornarOk_QuandoClimaForEncontrado()
        {
            // Arrange
            var icao = "SBGR";
            var climaMock = new ClimaAeroportoDTO
            {
                Icao = "SBGR",
                DataAtualizacao = DateTime.Now,
                PressaoAtmosferica = 1013,
                Visibilidade = "10km",
                Vento = 5,
                DirecaoVento = 180,
                Umidade = 70,
                SiglaCondicao = "L",
                DescricaoCondicao = "Limpo",
                Temperatura = 25
            };

            this._climaServicoMock = new Mock<IClimaServico>();
            this._climaServicoMock
                .Setup(s => s.BuscaClimaPorIcaoAeroportoAsync(icao))
                .ReturnsAsync(climaMock);

            this._requisicaoServicoMock = new Mock<IRequisicaoServico>();
            this._logRequisicaoServicoMock = new Mock<ILogRequisicaoServico>();
            this._loggerMock = new Mock<ILogger<ClimaController>>();

            this._climaController = new ClimaController(
                this._loggerMock.Object,
                this._climaServicoMock.Object,
                this._requisicaoServicoMock.Object,
                this._logRequisicaoServicoMock.Object
            );

            // Act
            var resultado = await this._climaController.GetClimaPorIcaoAeroportoAsync(icao);

            // Assert
            resultado.Should().BeOfType<ActionResult<ClimaAeroportoDTO>>();

            var actionResult = resultado as ActionResult<ClimaAeroportoDTO>;
            actionResult.Result.Should().BeOfType<OkObjectResult>();
            
            var resultadoOk = actionResult.Result as OkObjectResult;
            resultadoOk.Value.Should().Be(climaMock);
        }

        [Fact]
        public async Task GetClimaPorIcaoAeroportoAsync_DeveRetornarNotFound_QuandoClimaNaoForEncontrado()
        {
            // Arrange
            var icao = "SBGR";

            this._climaServicoMock = new Mock<IClimaServico>();
            this._climaServicoMock
                .Setup(s => s.BuscaClimaPorIcaoAeroportoAsync(icao))
                .ReturnsAsync((ClimaAeroportoDTO)null);

            this._requisicaoServicoMock = new Mock<IRequisicaoServico>();
            this._logRequisicaoServicoMock = new Mock<ILogRequisicaoServico>();
            this._loggerMock = new Mock<ILogger<ClimaController>>();

            this._climaController = new ClimaController(
                this._loggerMock.Object,
                this._climaServicoMock.Object,
                this._requisicaoServicoMock.Object,
                this._logRequisicaoServicoMock.Object
            );

            // Act
            var resultado = await this._climaController.GetClimaPorIcaoAeroportoAsync(icao);

            // Assert
            resultado.Should().BeOfType<ActionResult<ClimaAeroportoDTO>>();

            var actionResult = resultado as ActionResult<ClimaAeroportoDTO>;
            actionResult.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetClimaPorIcaoAeroportoAsync_DeveRetornarStatus500_QuandoExcecaoForLançada()
        {
            // Arrange
            var icao = "SBGR";
            var excecao = new Exception("Erro inesperado");

            this._climaServicoMock = new Mock<IClimaServico>();
            this._climaServicoMock
                .Setup(s => s.BuscaClimaPorIcaoAeroportoAsync(icao))
                .ThrowsAsync(excecao);

            this._requisicaoServicoMock = new Mock<IRequisicaoServico>();
            this._logRequisicaoServicoMock = new Mock<ILogRequisicaoServico>();
            this._loggerMock = new Mock<ILogger<ClimaController>>();

            this._climaController = new ClimaController(
                this._loggerMock.Object,
                this._climaServicoMock.Object,
                this._requisicaoServicoMock.Object,
                this._logRequisicaoServicoMock.Object
            );

            // Act
            var resultado = await _climaController.GetClimaPorIcaoAeroportoAsync(icao);

            // Assert            
            resultado.Result.Should().BeOfType<ObjectResult>();

            var objectResult = resultado.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be(excecao.Message);
        }
    }
}