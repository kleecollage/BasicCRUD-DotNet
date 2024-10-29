using CrudNET8MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrudNET8MVC.Data;

public class ApplicationDbContext: IdentityDbContext
{
    /* ctor <- constructor shortcut  */
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }
    // Agregar los modelos aqui (Cada modelo correponde a una tabla en la BD)
    public DbSet<Contacto> Contacto { get; set; }
    
    
}