using System.Text.Json.Serialization;

namespace AeC.DeveloperChallenge.Clima.Modelos.DTOs
{
    /// <summary>
    /// Contém as informações de detalhes de previsão de clima de cidade
    /// </summary>
    public class DetalhesClimaCidadeDTO
    {
        #region Propriedades

        /// <summary>
        /// Data da previsão
        /// </summary>
        [JsonPropertyName("data")]
        public DateTime Data { get; set; }

        /// <summary>
        /// Código da condição do clima
        /// </summary>
        [JsonPropertyName("condicao")]
        public string CodigoCondicao { get; set; } = null!;

        /// <summary>
        /// Descrição da condição do clima
        /// </summary>
        [JsonPropertyName("condicao_desc")]
        public string DescricaoCondicao { get; set; } = null!;

        /// <summary>
        /// Temperatura mínima em °C
        /// </summary>
        [JsonPropertyName("min")]
        public int TemperaturaMinima { get; set; }

        /// <summary>
        /// Temperatura máxima em °C
        /// </summary>
        [JsonPropertyName("max")]
        public int TemperaturaMaxima { get; set; }

        /// <summary>
        /// Medida de índice de ultravioleta
        /// </summary>
        [JsonPropertyName("indice_uv")]
        public int IndiceUV { get; set; }

        #endregion
    }
}