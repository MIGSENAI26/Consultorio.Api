using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Consultorio.Api.Models
{
    public class Medico
    {
        public int Id { get; set; }
        [Required] public string? Nome { get; set; }
        [Required] public string Crm { get; set; }
        public int ConsultoriooId { get; set; }
        [JsonIgnore]
        public Consultorioo? Consultorio { get; set; }
    }
}
