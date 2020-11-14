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
    [Route("Produto")]
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produtos.Include(p => p.Categoria).ToListAsync());
        } 
         
        public IActionResult Salvar()
        {
            ViewBag.Categorias = _context.Categorias.ToList();
            return View();
        }

 
        [HttpPost("Salvar")]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Salvar(Produto modelo)
        {

            if (modelo.Id == 0)
            {
                _context.Produtos.Add(modelo);
            }
            else
            {
                var produto = _context.Produtos.First(c => c.Id == modelo.Id);
                produto.Nome = modelo.Nome;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet("Editar")]
        public IActionResult Editar(int id)
        {

            var produto = _context.Produtos.First(c => c.Id == id);
            return View("Salvar", produto);
        }

        [HttpGet("Deletar")]
        public async Task<IActionResult> Deletar(int id)
        {
            var produto = _context.Produtos.First(c => c.Id == id);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
