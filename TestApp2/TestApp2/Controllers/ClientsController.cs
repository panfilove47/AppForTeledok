using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;
using TestApp2.Data;

namespace TestApp2.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ClientsContext _context;

        public ClientsController(ClientsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (_context.Client != null)
            {
                return View(await _context.Client.ToListAsync());
            }
            else
            {
                return Problem("Клиентов не существует");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,INN,Name,ClientType,CreatedAt,LastUpdatedAt")] Client client)
        {
            if (ModelState.IsValid)
            {
                if (!IsINNExists(client.INN))
                {
                    client.CreatedAt = DateTime.Now;
                    _context.Add(client);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(nameof(client.INN), "Текущий ИНН уже есть в базе данных");
                }
            }
            return View(client);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,INN,Name,ClientType,CreatedAt,LastUpdatedAt")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }
            var searchInn = await _context.Client.FirstOrDefaultAsync(x => x.Id == id);
            _context.Entry(searchInn).State = EntityState.Detached;
            string INN = searchInn.INN;
            if (ModelState.IsValid)
            {
                if (!IsINNExists(client.INN) || client.INN == INN)
                {
                    client.LastUpdatedAt = DateTime.Now;
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError(nameof(client.INN), "Текущий ИНН уже есть в базе данных");
                    return View(client);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Client == null)
            {
                return Problem("Клиентов не существует");
            }
            var client = await _context.Client.FindAsync(id);
            if (client != null)
            {
                _context.Client.Remove(client);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
          return (_context.Client?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool IsINNExists(string INN)
        {
            return _context.Client.Any(e => e.INN == INN);
        }
    }
}
