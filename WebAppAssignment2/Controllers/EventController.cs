using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAssignment2.Models;

namespace WebAppAssignment2.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            RegistrationEntities1 database = new RegistrationEntities1();
            var evnt = from e in database.Events
                       orderby e.EventName
                       select e;
            return View(evnt);
        }

        public ActionResult RegForm()
        {
            return View();
        }

        public ActionResult Register([Bind(Include = "guestnumber,paymentamount,eventname,email")]Register r)
        {
            using (RegistrationEntities2 entities = new RegistrationEntities2())
            {
                try
                {
                    if (r.EventName == "")
                    {
                        Messagebox("Please enter an event name");
                        return View("RegForm");
                    }
                    else if (r.Email == "")
                    {
                        Messagebox("Please enter a registered clients email");
                        return View("RegForm");
                    }
                    entities.Registers.Add(r);
                    entities.SaveChanges();
                    Messagebox("Registration for event successfull");
                    return View(r);
                }
                catch (Exception e)
                {
                    Messagebox("An error occurred, Please ensure all required fields contain valid data and try again");
                    return View("RegForm");
                }
            }
        }

        public ActionResult Registrations()
        {
            RegistrationEntities2 db = new RegistrationEntities2();
            var reg = from e in db.Registers
                       orderby e.RegisterID
                       select e;
            return View(reg);
        }

        public void Messagebox(string xMessage)
        {
            Response.Write("<script>alert('" + xMessage + "')</script>");
        }
    }
}