using AeC.DeveloperChallenge.Clima.Modelos.Entidades;
using AeC.DeveloperChallenge.Clima.Repositorios.BancoDados;
using AeC.DeveloperChallenge.Clima.Repositorios.ExecutorBancoDados.Interfaces;
using FluentAssertions;
using Moq;
using System.Data;

namespace AeC.DeveloperChallenge.Clima.Repositorios.Testes.BancoDados
{
    /// <summary>
    /// Contém as implementações referentes aos testes de repositório de requisições
    /// </summary>
    public class RequisicaoRepositorioTestes
    {
        [Fact]
        public async Task AdicionaRequisicaoAsync_DeveRetornarCodigoRequisicao_QuandoInsercaoForBemSucedida()
        {
            // Arrange
            var executorMock = new Mock<IExecutorBancoDados>();
            var codigoEsperado = 1L;
            var requisicao = new Requisicao
            {
                ControllerOrigem = "Controller",
                MetodoOrigem = "Metodo",
                MetodoHTTP = "GET",
                Data = DateTime.Now,
                Parametros = "parametros",
                Retorno = "retorno"
            };

            executorMock
                .Setup(executor => executor.ConsultaUnicoValorAsync<long>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>()))
                .ReturnsAsync(codigoEsperado);

            var repositorio = new RequisicaoRepositorio(executorMock.Object);

            // Act
            var codigoGerado = await repositorio.AdicionaRequisicaoAsync(requisicao);

            // Assert
            codigoGerado.Should().Be(codigoEsperado);

            executorMock.Verify(executor => executor.ConsultaUnicoValorAsync<long>(
                It.IsAny<string>(), 
                It.IsAny<object>(), 
                It.IsAny<IDbTransaction>(), 
                It.IsAny<int?>()
            ), Times.Once);
        }

        [Fact]
        public async Task AdicionaRequisicaoAsync_DeveLancarException_QuandoInsercaoFalhar()
        {
            // Arrange
            var executorMock = new Mock<IExecutorBancoDados>();
            var requisicao = new Requisicao
            {
                ControllerOrigem = "Controller",
                MetodoOrigem = "Metodo",
                MetodoHTTP = "GET",
                Data = DateTime.Now,
                Parametros = "parametros",
                Retorno = "retorno"
            };

            executorMock
                .Setup(executor => executor.ConsultaUnicoValorAsync<long>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>()))
                .ThrowsAsync(new Exception("Erro de inserção"));

            var repositorio = new RequisicaoRepositorio(executorMock.Object);

            // Act
            Func<Task> acao = async () => await repositorio.AdicionaRequisicaoAsync(requisicao);

            // Assert
            await acao.Should().ThrowAsync<Exception>().WithMessage("Erro de inserção");

            executorMock.Verify(executor => executor.ConsultaUnicoValorAsync<long>(
                It.IsAny<string>(),
                It.IsAny<object>(),
                It.IsAny<IDbTransaction>(),
                It.IsAny<int?>()
            ), Times.Once);
        }
    }
}