using AeC.DeveloperChallenge.Clima.Modelos.Entidades;
using AeC.DeveloperChallenge.Clima.Repositorios.BancoDados.Interfaces;
using AeC.DeveloperChallenge.Clima.Repositorios.ExecutorBancoDados.Interfaces;

namespace AeC.DeveloperChallenge.Clima.Repositorios.BancoDados
{
    /// <summary>
    /// Contém as implementações referentes ao repositório de requisições
    /// </summary>
    public class RequisicaoRepositorio : IRequisicaoRepositorio
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
        public RequisicaoRepositorio(IExecutorBancoDados executorBancoDados)
        {
            this._executorBancoDados = executorBancoDados;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Adiciona uma nova requisição
        /// </summary>
        /// <param name="requisicao">Informações da requisição</param>
        /// <returns>Código da requisição criada</returns>
        public async Task<long> AdicionaRequisicaoAsync(Requisicao requisicao)
        {
            var comandoSQL = "INSERT INTO Requisicao " +
                             "(ControllerOrigem, MetodoOrigem, MetodoHttp, Data, Parametros, Retorno) " +
                             "VALUES (@ControllerOrigem, @MetodoOrigem, @MetodoHttp, @Data, @Parametros, @Retorno); " +
                             "SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";

            return await this._executorBancoDados.ConsultaUnicoValorAsync<long>(comandoSQL, requisicao);
        }

        #endregion
    }
}