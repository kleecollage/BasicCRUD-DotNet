using CrudNET8MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudNET8MVC.Data;

public class ApplicationDbContext: DbContext
{
    /* ctor <- constructor shortcut  */
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }
    // Agregar los modelos aqui (Cada modelo correponde a una tabla en la BD)
    public DbSet<Contacto> Contacto { get; set; }
    
    
}