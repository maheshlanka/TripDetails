using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using MyTrips.Models;
namespace MyTrips.Controllers
{
    public class TripController : Controller
    {
        // GET: Trip
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            using(DBModel1 db = new DBModel1())
            {
                List<Trip> tripList = db.Trips.ToList<Trip>();
                return Json(new {data = tripList}, JsonRequestBehavior.AllowGet); 

            }
        }
        [HttpGet]
        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Trip());
            }
            else
            {
                using (DBModel1 db = new DBModel1())
                {
                    return View(db.Trips.Where(x => x.Id==id).FirstOrDefault<Trip>());
                }
            }
        }
        
        [HttpPost]
        public ActionResult AddorEdit(Trip trp)
        {
            using (DBModel1 db = new DBModel1())
            {
                if(trp.Id ==0)
                {
                    db.Trips.Add(trp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    db.Entry(trp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);

                }
            }
                
        }
        [HttpPost]
        public ActionResult Delete(int id )
        {
            using (DBModel1 db = new DBModel1())
            {
                Trip trp = db.Trips.Where(x => x.Id == id).FirstOrDefault<Trip>();
                db.Trips.Remove(trp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);

            }

        }
    }
}