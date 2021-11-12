/*************************************************************************************
*  DATE:            NAME:       DESCRIPTION:
*  11-12-2021       Emma        Initial deployment of ARTIST controller & index view.
*************************************************************************************/


using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            List<Artist> artists = context.Artists.OrderBy(a => a.ArtistName).ToList();
            return View(artists);
        }
    }
}
