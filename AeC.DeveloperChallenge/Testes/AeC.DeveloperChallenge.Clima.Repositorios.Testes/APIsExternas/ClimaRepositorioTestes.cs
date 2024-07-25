using AeC.DeveloperChallenge.Clima.Modelos.DTOs;
using AeC.DeveloperChallenge.Clima.Repositorios.APIsExternas;
using AeC.DeveloperChallenge.Clima.Repositorios.APIsExternas.Interfaces;
using FluentAssertions;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;

namespace AeC.DeveloperChallenge.Clima.Repositorios.Testes.APIsExternas
{
    /// <summary>
    /// Contém as implementações referentes aos testes de repositório de climas
    /// </summary>
    public class ClimaRepositorioTestes
    {
        [Fact]
        public async Task BuscaClimaPorCodigoCidadeAsync_DeveRetornarClima_QuandoChamadaForBemSucedida()
        {
            // Arrange
            var codigoCidade = 123;
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
            var climaMockJson = JsonSerializer.Serialize(climaMock);
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(climaMockJson)
                });

            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost/")
            };
            IClimaRepositorio repositorio = new ClimaRepositorio(httpClient);

            // Act
            var resultado = await repositorio.BuscaClimaPorCodigoCidadeAsync(codigoCidade);

            // Assert
            resultado.Should().BeEquivalentTo(climaMock);
        }

        [Fact]
        public async Task BuscaClimaPorCodigoCidadeAsync_DeveLancarException_QuandoChamadaFalhar()
        {
            // Arrange
            var codigoCidade = 123;
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost/")
            };
            IClimaRepositorio repositorio = new ClimaRepositorio(httpClient);

            // Act/Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => repositorio.BuscaClimaPorCodigoCidadeAsync(codigoCidade));
        }

        [Fact]
        public async Task BuscaClimaPorIcaoAeroportoAsync_DeveRetornarClima_QuandoChamadaForBemSucedida()
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
            var climaMockJson = JsonSerializer.Serialize(climaMock);
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(climaMockJson)
                });

            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost/")
            };
            IClimaRepositorio repositorio = new ClimaRepositorio(httpClient);

            // Act
            var resultado = await repositorio.BuscaClimaPorIcaoAeroportoAsync(icao);

            // Assert
            resultado.Should().BeEquivalentTo(climaMock);
        }

        [Fact]
        public async Task BuscaClimaPorIcaoAeroportoAsync_DeveLancarException_QuandoChamadaFalhar()
        {
            // Arrange
            var icao = "SBGR";
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost/")
            };
            IClimaRepositorio repositorio = new ClimaRepositorio(httpClient);

            // Act/Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => repositorio.BuscaClimaPorIcaoAeroportoAsync(icao));
        }
    }
}