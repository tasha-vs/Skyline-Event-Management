using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAssignment2.Models;

namespace WebAppAssignment2.Controllers
{
    public class UpdateController : Controller
    {
        // GET: Update
        public ActionResult ClientIndex()
        {
            return View();
        }

        public ActionResult UpdateClient(string email, string address, string phone)
        {
            try
            {
                RegistrationEntities db = new RegistrationEntities();
                Client c = db.Clients.Find(email);

                if (email == "")
                {
                    Messagebox("Please enter the email of the client you wish to update");
                    return View("ClientIndex");
                }

                if (c is null)
                {
                    Messagebox("Please enter a valid client email");
                    return View("ClientIndex");
                }

                if (address != "")
                {
                    c.Address = address;
                    db.SaveChanges();
                }

                if (phone != "")
                {
                    c.Phone = phone;
                    db.SaveChanges();
                }

                if (address == "" && phone == "")
                {
                    Messagebox("Please enter data to update");
                    return View("ClientIndex");
                }

                return View(c);
            }
            catch(Exception e)
            {
                Messagebox("An error occured, Please ensure valid data is provided and try again...");
                return View("ClientIndex");
            }

        }

        public void Messagebox(string xMessage)
        {
            Response.Write("<script>alert('" + xMessage + "')</script>");
        }
    }
}