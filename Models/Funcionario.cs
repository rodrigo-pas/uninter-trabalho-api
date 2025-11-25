// Models/Funcionario.cs
using System.ComponentModel.DataAnnotations;

namespace ApiEmpresas.Models
{
    public class Funcionario
    {
        // Chave Primária
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do funcionário é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        // Chave Estrangeira (FK) que liga este funcionário a uma Empresa
        [Required(ErrorMessage = "O funcionário deve estar associado a uma Empresa.")]
        public int EmpresaId { get; set; }

        // Propriedade de Navegação para a Empresa associada (Facilita consultas)
        public Empresa? Empresa { get; set; }

        [Range(1000.00, 50000.00, ErrorMessage = "O salário deve estar entre R$ 1000 e R$ 50000.")]
        public decimal Salario { get; set; }
    }
}