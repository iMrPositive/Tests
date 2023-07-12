using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_CarSells_Project_3.Models;

namespace WebApp_CarSells_Project_3.Controllers
{
    public class CarController : Controller
    {
        //
        // GET: /Car/

        CarDatabaseEntities CS = new CarDatabaseEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CarStore()
        {
            return View(CS.Tables.ToList());
        }


        public ActionResult AddCar()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddCar(Table c)
        {
            if (c.ImageFile != null)
            {
                string carFileName = c.ImageFile.FileName;
                c.ImageFile.SaveAs(Server.MapPath("~/CarImages/" + carFileName));
                c.CarImgPath = "~/CarImages/" + carFileName;
            }
            CS.Tables.Add(c);
            CS.SaveChanges();
            return RedirectToAction("CarStore");
        }

        public ActionResult Delete(int? id)
        {
            Table CarD = CS.Tables.Find(id);
            if (CarD == null)
            {
                return HttpNotFound();
            }
            else
            {
                CS.Tables.Remove(CarD);
                CS.SaveChanges();
                return RedirectToAction("CarStore");
            }
            
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return Content("Bad Request, there is no Parameter");
                return new HttpStatusCodeResult(404);
            }
            Table CarE = CS.Tables.Find(id);
            if (CarE == null)
            {
                return View("Error");
                //return HttpNotFound();
            }
            else
            {
                return View(CarE);
            }
        }


        [HttpPost]
        public ActionResult Edit(Table c)
        {

            CarDatabaseEntities CS_Temp = new CarDatabaseEntities();

            Table oldCarPic = CS_Temp.Tables.Find(c.Id);

            if (c.ImageFile != null)
            {
                string carFileName = c.ImageFile.FileName;
                c.ImageFile.SaveAs(Server.MapPath("~/CarImages/" + carFileName));
                c.CarImgPath = "~/CarImages/" + carFileName;

                if (oldCarPic.CarImgPath != null)
                {
                    System.IO.File.Delete(Server.MapPath(oldCarPic.CarImgPath));
                }
            }
            else
            {
                c.CarImgPath = oldCarPic.CarImgPath;
            }

            CS.Entry(c).State = EntityState.Modified;
            CS.SaveChanges();
            return RedirectToAction("CarStore");
        }


        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                //return Content("Bad Request, there is no Parameter");
                return new HttpStatusCodeResult(404);
            }
            Table CarDe = CS.Tables.Find(id);
            if (CarDe == null)
            {
                return View("Error");
                //return HttpNotFound();
            }
            return View(CarDe);
        }
	}
}