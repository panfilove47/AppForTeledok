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
    public class FoundersController : Controller
    {
        private readonly ClientsContext _context;

        public FoundersController(ClientsContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            if (_context.Founder != null)
            {
                return View(await _context.Founder.ToListAsync());
            }
            else
            {
                return Problem("Учредителей не существует");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,INN,FIO,CreatedAt,LastUpdatedAt, ClientId")] Founder founder)
        {
            if (ModelState.IsValid)
            {
                if (ClientExists(founder.ClientId))
                {
                    if (!IsINNExists(founder.INN))
                    {
                        founder.CreatedAt = DateTime.Now;
                        _context.Add(founder);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(founder.INN), "Текущий ИНН уже есть в базе данных");
                    }
                }
                else 
                {
                    ModelState.AddModelError(nameof(founder.ClientId), "Юридического лица с таким id не существует");
                }
            }
            return View(founder);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Founder == null)
            {
                return NotFound();
            }

            var founder = await _context.Founder.FindAsync(id);
            if (founder == null)
            {
                return NotFound();
            }
            return View(founder);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,INN,FIO,CreatedAt,LastUpdatedAt, ClientId")] Founder founder)
        {
            if (id != founder.Id)
            {
                return NotFound();
            }

            var searchInn = await _context.Founder.FirstOrDefaultAsync(x => x.Id == id);
            _context.Entry(searchInn).State = EntityState.Detached;
            string INN = searchInn.INN;
            if (ModelState.IsValid)
            {
                if (ClientExists(founder.ClientId))
                {
                    if (!IsINNExists(founder.INN) || founder.INN == INN)
                    {
                        founder.LastUpdatedAt = DateTime.Now;
                        _context.Update(founder);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(founder.INN), "Текущий ИНН уже есть в базе данных");
                        return View(founder);
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(founder.ClientId), "Юридического лица с таким id не существует");
                    return View(founder);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(founder);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Founder == null)
            {
                return NotFound();
            }

            var founder = await _context.Founder
                .FirstOrDefaultAsync(m => m.Id == id);
            if (founder == null)
            {
                return NotFound();
            }

            return View(founder);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Founder == null)
            {
                return Problem("Учредителей не существует");
            }
            var founder = await _context.Founder.FindAsync(id);
            if (founder != null)
            {
                _context.Founder.Remove(founder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FounderExists(int id)
        {
          return (_context.Founder?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool IsINNExists(string INN)
        {
            return _context.Founder.Any(e => e.INN == INN);
        }
        private bool ClientExists(int id)
        {
            return _context.Client.Any(client => client.Id == id && client.ClientType == "Юридическое лицо");
        }
    }
}
