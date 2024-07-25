using AeC.DeveloperChallenge.Clima.Modelos.DTOs;
using AeC.DeveloperChallenge.Clima.Repositorios.APIsExternas.Interfaces;
using AeC.DeveloperChallenge.Clima.Servicos.APIsExternas;
using FluentAssertions;
using Moq;

namespace AeC.DeveloperChallenge.Clima.Servicos.Testes.APIsExternas
{
    /// <summary>
    /// Contém as implementações referentes aos testes de serviço de cidades
    /// </summary>
    public class CidadeServicoTestes
    {
        private Mock<ICidadeRepositorio> _cidadeRepositorioMock;
        private CidadeServico _cidadeServico;

        [Fact]
        public async Task BuscaCidadesPorNomeAsync_DeveRetornarCidades_QuandoRepositorioRetornarCidades()
        {
            // Arrange
            var nomeCidade = "Cidade Teste";
            var cidadesEsperadas = new List<CidadeDTO>
            {
                new CidadeDTO { Nome = "Cidade Teste", UF = "SP" }
            };

            this._cidadeRepositorioMock = new Mock<ICidadeRepositorio>();
            this._cidadeRepositorioMock
                .Setup(repositorio => repositorio.BuscaCidadesPorNomeAsync(nomeCidade))
                .ReturnsAsync(cidadesEsperadas);

            // Instancia o serviço com o repositório mockado
            this._cidadeServico = new CidadeServico(this._cidadeRepositorioMock.Object);

            // Act
            var resultado = await _cidadeServico.BuscaCidadesPorNomeAsync(nomeCidade);

            // Assert
            resultado.Should().BeEquivalentTo(cidadesEsperadas);
        }

        [Fact]
        public async Task BuscaCidadesPorNomeAsync_DeveLancarException_QuandoRepositorioLancarException()
        {
            // Arrange
            var nomeCidade = "Cidade Teste";
            var excecaoEsperada = new Exception("Erro ao buscar cidades");

            this._cidadeRepositorioMock = new Mock<ICidadeRepositorio>();
            this._cidadeRepositorioMock
                .Setup(repositorio => repositorio.BuscaCidadesPorNomeAsync(nomeCidade))
                .ThrowsAsync(excecaoEsperada);

            // Instancia o serviço com o repositório mockado
            _cidadeServico = new CidadeServico(_cidadeRepositorioMock.Object);

            // Act
            Func<Task> acao = async () => await _cidadeServico.BuscaCidadesPorNomeAsync(nomeCidade);

            // Assert
            await acao.Should().ThrowAsync<Exception>().WithMessage(excecaoEsperada.Message);
        }
    }
}