using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarefas.Data;
using Tarefas.Models;
using Tarefas.Services;

namespace Tarefas.Controllers
{
    public class TarefasController : Controller
    {
        private ITarefaItemService _tarefaService;

        public TarefasController(ITarefaItemService tarefaService)
        {
            _tarefaService = tarefaService;
        }


        //Lista de Tarefas
        public async Task<IActionResult> Index()
        {
            var tarefas = _tarefaService.GetItensAsync();
            return View(await tarefas);
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
                var isOk = await _tarefaService.AdicionarItem(tarefa);
                if (isOk)
                    return RedirectToAction("Index");

                return NotFound();
            }

            return View(tarefa);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var tarefa = await _tarefaService.GetItemAsync(id);

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
                try { await _tarefaService.EditarItem(tarefa); }
                catch { return NotFound(); }

                return RedirectToAction("Index");
            }

            return View(tarefa);
        }

        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            var retorno = false;

            try { retorno = await _tarefaService.DeletarItem(id); }
            catch { return NotFound(); }

            if(!retorno)
                return NotFound();

            return RedirectToAction("Index");

        }
    }
}