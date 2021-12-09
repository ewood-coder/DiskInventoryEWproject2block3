/*************************************************************************************
*  DATE:            NAME:       DESCRIPTION:
*  11-12-2021       Emma        Initial deployment of ARTIST controller & index view.
*  11-19-2021       Emma        Added edit & delete button functions.
*  12-6-2021        Emma        Add stored procedure calls to insert/update/delete tables.
*************************************************************************************/


using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiskInventoryEWproject2.Models;

namespace DiskInventoryEWproject2.Controllers
{
    public class ArtistController : Controller
    {
        private disk_inventoryEWContext context { get; set; }
        public ArtistController(disk_inventoryEWContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            //List<Artist> artists = context.Artists.OrderBy(a => a.ArtistName).ToList();
            var artists = context.Artists.OrderBy(a => a.ArtistName).Include(at => at.ArtistType).ToList();
            return View(artists);
        }
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.Description).ToList();
            return View("Edit", new Artist());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.Description).ToList();
            var artist = context.Artists.Find(id);
            return View(artist);
        }
        [HttpPost]
        public IActionResult Edit(Artist artist)
        {
            if (ModelState.IsValid)
            {
                if (artist.ArtistId == 0)   // means add the artist
                {
                    //context.Artists.Add(artist);
                    context.Database.ExecuteSqlRaw("execute sp_ins_artist @p0, @p1",
                        parameters: new[] { artist.ArtistName, artist.ArtistTypeId.ToString() });
                }
                else                       // means update the artist
                {
                    //context.Artists.Update(artist);
                    context.Database.ExecuteSqlRaw("execute sp_upd_artist @p0, @p1, @p2",
                        parameters: new[] { artist.ArtistId.ToString(), artist.ArtistName, artist.ArtistTypeId.ToString() });
                }
                //context.SaveChanges();
                return RedirectToAction("Index", "Artist");
            }
            else
            {
                ViewBag.Action = (artist.ArtistId == 0) ? "Add" : "Edit";
                ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.Description).ToList();
                return View(artist);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var artist = context.Artists.Find(id);
            return View(artist);
        }
        [HttpPost]
        public IActionResult Delete(Artist artist)
        {
            //context.Artists.Remove(artist);
            //context.SaveChanges();
            context.Database.ExecuteSqlRaw("execute sp_del_artist @p0",
                parameters: new[] { artist.ArtistId.ToString() });
            return RedirectToAction("Index", "Artist");
        }
    }
}
