using AeC.DeveloperChallenge.Clima.Modelos.Entidades;
using AeC.DeveloperChallenge.Clima.Repositorios.BancoDados;
using AeC.DeveloperChallenge.Clima.Repositorios.BancoDados.Interfaces;
using FluentAssertions;
using Moq;

namespace AeC.DeveloperChallenge.Clima.Servicos.Testes.BancoDados
{
    /// <summary>
    /// Contém as implementações referentes aos testes de serviço de logs de requisições
    /// </summary>
    public class LogRequisicaoServicoTestes
    {
        private Mock<ILogRequisicaoRepositorio> _logRequisicaoRepositorioMock;
        private LogRequisicaoServico _logRequisicaoServico;

        [Fact]
        public async Task AdicionaLogRequisicaoAsync_DeveExecutarComSucesso_QuandoLogForAdicionado()
        {
            // Arrange
            var logRequisicao = new LogRequisicao
            {
                CodigoRequisicao = 1,
                Tipo = "Info",
                Mensagem = "Log de teste"
            };

            this._logRequisicaoRepositorioMock = new Mock<ILogRequisicaoRepositorio>();
            this._logRequisicaoRepositorioMock
                .Setup(repo => repo.AdicionaLogRequisicaoAsync(logRequisicao))
                .Returns(Task.CompletedTask);

            this._logRequisicaoServico = new LogRequisicaoServico(this._logRequisicaoRepositorioMock.Object);

            // Act
            Func<Task> acao = async () => await this._logRequisicaoServico.AdicionaLogRequisicaoAsync(logRequisicao);

            // Assert
            await acao.Should().NotThrowAsync();
        }

        [Fact]
        public async Task AdicionaLogRequisicaoAsync_DeveLancarException_QuandoAdicionarFalhar()
        {
            // Arrange
            var logRequisicao = new LogRequisicao
            {
                CodigoRequisicao = 1,
                Tipo = "Error",
                Mensagem = "Erro de teste"
            };

            this._logRequisicaoRepositorioMock = new Mock<ILogRequisicaoRepositorio>();
            this._logRequisicaoRepositorioMock
                .Setup(repo => repo.AdicionaLogRequisicaoAsync(logRequisicao))
                .ThrowsAsync(new Exception("Erro ao adicionar log"));

            this._logRequisicaoServico = new LogRequisicaoServico(this._logRequisicaoRepositorioMock.Object);

            // Act
            Func<Task> acao = async () => await this._logRequisicaoServico.AdicionaLogRequisicaoAsync(logRequisicao);

            // Assert
            await acao.Should().ThrowAsync<Exception>()
                .WithMessage("Erro ao adicionar log");
        }
    }
}