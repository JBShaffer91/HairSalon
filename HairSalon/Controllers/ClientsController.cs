using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HairSalon.Models;
using System.Linq;
using System.Threading.Tasks;

namespace HairSalon.Controllers
{
    public class ClientsController : Controller
    {
        private readonly HairSalonContext _db;

        public ClientsController(HairSalonContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var clients = await _db.Clients.Include(client => client.Stylist).ToListAsync();
            return View(clients);
        }

        public IActionResult Create()
        {
            ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Client client)
        {
            if (!ModelState.IsValid)
            {
                return View(client);
            }

            await _db.Clients.AddAsync(client);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = await _db.Clients
                .Include(c => c.Stylist)
                .FirstOrDefaultAsync(c => c.ClientId == id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = await _db.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "Name");
            return View(client);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Client client)
        {
            if (id != client.ClientId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Entry(client).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(client);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = await _db.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _db.Clients.FindAsync(id);
            _db.Clients.Remove(client);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _db.Clients.Any(e => e.ClientId == id);
        }
    }
}
