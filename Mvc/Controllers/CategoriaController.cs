using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dados;
using Dominio.Entidades;

namespace Mvc.Controllers
{
    [Route("Categoria")]
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public CategoriaController(ApplicationDbContext context) {
            _contexto = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var categorias = _contexto.Categorias.ToList();
            return View(categorias);
        }

        [HttpGet("Salvar")]
        public IActionResult Salvar(){
            return View();
        }
        [HttpPost("Salvar")]
        public async Task<IActionResult> Salvar(Categoria modelo){

            if (modelo.Id == 0){
                _contexto.Categorias.Add(modelo);
            }
            else
            {
                var categoria = _contexto.Categorias.First(c => c.Id == modelo.Id);
                categoria.Nome = modelo.Nome;
            }
            await _contexto.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet("Editar")]
        public IActionResult Editar(int id)
        {
            var categoria = _contexto.Categorias.First(c => c.Id == id);
            return View("Salvar",categoria);
        }

        [HttpGet("Deletar")]
        public async Task<IActionResult> Deletar(int id)
        {
            var categoria = _contexto.Categorias.First(c => c.Id == id);
            _contexto.Categorias.Remove(categoria);
            await _contexto.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
