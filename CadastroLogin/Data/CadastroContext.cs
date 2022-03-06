using CadastroLogin.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CadastroLogin.Data
{
    public class CadastroContext : DbContext
    {
        public CadastroContext(DbContextOptions<CadastroContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }


    }
}
