namespace AeC.DeveloperChallenge.Clima.Modelos.Entidades
{
    /// <summary>
    /// Contém os dados de log de uma requisição
    /// </summary>
    public class LogRequisicao
    {
        #region Propriedades

        /// <summary>
        /// Código de log de requisição
        /// </summary>
        public long Codigo { get; set; }

        /// <summary>
        /// Código da requisição
        /// </summary>
        public long CodigoRequisicao { get; set; }

        /// <summary>
        /// Tipo do log
        /// </summary>
        public string Tipo { get; set; } = null!;

        /// <summary>
        /// Mensagem
        /// </summary>
        public string Mensagem { get; set; } = null!;

        #endregion
    }
}