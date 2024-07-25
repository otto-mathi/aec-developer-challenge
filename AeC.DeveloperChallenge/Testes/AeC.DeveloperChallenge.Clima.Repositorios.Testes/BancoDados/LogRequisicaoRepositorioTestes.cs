using AeC.DeveloperChallenge.Clima.Modelos.Entidades;
using AeC.DeveloperChallenge.Clima.Repositorios.BancoDados;
using AeC.DeveloperChallenge.Clima.Repositorios.ExecutorBancoDados.Interfaces;
using FluentAssertions;
using Moq;
using System.Data;

namespace AeC.DeveloperChallenge.Clima.Repositorios.Testes.BancoDados
{
    /// <summary>
    /// Contém as implementações referentes aos testes de repositório de logs de requisições
    /// </summary>
    public class LogRequisicaoRepositorioTestes
    {
        [Fact]
        public async Task AdicionaLogRequisicaoAsync_DeveExecutarComSucesso_QuandoInsercaoForBemSucedida()
        {
            // Arrange
            var executorComandosMock = new Mock<IExecutorBancoDados>();
            executorComandosMock
                .Setup(executor => executor.ExecutaAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>()))
                .ReturnsAsync(1);

            var repositorio = new LogRequisicaoRepositorio(executorComandosMock.Object);
            var logRequisicao = new LogRequisicao
            {
                CodigoRequisicao = 1,
                Tipo = "Sucesso",                
                Mensagem = "Mensagem de log"
            };

            // Act
            await repositorio.AdicionaLogRequisicaoAsync(logRequisicao);

            // Assert
            executorComandosMock.Verify(executor => executor.ExecutaAsync(
                It.IsAny<string>(),
                It.IsAny<object>(),
                It.IsAny<IDbTransaction>(),
                It.IsAny<int?>()
            ), Times.Once);
        }

        [Fact]
        public async Task AdicionaLogRequisicaoAsync_DeveLancarException_QuandoInsercaoFalhar()
        {
            // Arrange
            var executorMock = new Mock<IExecutorBancoDados>();
            var logRequisicao = new LogRequisicao
            {
                CodigoRequisicao = 1,
                Tipo = "Erro",
                Mensagem = "Mensagem de erro"
            };

            executorMock
                .Setup(executor => executor.ExecutaAsync(It.IsAny<string>(), It.IsAny<object>(), null!, null))
                .ThrowsAsync(new Exception("Erro de inserção"));

            var repositorio = new LogRequisicaoRepositorio(executorMock.Object);

            // Act
            Func<Task> acao = async () => await repositorio.AdicionaLogRequisicaoAsync(logRequisicao);

            // Assert
            await acao.Should().ThrowAsync<Exception>().WithMessage("Erro de inserção");

            executorMock.Verify(executor => executor.ExecutaAsync(
                It.IsAny<string>(),
                It.IsAny<object>(),
                null!,
                null
            ), Times.Once);
        }
    }
}