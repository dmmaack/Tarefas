using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarefas.Data;
using Tarefas.Models;

namespace Tarefas.Controllers
{
    public class TarefasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TarefasController(ApplicationDbContext context)
        {
            _context = context;
        }


        //Lista de Tarefas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tarefas.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Nome,DataConclusao,EstaCompleta")] TarefaItem tarefa)
        {
            if (ModelState.IsValid)
            {
                this._context.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tarefa);
        }
    }
}