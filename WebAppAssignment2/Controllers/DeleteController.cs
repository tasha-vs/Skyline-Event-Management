using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAssignment2.Models;
using WebAppAssignment2.Controllers;

namespace WebAppAssignment2.Controllers
{
    public class DeleteController : Controller
    {
        // GET: Delete
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DeleteClientByEmail(string email)
        {
            email = email.ToLower().Trim();
            try
            {
                RegistrationEntities db = new RegistrationEntities();
                Client c = db.Clients.Find(email);
                db.Clients.Remove(c);
                db.SaveChanges();

                Messagebox("Client deletion Successfull");
                return View();
            }
            catch(Exception e)
            {
                Messagebox("An error occured, client deletion failed");
                return View("Index");
            }
        }

        public ActionResult DeleteClientByFullName(string fullName)
        {
            fullName = fullName.Trim();
            try
            {
                RegistrationEntities ent = new RegistrationEntities();
                List<Client> clients = ent.Clients.Where(x => x.FullName.ToLower() == fullName.ToLower()).ToList();
                foreach (var i in clients)
                {
                    ent.Clients.Remove(i);
                }
                ent.SaveChanges();

                Messagebox("Client deletion Successfull");
                return View();
            }
            catch (Exception e)
            {
                Messagebox("An error occured, client deletion failed");
                return View("Index");
            }
        }

        public ActionResult EventIndex()
        {
            return View();
        }

        public ActionResult DeleteEvent(string clientEmail, string eventName)
        {
            //trimming the whitespace of our parsed variables
            clientEmail = clientEmail.ToLower().Trim();
            eventName = eventName.Trim();

            //grabbing both databases (clients and registrations)
            RegistrationEntities cdb = new RegistrationEntities();
            RegistrationEntities2 rdb = new RegistrationEntities2();

            //creating lists for both databases that contain the parsed variables
            List<Client> clients = cdb.Clients.Where(x => x.Email.ToLower() == clientEmail.ToLower()).ToList();
            List<Register> reg = rdb.Registers.Where(x => x.EventName.ToLower() == eventName.ToLower()).ToList();

            try
            {
                //if a valid client has been parsed
                if (clients.Capacity > 0)
                {
                    //grabbing all items that contain the client email in registration db
                    var r = rdb.Registers.Where(x => x.Email.ToLower() == clientEmail.ToLower()).ToList();
                    foreach(var ev in reg)
                    {
                        //checking to make sure a valid event has been parsed
                        if (reg.Capacity > 0)
                        {
                            //ensuring correct client registration is deleted
                            if (ev.Email == clientEmail)
                            {
                                rdb.Registers.Remove(ev);
                                rdb.SaveChanges();
                                //alerting user
                                Messagebox("Registration deletion Successfull");
                                return View();
                            }
                        }
                        //deleting the events in the registration db that have the same name as the parsed event name
                    }

                    Messagebox("Please Enter a Valid Event");
                    return View("EventIndex");

                }
                //if no valid client email was parsed
                else
                {
                    //alerting user
                    Messagebox("Please Enter a Valid Client");
                    return View("EventIndex");
                }
            }
            catch
            {
                //alerting user if an error was found
                Messagebox("Event Registration Not Found, Registration Deletion Failed, Please Try Again");
                return View("EventIndex");
            }
        }

        public void Messagebox(string xMessage)
        {
            Response.Write("<script>alert('" + xMessage + "')</script>");
        }
    }
}