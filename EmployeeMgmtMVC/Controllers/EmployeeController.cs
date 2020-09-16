using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeMgmtMVC.Models;
using System.Data.Entity;

namespace EmployeeMgmtMVC.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (Ka14empdbEntities db = new Ka14empdbEntities())
            {
                List<employpersonal> empList = db.employpersonals.ToList<employpersonal>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        } 
        
        [HttpGet]
        public ActionResult AddorEdit(int id=0)
        {
            if (id == 0)
            {
                return View(new employpersonal());
            }

            else
            {
                using(Ka14empdbEntities db = new Ka14empdbEntities())
                {
                    return View(db.employpersonals.Where(x => x.id == id).FirstOrDefault<employpersonal>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddorEdit(employpersonal emp)
        {
            using(Ka14empdbEntities db = new Ka14empdbEntities())
            {
                if(emp.id == 0)
                {
                    db.employpersonals.Add(emp);
                    db.SaveChanges();
                    return Json( new { success = true, message = "Employee Added successfully"}, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Employee updated successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using(Ka14empdbEntities db = new Ka14empdbEntities())
            {
                employpersonal emp = db.employpersonals.Where(x => x.id == id).FirstOrDefault<employpersonal>();
                db.employpersonals.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Succesfully " }, JsonRequestBehavior.AllowGet);

            }
        }

    }
}