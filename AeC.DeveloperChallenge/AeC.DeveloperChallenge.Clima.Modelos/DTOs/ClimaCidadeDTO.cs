using System.Text.Json.Serialization;

namespace AeC.DeveloperChallenge.Clima.Modelos.DTOs
{
    /// <summary>
    /// Contém as informações de previsão de clima de cidade
    /// </summary>
    public class ClimaCidadeDTO
    {
        #region Propriedades

        /// <summary>
        /// Nome da cidade
        /// </summary>
        [JsonPropertyName("cidade")]
        public string Cidade { get; set; } = null!;

        /// <summary>
        /// UF da cidade
        /// </summary>
        [JsonPropertyName("estado")]
        public string UF { get; set; } = null!;

        /// <summary>
        /// Data de atualização
        /// </summary>
        [JsonPropertyName("atualizado_em")]
        public DateTime DataAtualizacao { get; set; }

        /// <summary>
        /// Detalhes da previsão do clima
        /// </summary>
        [JsonPropertyName("clima")]
        public List<DetalhesClimaCidadeDTO> Detalhes { get; set; } = null!;

        #endregion
    }
}