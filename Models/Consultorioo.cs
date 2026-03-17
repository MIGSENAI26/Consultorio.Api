using System.ComponentModel.DataAnnotations;

namespace Consultorio.Api.Models
{
    public class Consultorioo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [Required] public string Cep { get; set; }
        public string? Logradouro { get; set; }
        public string? Bairro { get; set; }
        public string? Localidade { get; set; }
        public string? Uf { get; set; }
        public string Numero { get; set; }
    }
}
