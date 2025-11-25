// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using ApiEmpresas.Models; // Referencia os modelos de dados (Empresa e Funcionario)
using System.Text.Json.Serialization;

namespace ApiEmpresas.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Construtor padrão do DbContext, recebe as opções de configuração
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Mapeamento das coleções para tabelas no banco de dados (DbSets)
        public required DbSet<Empresa> Empresas { get; set; }
        public required DbSet<Funcionario> Funcionarios { get; set; }

        // Configuração da modelagem de dados e relacionamentos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Funcionario>()
                .HasOne(f => f.Empresa)             // Um Funcionário tem UMA Empresa
                .WithMany(e => e.Funcionarios)      // Uma Empresa tem MUITOS Funcionários
                .HasForeignKey(f => f.EmpresaId)    // Define EmpresaId como Chave Estrangeira (FK)
                .IsRequired();                      // Define a Chave Estrangeira como obrigatória
        }
    }
}