/*************************************************************************************
*  DATE:            NAME:       DESCRIPTION:
*  11-12-2021       Emma        Initial deployment of BORROWER controller & index view.
*  12-3-2021        Emma        Added Add action for borrower.
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
            //List<Borrower> borrowers = context.Borrowers.OrderBy(b => b.BorrowerId).ThenBy(b => b.Fname).ThenBy(b => b.Lname).ThenBy(b => b.PhoneNum).ToList();
            List<Borrower> borrowers = context.Borrowers.OrderBy(b => b.Lname).ThenBy(b => b.Fname).ThenBy(b => b.BorrowerId).ThenBy(b => b.PhoneNum).ToList();
            return View(borrowers);
        }
        [HttpGet]
        public  IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Borrower());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }
        [HttpPost]
        public IActionResult Edit(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                if (borrower.BorrowerId == 0)   // means add the borrower
                {
                    context.Borrowers.Add(borrower);
                }
                else                       // means update the borrower
                {
                    context.Borrowers.Update(borrower);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Borrower");
            }
            else
            {
                ViewBag.Action = (borrower.BorrowerId == 0) ? "Add" : "Edit";
                return View(borrower);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }
        [HttpPost]
        public IActionResult Delete(Borrower borrower)
        {
            context.Borrowers.Remove(borrower);
            context.SaveChanges();
            return RedirectToAction("Index", "Borrower");
        }
    }
}
