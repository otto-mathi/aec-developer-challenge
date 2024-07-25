using AeC.DeveloperChallenge.Clima.Modelos.DTOs;
using AeC.DeveloperChallenge.Clima.Repositorios.APIsExternas.Interfaces;
using AeC.DeveloperChallenge.Clima.Repositorios.BancoDados.Interfaces;
using AeC.DeveloperChallenge.Clima.Servicos.APIsExternas;
using AeC.DeveloperChallenge.Clima.Servicos.APIsExternas.Interfaces;
using FluentAssertions;
using Moq;

namespace AeC.DeveloperChallenge.Clima.Servicos.Testes.APIsExternas
{
    /// <summary>
    /// Contém as implementações referentes aos testes de serviço de climas
    /// </summary>
    public class ClimaServicoTestes
    {
        private Mock<ICidadeServico> _cidadeServicoMock;
        private Mock<IRequisicaoServico> _requisicaoServicoMock;
        private Mock<IClimaRepositorio> _climaRepositorioMock;
        private ClimaServico _climaServico;

        [Fact]
        public async Task BuscaClimaPorNomeCidadeAsync_DeveRetornarClima_QuandoCidadesExistem()
        {
            // Arrange
            var nomeCidade = "Cidade Teste";
            var cidadeDTO = new CidadeDTO { Nome = "Cidade Teste", Codigo = 1 };
            var climaCidadeDTO = new ClimaCidadeDTO
            {
                Cidade = "Cidade Teste",
                UF = "SP",
                DataAtualizacao = DateTime.Now,
                Detalhes = new List<DetalhesClimaCidadeDTO>
                {
                    new DetalhesClimaCidadeDTO
                    {
                        Data = DateTime.Now,
                        CodigoCondicao = "L",
                        DescricaoCondicao = "Limpo",
                        TemperaturaMinima = 20,
                        TemperaturaMaxima = 30,
                        IndiceUV = 5
                    }
                }
            };

            this._requisicaoServicoMock = new Mock<IRequisicaoServico>();

            this._cidadeServicoMock = new Mock<ICidadeServico>();            
            this._cidadeServicoMock
                .Setup(servico => servico.BuscaCidadesPorNomeAsync(nomeCidade))
                .ReturnsAsync(new List<CidadeDTO> { cidadeDTO });

            this._climaRepositorioMock = new Mock<IClimaRepositorio>();
            this._climaRepositorioMock
                .Setup(repositorio => repositorio.BuscaClimaPorCodigoCidadeAsync(cidadeDTO.Codigo))
                .ReturnsAsync(climaCidadeDTO);

            this._climaServico = new ClimaServico(this._cidadeServicoMock.Object, this._requisicaoServicoMock.Object, this._climaRepositorioMock.Object);

            // Act
            var resultado = await this._climaServico.BuscaClimaPorNomeCidadeAsync(nomeCidade);

            // Assert
            resultado.Should().BeEquivalentTo(climaCidadeDTO);
        }

        [Fact]
        public async Task BuscaClimaPorNomeCidadeAsync_DeveRetornarNull_QuandoCidadeNaoExistir()
        {
            // Arrange
            var nomeCidade = "Cidade Teste";

            this._requisicaoServicoMock = new Mock<IRequisicaoServico>();
            this._climaRepositorioMock = new Mock<IClimaRepositorio>();

            this._cidadeServicoMock = new Mock<ICidadeServico>();
            this._cidadeServicoMock
                .Setup(servico => servico.BuscaCidadesPorNomeAsync(nomeCidade))
                .ReturnsAsync(new List<CidadeDTO>());

            this._climaServico = new ClimaServico(this._cidadeServicoMock.Object, this._requisicaoServicoMock.Object, this._climaRepositorioMock.Object);

            // Act
            var resultado = await this._climaServico.BuscaClimaPorNomeCidadeAsync(nomeCidade);

            // Assert
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task BuscaClimasPorNomeCidadesAsync_DeveRetornarClimas_QuandoCidadesExistem()
        {
            // Arrange
            var nomeCidade = "Cidade Teste";
            var cidadeDTO = new CidadeDTO { Nome = "Cidade Teste", Codigo = 1 };
            var climaCidadeDTO = new ClimaCidadeDTO
            {
                Cidade = "Cidade Teste",
                UF = "SP",
                DataAtualizacao = DateTime.Now,
                Detalhes = new List<DetalhesClimaCidadeDTO>
                {
                    new DetalhesClimaCidadeDTO
                    {
                        Data = DateTime.Now,
                        CodigoCondicao = "L",
                        DescricaoCondicao = "Limpo",
                        TemperaturaMinima = 20,
                        TemperaturaMaxima = 30,
                        IndiceUV = 5
                    }
                }
            };

            this._requisicaoServicoMock = new Mock<IRequisicaoServico>();

            this._cidadeServicoMock = new Mock<ICidadeServico>();
            this._cidadeServicoMock
                .Setup(servico => servico.BuscaCidadesPorNomeAsync(nomeCidade))
                .ReturnsAsync(new List<CidadeDTO> { cidadeDTO });

            this._climaRepositorioMock = new Mock<IClimaRepositorio>();
            this._climaRepositorioMock
                .Setup(repositorio => repositorio.BuscaClimaPorCodigoCidadeAsync(cidadeDTO.Codigo))
                .ReturnsAsync(climaCidadeDTO);

            this._climaServico = new ClimaServico(this._cidadeServicoMock.Object, this._requisicaoServicoMock.Object, this._climaRepositorioMock.Object);

            // Act
            var resultado = await this._climaServico.BuscaClimasPorNomeCidadesAsync(nomeCidade);

            // Assert
            resultado.Should().Contain(climaCidadeDTO);
        }

        [Fact]
        public async Task BuscaClimaPorIcaoAeroportoAsync_DeveRetornarClima_QuandoAeroportoExiste()
        {
            // Arrange
            var icao = "SBGR";
            var climaAeroportoDTO = new ClimaAeroportoDTO
            {
                Icao = "SBGR",
                DataAtualizacao = DateTime.Now,
                PressaoAtmosferica = 1013,
                Visibilidade = "10 km",
                Vento = 5,
                DirecaoVento = 270,
                Umidade = 80,
                SiglaCondicao = "L",
                DescricaoCondicao = "Limpo",
                Temperatura = 25
            };

            this._requisicaoServicoMock = new Mock<IRequisicaoServico>();
            this._cidadeServicoMock = new Mock<ICidadeServico>();

            this._climaRepositorioMock = new Mock<IClimaRepositorio>();
            this._climaRepositorioMock
                .Setup(repositorio => repositorio.BuscaClimaPorIcaoAeroportoAsync(icao))
                .ReturnsAsync(climaAeroportoDTO);

            this._climaServico = new ClimaServico(this._cidadeServicoMock.Object, this._requisicaoServicoMock.Object, this._climaRepositorioMock.Object);

            // Act
            var resultado = await this._climaServico.BuscaClimaPorIcaoAeroportoAsync(icao);

            // Assert
            resultado.Should().BeEquivalentTo(climaAeroportoDTO);
        }
    }
}