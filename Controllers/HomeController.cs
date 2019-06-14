using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tarefas.Data;
using Tarefas.Models;

namespace Tarefas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
