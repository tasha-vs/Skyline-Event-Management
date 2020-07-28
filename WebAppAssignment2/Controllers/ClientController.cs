using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAssignment2.Models;

namespace WebAppAssignment2.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            RegistrationEntities database = new RegistrationEntities();
            var client = from c in database.Clients
                         orderby c.Email
                         select c;
            return View(client);
        }

        public ActionResult RegForm()
        {
            return View();
        }

        public ActionResult Register([Bind(Include = "email,fullName,address,age,phone")]Client client)
        {
            using (RegistrationEntities entities = new RegistrationEntities())
            {
                try
                {
                    if (client.Email == "" || client.FullName == "" || client.Address == "")
                    {
                        Messagebox("Please ensure all required fields contain data");
                        return View("RegForm");
                    }

                    entities.Clients.Add(client);
                    entities.SaveChanges();
                    Messagebox("Client Successfully Registered");
                    return View(client);
                }
                catch(Exception e)
                {
                    Messagebox("An error occurred, Please ensure all required fields contain data");
                    return View("RegForm");
                }
            } 
        }

        public ActionResult Edit([Bind(Include = "email,fullName,address,age,phone")]Client client)
        {
            return View();
        }

        public void Messagebox(string xMessage)
        {
            Response.Write("<script>alert('" + xMessage + "')</script>");
        }
    }
}