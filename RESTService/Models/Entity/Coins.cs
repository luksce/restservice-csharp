using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace RESTService.Models.Entity
{
    public class Coins
    {
        [System.Text.Json.Serialization.JsonIgnore]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [JsonProperty("moeda")]
        public string Moeda { get; set; }

        [JsonProperty("data_inicio")]
        public DateTime DataInicio { get; set; }

        [JsonProperty("data_fim")]
        public DateTime DataFim { get; set; }

        public string Mensagem { get; set; }

    }
}
