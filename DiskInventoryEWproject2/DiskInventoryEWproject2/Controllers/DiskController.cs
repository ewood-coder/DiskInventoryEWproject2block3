/*************************************************************************************
*  DATE:            NAME:       DESCRIPTION:
*  11-12-2021       Emma        Initial deployment of DISK controller & index view.
*  11-19-2021       Emma        Added more post and get methods.
*  12-6-2021        Emma        Add stored procedure calls to insert/update/delete tables.
*************************************************************************************/


using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventoryEWproject2.Models;
using Microsoft.EntityFrameworkCore;

namespace DiskInventoryEWproject2.Controllers
{
    public class DiskController : Controller
    {
        private disk_inventoryEWContext context { get; set; }
        public DiskController(disk_inventoryEWContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            List<Disk> disks = context.Disks.OrderBy(c => c.CdName).Include(c => c.Genre).Include(c => c.Status).Include(c => c.DiskType).ToList();
            return View(disks);
        }
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.DiskTypes = context.DiskTypes.OrderBy(t => t.Description).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
            ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
            return View("Edit", new Disk());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.DiskTypes = context.DiskTypes.OrderBy(t => t.Description).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
            ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
            var disk = context.Disks.Find(id);
            return View(disk);
        }

        
        [HttpPost]
        public IActionResult Edit(Disk disk)
        {
            if (ModelState.IsValid)
            {
                if (disk.CdId == 0)   // means add the artist
                {
                    //context.Disks.Add(disk);
                    context.Database.ExecuteSqlRaw("execute sp_ins_disk @p0, @p1, @p2, @p3, @p4",
                        parameters: new[] { disk.CdName,
                                            disk.ReleaseDate.ToString(),
                                            disk.DiskTypeId.ToString(),
                                            disk.StatusId.ToString(),
                                            disk.GenreId.ToString()
                                            });
                }
                else                       // means update the artist
                {
                    //context.Disks.Update(disk);
                    context.Database.ExecuteSqlRaw("execute sp_upd_disk @p0, @p1, @p2, @p3, @p4, @p5",
                        parameters: new[] { disk.CdId.ToString(),
                                            disk.CdName,
                                            disk.ReleaseDate.ToString(),
                                            disk.DiskTypeId.ToString(),
                                            disk.StatusId.ToString(),
                                            disk.GenreId.ToString()
                                            });
                }
                //context.SaveChanges();
                return RedirectToAction("Index", "Disk");
            }
            else
            {
                ViewBag.Action = (disk.CdId == 0) ? "Add" : "Edit";
                ViewBag.DiskTypes = context.DiskTypes.OrderBy(t => t.Description).ToList();
                ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
                ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
                return View(disk);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var disk = context.Disks.Find(id);
            return View(disk);
        }
        [HttpPost]
        public IActionResult Delete(Disk disk)
        {
            //context.Disks.Remove(disk);
            //context.SaveChanges();
            context.Database.ExecuteSqlRaw("execute sp_del_disk @p0",
                        parameters: new[] { disk.CdId.ToString() });
            return RedirectToAction("Index", "Disk");
        }
    }
}
