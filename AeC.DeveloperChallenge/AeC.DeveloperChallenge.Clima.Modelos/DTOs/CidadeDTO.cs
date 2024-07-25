using System.Text.Json.Serialization;

namespace AeC.DeveloperChallenge.Clima.Modelos.DTOs
{
    /// <summary>
    /// Contém as informações de uma cidade
    /// </summary>
    public class CidadeDTO
    {
        #region Propriedades

        /// <summary>
        /// Código da cidade
        /// </summary>
        [JsonPropertyName("id")]
        public int Codigo { get; set; }

        /// <summary>
        /// Nome da cidade
        /// </summary>
        [JsonPropertyName("nome")]
        public string Nome { get; set; } = null!;

        /// <summary>
        /// UF da cidade
        /// </summary>
        [JsonPropertyName("estado")]
        public string UF { get; set; } = null!;

        #endregion
    }
}