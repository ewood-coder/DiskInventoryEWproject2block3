/*************************************************************************************
*  DATE:            NAME:       DESCRIPTION:
*  11-12-2021       Emma        Initial deployment of DISK controller & index view.
*************************************************************************************/


using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventoryEWproject2.Models;

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
            List<Disk> disks = context.Disks.OrderBy(c => c.CdId).ThenBy(c => c.CdName).ThenBy(c => c.ReleaseDate).ThenBy(c => c.GenreId).ThenBy(c => c.StatusId).ThenBy(c => c.DiskTypeId).ToList();
            return View(disks);
        }
    }
}
