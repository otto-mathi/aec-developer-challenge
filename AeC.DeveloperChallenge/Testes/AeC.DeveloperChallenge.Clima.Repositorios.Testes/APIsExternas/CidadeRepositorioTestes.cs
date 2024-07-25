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
    /// Contém as implementações referentes aos testes de repositório de cidades
    /// </summary>
    public class CidadeRepositorioTestes
    {
        [Fact]
        public async Task BuscaCidadesPorNomeAsync_DeveRetornarListaDeCidades_QuandoChamadaForBemSucedida()
        {
            // Arrange
            var nomeCidade = "Cidade Teste";
            var cidadesMock = new List<CidadeDTO>
            {
                new CidadeDTO { Nome = "Cidade Teste 1" },
                new CidadeDTO { Nome = "Cidade Teste 2" }
            };
            var cidadesMockJson = JsonSerializer.Serialize(cidadesMock);
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
                    Content = new StringContent(cidadesMockJson)
                });

            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost/")
            };
            ICidadeRepositorio repositorio = new CidadeRepositorio(httpClient);

            // Act
            var resultado = await repositorio.BuscaCidadesPorNomeAsync(nomeCidade);

            // Assert
            resultado.Should().BeEquivalentTo(cidadesMock);
        }

        [Fact]
        public async Task BuscaCidadesPorNomeAsync_DeveLancarException_QuandoChamadaFalhar()
        {
            // Arrange
            var nomeCidade = "Cidade Teste";
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
            ICidadeRepositorio repositorio = new CidadeRepositorio(httpClient);

            // Act/Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => repositorio.BuscaCidadesPorNomeAsync(nomeCidade));
        }
    }
}