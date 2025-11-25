// Controllers/EmpresasController.cs
using Microsoft.AspNetCore.Mvc;
using ApiEmpresas.Data; // Contexto de acesso ao banco de dados
using Microsoft.EntityFrameworkCore;
using ApiEmpresas.Models; // Model da entidade Empresa

namespace ApiEmpresas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Construtor: Utilizando Injeção de Dependência para obter o DbContext
        public EmpresasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Empresas
        // Endpoint para consultar todas as empresas.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
        {
            // Retorna a lista completa de Empresas do contexto
            return await _context.Empresas.ToListAsync();
        }

        // POST: api/Empresas
        // Endpoint para criar um novo registro de Empresa.
        [HttpPost]
        public async Task<ActionResult<Empresa>> PostEmpresa(Empresa empresa)
        {
            // Adiciona a nova empresa ao contexto do Entity Framework
            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync(); // Persiste a mudança no MySQL

            // Retorna o Status 201 Created com o recurso criado e sua URI
            return CreatedAtAction(nameof(GetEmpresas), new { id = empresa.Id }, empresa);
        }
        
        // PUT: api/Empresas/5
        // Endpoint para atualizar um registro existente de Empresa.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpresa(int id, Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return BadRequest(); // Status 400: Erro se os IDs não coincidirem
            }

            // Define o estado da entidade como Modificado para que o EF a atualize
            _context.Entry(empresa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Trata exceção de concorrência: verifica se a entidade existe antes de falhar
                if (!_context.Empresas.Any(e => e.Id == id))
                {
                    return NotFound(); // Retorna 404 Not Found se a Empresa não existir
                }
                else
                {
                    throw; // Lança exceção para outros erros
                }
            }

            return NoContent(); // Retorna 204 No Content para indicar sucesso sem corpo de resposta
        }

        // DELETE: api/Empresas/5
        // Endpoint para remover um registro de Empresa pelo ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound(); // Retorna 404 Not Found caso o ID não seja encontrado
            }
            
            // Remove o objeto do contexto e salva a exclusão no banco
            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
            
            return NoContent(); // Confirma a exclusão com 204 No Content
        }
    }
}