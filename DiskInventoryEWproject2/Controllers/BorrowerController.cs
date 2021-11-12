/*************************************************************************************
*  DATE:            NAME:       DESCRIPTION:
*  11-12-2021       Emma        Initial deployment of BORROWER controller & index view.
*************************************************************************************/


using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventoryEWproject2.Models;

namespace DiskInventoryEWproject2.Controllers
{
    public class BorrowerController : Controller
    {
        private disk_inventoryEWContext context { get; set; }
        public BorrowerController(disk_inventoryEWContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            List<Borrower> borrowers = context.Borrowers.OrderBy(b => b.BorrowerId).ThenBy(b => b.Fname).ThenBy(b => b.Lname).ThenBy(b => b.PhoneNum).ToList();
            return View(borrowers);
        }
    }
}
