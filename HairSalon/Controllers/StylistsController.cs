using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HairSalon.Models;
using System.Linq;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    private readonly HairSalonContext _db;

    public StylistsController(HairSalonContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      var stylists = _db.Stylists;
      return View(stylists == null ? new List<Stylist>() : stylists.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Stylist stylist)
    {
      _db.Stylists.Add(stylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Stylist thisStylist = _db.Stylists
      .Include(stylist => stylist.Clients)
      .FirstOrDefault(stylist => stylist.StylistId == id);

      if (thisStylist == null)
      {
          return NotFound();
      }

      return View(thisStylist);
    }
  }
}
