using Microsoft.EntityFrameworkCore;
using Dominio.Entidades; 
namespace Dados
{
    public class ApplicationDbContext : DbContext
    {            
        string conection = @"Password=123456;Persist Security Info=True;User ID=sa;Initial Catalog=master;Data Source=DESKTOP-IQFSBF6\SQLEXPRESS";
            
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options){

        } 
        
 
 
        public DbSet<Categoria> Categorias{get; set;}
        public DbSet<Produto> Produtos{get; set;} 
    }
}