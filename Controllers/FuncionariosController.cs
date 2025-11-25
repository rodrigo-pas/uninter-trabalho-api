// Controllers/FuncionariosController.cs
using Microsoft.AspNetCore.Mvc;
using ApiEmpresas.Data; // Contexto de acesso ao banco de dados (EF Core)
using Microsoft.EntityFrameworkCore;
using ApiEmpresas.Models; // Model da entidade Funcionario

namespace ApiEmpresas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Construtor: Utilizando Injeção de Dependência para obter o DbContext
        public FuncionariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Funcionarios
        // Endpoint para consultar a lista completa de funcionários.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            // Inclui a propriedade de navegação 'Empresa' na consulta para trazer dados completos (JOIN implícito)
            return await _context.Funcionarios.Include(f => f.Empresa).ToListAsync();
        }

        // POST: api/Funcionarios
        // Endpoint para criar um novo registro de Funcionario.
        [HttpPost]
        public async Task<ActionResult<Funcionario>> PostFuncionario(Funcionario funcionario)
        {
            // Validação de Chave Estrangeira (FK): Garante que a EmpresaId existe antes de salvar
            if (!_context.Empresas.Any(e => e.Id == funcionario.EmpresaId))
            {
                // Retorna 404 Not Found se a EmpresaId for inválida.
                return NotFound("EmpresaId inválido. A empresa associada não foi encontrada.");
            }

            _context.Funcionarios.Add(funcionario); // Adiciona o novo funcionário ao contexto
            await _context.SaveChangesAsync(); // Persiste a mudança no MySQL

            // Retorna o Status 201 Created com o recurso criado e sua URI
            return CreatedAtAction(nameof(GetFuncionarios), new { id = funcionario.Id }, funcionario);
        }

        // PUT: api/Funcionarios/5
        // Endpoint para atualizar um registro existente de Funcionario.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuncionario(int id, Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return BadRequest(); // Status 400: Erro se os IDs não coincidirem
            }

            // Validação de FK: Garante que a nova EmpresaId existe antes de atualizar
            if (!_context.Empresas.Any(e => e.Id == funcionario.EmpresaId))
            {
                return NotFound("EmpresaId inválido. A empresa associada não foi encontrada.");
            }

            // Define o estado da entidade como Modificado para que o EF a atualize
            _context.Entry(funcionario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Trata exceção de concorrência: verifica se a entidade existe
                if (!_context.Funcionarios.Any(f => f.Id == id))
                {
                    return NotFound(); // Retorna 404 Not Found se o Funcionario não existir
                }
                else
                {
                    throw; // Lança exceção para outros erros
                }
            }

            return NoContent(); // Retorna 204 No Content para sucesso na atualização
        }

        // DELETE: api/Funcionarios/5
        // Endpoint para remover um registro de Funcionario pelo ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound(); // Retorna 404 Not Found caso o ID não seja encontrado
            }

            _context.Funcionarios.Remove(funcionario); // Remove o objeto do contexto
            await _context.SaveChangesAsync(); // Persiste a exclusão

            return NoContent(); // Confirma a exclusão com 204 No Content
        }
    }
}