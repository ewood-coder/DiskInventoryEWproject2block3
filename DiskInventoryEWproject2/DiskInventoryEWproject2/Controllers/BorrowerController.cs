/*************************************************************************************
*  DATE:            NAME:       DESCRIPTION:
*  11-12-2021       Emma        Initial deployment of BORROWER controller & index view.
*  12-3-2021        Emma        Added Add action for borrower.
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
                    //context.Borrowers.Add(borrower);
                    context.Database.ExecuteSqlRaw("execute sp_ins_borrower @p0, @p1, @p2",
                        parameters: new[] { borrower.Fname, borrower.Lname, borrower.PhoneNum.ToString() });
                }
                else                       // means update the borrower
                {
                    //context.Borrowers.Update(borrower);
                    context.Database.ExecuteSqlRaw("execute sp_upd_borrower @p0, @p1, @p2, @p3",
                        parameters: new[] { borrower.BorrowerId.ToString(), borrower.Fname, borrower.Lname, borrower.PhoneNum.ToString() });
                }
                //context.SaveChanges();
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
            //context.Borrowers.Remove(borrower);
            //context.SaveChanges();
            context.Database.ExecuteSqlRaw("execute sp_del_borrower @p0",
                        parameters: new[] { borrower.BorrowerId.ToString() });
            return RedirectToAction("Index", "Borrower");
        }
    }
}
