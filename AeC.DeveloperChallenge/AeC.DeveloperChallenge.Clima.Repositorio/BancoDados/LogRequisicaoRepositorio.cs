using AeC.DeveloperChallenge.Clima.Modelos.Entidades;
using AeC.DeveloperChallenge.Clima.Repositorios.BancoDados.Interfaces;
using AeC.DeveloperChallenge.Clima.Repositorios.ExecutorBancoDados.Interfaces;

namespace AeC.DeveloperChallenge.Clima.Repositorios.BancoDados
{
    /// <summary>
    /// Contém as implementações referentes ao repositório de logs de requisições
    /// </summary>
    public class LogRequisicaoRepositorio : ILogRequisicaoRepositorio
    {
        #region Campos

        /// <summary>
        /// Executor de comandos assíncronos de banco de dados
        /// </summary>
        private readonly IExecutorBancoDados _executorBancoDados;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="executorBancoDados">Injeção de dependência do executor de comandos assíncronos de banco de dados</param>
        public LogRequisicaoRepositorio(IExecutorBancoDados executorBancoDados)
        {
            this._executorBancoDados = executorBancoDados;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Adiciona um novo log de requisição
        /// </summary>
        /// <param name="requisicao">Informações do log de requisição</param>
        public async Task AdicionaLogRequisicaoAsync(LogRequisicao logRequisicao)
        {
            var comandoSQL = "INSERT INTO LogRequisicao " +
                             "(CodigoRequisicao, Tipo, Data, Mensagem) " +
                             "VALUES (@CodigoRequisicao, @Tipo, @Data, @Mensagem)";

            await this._executorBancoDados.ExecutaAsync(comandoSQL, logRequisicao);
        }

        #endregion
    }
}