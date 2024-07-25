using System.Text.Json.Serialization;

namespace AeC.DeveloperChallenge.Clima.Modelos.DTOs
{
    /// <summary>
    /// Contém as informações de previsão de clima de aeroporto
    /// </summary>
    public class ClimaAeroportoDTO
    {
        #region Propriedades

        /// <summary>
        /// ICAO do aeroporto
        /// </summary>
        [JsonPropertyName("codigo_icao")]
        public string Icao { get; set; } = null!;

        /// <summary>
        /// Data de atualização
        /// </summary>
        [JsonPropertyName("atualizado_em")]
        public DateTime DataAtualizacao { get; set; }

        /// <summary>
        /// Pressão atmosférica
        /// </summary>
        [JsonPropertyName("pressao_atmosferica")]
        public int PressaoAtmosferica { get; set; }

        /// <summary>
        /// Visibilidade
        /// </summary>
        [JsonPropertyName("visibilidade")]
        public string Visibilidade { get; set; } = null!;

        /// <summary>
        /// Vento
        /// </summary>
        [JsonPropertyName("vento")]
        public int Vento { get; set; }

        /// <summary>
        /// Direção do vento
        /// </summary>
        [JsonPropertyName("direcao_vento")]
        public int DirecaoVento { get; set; }

        /// <summary>
        /// Umidade
        /// </summary>
        [JsonPropertyName("umidade")]
        public int Umidade { get; set; }

        /// <summary>
        /// Sigla da condição
        /// </summary>
        [JsonPropertyName("condicao")]
        public string SiglaCondicao { get; set; } = null!;

        /// <summary>
        /// Descrição da condição
        /// </summary>
        [JsonPropertyName("condicao_desc")]
        public string DescricaoCondicao { get; set; } = null!;

        /// <summary>
        /// Temperatura
        /// </summary>
        [JsonPropertyName("temp")]
        public int Temperatura { get; set; }

        #endregion
    }
}