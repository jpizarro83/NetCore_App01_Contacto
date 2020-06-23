using Microsoft.EntityFrameworkCore;
namespace Contactos.Models
{
    public class ContextDB: DbContext{

        public ContextDB()
        {
        }
        public ContextDB(DbContextOptions<ContextDB> options): base (options){

        }

        public DbSet<Contacto> Contactos{get;set;}
    }
}