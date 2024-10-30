using System.Diagnostics;
using CrudNET8MVC.Data;
using Microsoft.AspNetCore.Mvc;
using CrudNET8MVC.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace CrudNET8MVC.Controllers;

public class InicioController : Controller
{
    private readonly ApplicationDbContext _contexto;
    
    public InicioController(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    [HttpGet]
    public async Task <IActionResult> Index()
    {
        return View(await _contexto.Contacto.ToListAsync());
    }
    
    [Authorize]
    [HttpGet]
    public IActionResult Crear()
    {
        return View();
    }
    
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(Contacto contacto)
    {
        if (!ModelState.IsValid) return View();
        _contexto.Contacto.Add(contacto);
        await _contexto.SaveChangesAsync();
        
        // Para envio de correo
        var mensaje = new MimeMessage();
        mensaje.From.Add(new MailboxAddress("Test envio email", "antonio.hs6991@gmail.com"));
        mensaje.To.Add(new MailboxAddress("Test enviado", "klee.collage@gmail.com"));
        mensaje.Subject = "Test email asp.net core";
        mensaje.Body = new TextPart("plain")
        {
            Text = "Hola saludos desde asp.net core"
        };
        using (var cliente = new SmtpClient())
        {
            cliente.Connect("smtp.gmail.com", 587); // para gmail el puerto puede ser 465 o 587
            cliente.Authenticate("antonio.hs6991@gmail.com", "qsfl xzsj zihm lqmp");
            cliente.Send(mensaje);
            cliente.Disconnect(true);
        }
        
        
        return RedirectToAction(nameof(Index));
    }
    
    [Authorize]
    [HttpGet]
    public IActionResult Editar(int? id)
    {
        if (id == null) return NotFound();
        
        var contacto = _contexto.Contacto.Find(id);
        if (contacto == null) return NotFound();
        
        return View(contacto);
    }
    
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(Contacto contacto)
    {
        if (!ModelState.IsValid) return View();
        _contexto.Update(contacto);
        await _contexto.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Detalle(int? id)
    {
        if (id == null) return NotFound();
        
        var contacto = _contexto.Contacto.Find(id);
        if (contacto == null) return NotFound();
        
        return View(contacto);
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [Authorize]
    [HttpGet]
    public IActionResult Borrar(int? id)
    {
        if (id == null) return NotFound();
        
        var contacto = _contexto.Contacto.Find(id);
        if (contacto == null) return NotFound();
        
        return View(contacto);
    }
    
    [HttpPost, ActionName("Borrar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BorrarContacto(int? id)
    {
        var contacto = await _contexto.Contacto.FindAsync(id);
        if (contacto == null) return View();
        /*Borrado*/
        _contexto.Contacto.Remove(contacto);
        await _contexto.SaveChangesAsync();
        
        return RedirectToAction(nameof(Index));
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}