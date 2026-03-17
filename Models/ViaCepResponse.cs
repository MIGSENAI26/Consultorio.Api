namespace Consultorio.Api.Models
{
    public record ViaCepResponse
    {
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
    }
}
