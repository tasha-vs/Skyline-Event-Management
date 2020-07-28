using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAssignment2.Models;
using System.Diagnostics;

namespace WebAppAssignment2.Controllers
{
    public class SearchController : Controller
    {
        // GET: ClientSearch
        public ActionResult ClientIndex()
        {
            return View();
        }

        //come back to!!!!!!!!!!!!!!
        public ActionResult ClientEmailSearch(string email)
        {
            email = email.Trim();

            RegistrationEntities database = new RegistrationEntities();
            Client s = database.Clients.Find(email);

            if (email == "")
            {
                Messagebox("Please enter an email to search for");
                return View("ClientIndex");
            }

            try
            {
                if (!(s is null))
                {
                    if (s.Email == email)
                    {
                        return View(s);
                    }
                    else
                    {
                        Messagebox("Client not found");
                        return View("ClientIndex");
                    }
                }
                else
                {
                    Messagebox("Client not found");
                    return View("ClientIndex");
                }
                
            }
            catch(Exception e)
            {
                Messagebox("An error occured... Please try again");
                return View("ClientIndex");
            }      
        }

        public ActionResult ClientFullNameSearch(string fullName)
        {
            if (fullName == "")
            {
                Messagebox("Please enter a name to search for");
                return View("ClientIndex");
            }
            fullName = fullName.Trim();

            RegistrationEntities database = new RegistrationEntities();

            try
            {
                var s = from x in database.Clients
                        where x.FullName == fullName
                        select x;

                //checking to see if s is empty 
                if (!s.Any())
                {
                    Messagebox("Client not found");
                    return View("ClientIndex");
                }

                else { return View(s); }
            }
            catch(Exception e)
            {
                Messagebox("An error occured... Please try again");
                return View("ClientIndex");
            }

        }

        public ActionResult EventIndex()
        {
            return View();
        }

        public ActionResult EventNameSearch(string eventName)
        {

            eventName = eventName.Trim();
            RegistrationEntities1 db = new RegistrationEntities1();

            try
            {
                List<Event> e = db.Events.Where(x => x.EventName == eventName).ToList();

                if (!e.Any())
                {
                    Messagebox("Event not found");
                    return View("EventIndex");
                }
                else { return View(e); }
            }
            catch(Exception e)
            {
                Messagebox("An error occurred... Please try again");
                return View("EventIndex");
            }

        }

        public ActionResult EventDateSearch(string date)
        {
            date = date.Trim();
            RegistrationEntities1 db = new RegistrationEntities1();
            List<Event> e = db.Events.Where(x => x.Date == date).ToList();

            try
            {
                if (!e.Any())
                {
                    Messagebox("Event not found");
                    return View("EventIndex");
                }
                else { return View(e); }
            }
            catch(Exception exception)
            {
                Messagebox("An error occurred... Please try again");
                return View("EventIndex");
            }

        }

        public void Messagebox(string xMessage)
        {
            Response.Write("<script>alert('" + xMessage + "')</script>");
        }
    }
}