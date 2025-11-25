// Models/Empresa.cs
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization; // Necessário para o atributo [JsonIgnore]

namespace ApiEmpresas.Models
{
    public class Empresa
    {
        // Chave Primária
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da empresa é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CNPJ da empresa é obrigatório.")]
        [StringLength(18, MinimumLength = 14, ErrorMessage = "CNPJ inválido.")]
        public string Cnpj { get; set; } = string.Empty;

        // Relacionamento 1:N com Funcionário (Propriedade de Navegação)
        // [JsonIgnore] é usado para evitar ciclos infinitos na serialização JSON.
        [JsonIgnore] 
        public ICollection<Funcionario>? Funcionarios { get; set; }
    }
}