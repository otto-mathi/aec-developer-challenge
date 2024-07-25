namespace AeC.DeveloperChallenge.Clima.Modelos.Entidades
{
    /// <summary>
    /// Contém os dados de uma requisição
    /// </summary>
    public class Requisicao
    {
        #region Propriedades

        /// <summary>
        /// Código da requisição
        /// </summary>
        public long Codigo { get; set; }

        /// <summary>
        /// Nome da controller de origem
        /// </summary>
        public string ControllerOrigem { get; set; } = null!;

        /// <summary>
        /// Nome do método de origem
        /// </summary>
        public string MetodoOrigem { get; set; } = null!;

        /// <summary>
        /// Método HTTP
        /// </summary>
        public string MetodoHTTP { get; set; } = null!;

        /// <summary>
        /// Data
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// Parâmetros de entrada da requisição
        /// </summary>
        public string Parametros { get; set; } = null!;

        /// <summary>
        /// Retorno da requisição
        /// </summary>
        public string Retorno { get; set; } = null!;

        #endregion
    }
}