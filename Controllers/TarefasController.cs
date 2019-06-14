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

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var tarefa = await _context.Tarefas.SingleOrDefaultAsync(s => s.Id == id);

            if (tarefa == null)
                return NotFound();

            return View(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Nome,DataConclusao,EstaCompleta")] TarefaItem tarefa)
        {
            if (tarefa == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if(!ContatoExiste(tarefa.Id))
                        return NotFound();
                    else
                        throw ex;
                }

                return RedirectToAction("Index");
            }

            return View(tarefa);
        }

        private bool ContatoExiste(Guid id){
            return _context.Tarefas.Any(a => a.Id == id);
        }

        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> Delete (Guid? id)
        {
            var tarefa = await _context.Tarefas.SingleOrDefaultAsync(s => s.Id == id);

            if(tarefa == null)
                return NotFound();
            
            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }
    }
}