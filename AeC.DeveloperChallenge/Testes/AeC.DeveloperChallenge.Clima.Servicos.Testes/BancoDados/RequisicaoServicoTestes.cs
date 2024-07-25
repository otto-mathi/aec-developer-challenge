using AeC.DeveloperChallenge.Clima.Modelos.Entidades;
using AeC.DeveloperChallenge.Clima.Repositorios.BancoDados;
using AeC.DeveloperChallenge.Clima.Repositorios.BancoDados.Interfaces;
using FluentAssertions;
using Moq;

namespace AeC.DeveloperChallenge.Clima.Servicos.Testes.BancoDados
{
    /// <summary>
    /// Contém as implementações referentes aos testes de serviço de requisições
    /// </summary>
    public class RequisicaoServicoTestes
    {
        private Mock<IRequisicaoRepositorio> _requisicaoRepositorioMock;
        private RequisicaoServico _requisicaoServico;

        [Fact]
        public async Task AdicionaRequisicaoAsync_DeveRetornarCodigo_QuandoInsercaoForBemSucedida()
        {
            // Arrange
            var codigoRetornado = 1L;
            var requisicao = new Requisicao
            {
                ControllerOrigem = "Controller",
                MetodoOrigem = "Get",
                MetodoHTTP = "GET",
                Data = DateTime.Now,
                Parametros = "param1=valor1",
                Retorno = "{ chave1: valor1, chave2: valor2 }"
            };
            
            this._requisicaoRepositorioMock = new Mock<IRequisicaoRepositorio>();
            this._requisicaoRepositorioMock
                .Setup(repo => repo.AdicionaRequisicaoAsync(requisicao))
                .ReturnsAsync(codigoRetornado);

            _requisicaoServico = new RequisicaoServico(this._requisicaoRepositorioMock.Object);

            // Act
            var resultado = await this._requisicaoServico.AdicionaRequisicaoAsync(requisicao);

            // Assert
            resultado.Should().Be(codigoRetornado);
        }

        [Fact]
        public async Task AdicionaRequisicaoAsync_DeveLancarException_QuandoInsercaoFalhar()
        {
            // Arrange
            var requisicao = new Requisicao
            {
                ControllerOrigem = "Controller",
                MetodoOrigem = "Get",
                MetodoHTTP = "GET",
                Data = DateTime.Now,
                Parametros = "param1=valor1",
                Retorno = "{ chave1: valor1, chave2: valor2 }"
            };

            this._requisicaoRepositorioMock = new Mock<IRequisicaoRepositorio>();
            this._requisicaoRepositorioMock
                .Setup(repo => repo.AdicionaRequisicaoAsync(requisicao))
                .ThrowsAsync(new Exception("Erro na inserção"));

            this._requisicaoServico = new RequisicaoServico(this._requisicaoRepositorioMock.Object);

            // Act
            Func<Task> acao = async () => await this._requisicaoServico.AdicionaRequisicaoAsync(requisicao);

            // Assert
            await acao.Should().ThrowAsync<Exception>()
                .WithMessage("Erro na inserção");
        }
    }
}