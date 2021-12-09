/*************************************************************************************
*  DATE:            NAME:       DESCRIPTION:
*  12-3-2021        Emma        Initial deployment of DiskHasBorrower controller & index view.
*  12-6-2021        Emma        Add stored procedure calls to insert/update tables.
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
    public class DiskHasBorrowerController : Controller
    {
        private disk_inventoryEWContext context { get; set; }
        public DiskHasBorrowerController(disk_inventoryEWContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var diskhasborrowers = context.DiskHasBorrowers.
                Include(d => d.Cd).OrderBy(d => d.Cd.CdName).        // Add sort here!!!
                Include(b => b.Borrower).OrderBy(b => b.Borrower.Lname).ThenBy(b => b.Borrower.Fname).ToList();
            return View(diskhasborrowers);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();
            ViewBag.Disks = context.Disks.OrderBy(d => d.CdName).ToList();
            DiskHasBorrower newcheckout = new DiskHasBorrower();
            newcheckout.BorrowedDate = DateTime.Today;
            return View("Edit", newcheckout);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();
            ViewBag.Disks = context.Disks.OrderBy(d => d.CdName).ToList();
            var diskhasborrower = context.DiskHasBorrowers.Find(id);
            return View(diskhasborrower);
        }
        [HttpPost]
        public IActionResult Edit(DiskHasBorrower diskhasborrower)
        {
            if (ModelState.IsValid)
            {
                string returnedDate = diskhasborrower.ReturnedDate.ToString();
                returnedDate = (returnedDate == "") ? null : diskhasborrower.ReturnedDate.ToString();

                if (diskhasborrower.DiskHasBorrowerId == 0)   // means add the borrower
                {
                    //context.DiskHasBorrowers.Add(diskhasborrower);
                    context.Database.ExecuteSqlRaw("execute sp_ins_disk_has_borrower @p0, @p1, @p2, @p3",
                        parameters: new[] { diskhasborrower.BorrowerId.ToString(), 
                                            diskhasborrower.CdId.ToString(), 
                                            diskhasborrower.BorrowedDate.ToString(),
                                            //diskhasborrower.ReturnedDate.ToString() 
                                            returnedDate
                                            });
                }
                else                       // means update the borrower
                {
                    //context.DiskHasBorrowers.Update(diskhasborrower);
                    context.Database.ExecuteSqlRaw("execute sp_upd_disk_has_borrower @p0, @p1, @p2, @p3, @p4",
                        parameters: new[] { diskhasborrower.DiskHasBorrowerId.ToString(),
                                            diskhasborrower.BorrowerId.ToString(),
                                            diskhasborrower.CdId.ToString(),
                                            diskhasborrower.BorrowedDate.ToString(),
                                            //diskhasborrower.ReturnedDate.ToString()
                                            returnedDate
                                            });
                }
                //context.SaveChanges();
                return RedirectToAction("Index", "DiskHasBorrower");
            }
            else
            {
                ViewBag.Action = (diskhasborrower.DiskHasBorrowerId == 0) ? "Add" : "Edit";
                ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();
                ViewBag.Disks = context.Disks.OrderBy(d => d.CdName).ToList();
                return View(diskhasborrower);
            }
        }
    }
}
