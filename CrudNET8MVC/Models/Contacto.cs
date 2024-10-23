using System.ComponentModel.DataAnnotations;

namespace CrudNET8MVC.Models;

public class Contacto
{
    // [Key] // EntityFrameworkCore identifica la palabra id automaticamente como Key
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; } // prop <- shortcut
    
    [Required(ErrorMessage = "El telefono es obligatorio")]
    public string Telefono { get; set; }
    
    [Required(ErrorMessage = "El celular es obligatorio")]
    public string Celular { get; set; }
    
    [Required(ErrorMessage = "El email es obligatorio")]
    public string Email { get; set; }
    
    public DateTime FechaCreacion { get; set; }
    
    
}