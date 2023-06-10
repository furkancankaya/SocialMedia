using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SocialMedia.Models;
using StockManagementProject.DataAccessLayer;
using StockManagementProject.Repositories;

namespace SocialMedia.Controllers
{
    public class UsersController : Controller
    {
        
        UserRepository repositoryUser = new UserRepository();


        //----------------------------------------Login----------------------------
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            User loginUser= repositoryUser.GetByLogin(user.Mail, user.Password);
            if (loginUser == null)
            {
                ViewBag.Message = "Giriş başarısız!";
                return View();
            }
            else
            {
                ViewBag.Message = "Giriş başarılı.";
                Session["id"] = loginUser.Id;
                return RedirectToAction("Index","HomePage");
            }

           
        }

        //--------------------------------------------------------------------------------

        //-----------------------------------------Sign Up----------------------------
        [HttpGet]
        public ActionResult SignUp()
        {

            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            if (user != null)
            {
                bool status = repositoryUser.Add(user);
                if (status == true)
                {
                    ViewBag.Message = "Başarıyla kayıt oldunuz.";
                    return RedirectToAction("Login", "Users", new { data = user });
                }
                else
                {
                    ViewBag.Message = "Kayıt işlemi başarısız!";
                }
            }
            return View();
        }
        //--------------------------------------------------------------------------------

        //--------------------------------------Home--------------------------------------
        
        public ActionResult Home(User user)
        {
            if (user != null)
            {
                bool status = repositoryUser.Add(user);
                if (status == true)
                {
                    ViewBag.Message = "Başarıyla kayıt oldunuz.";
                }
                else
                {
                    ViewBag.Message = "Kayıt işlemi başarısız!";
                }
            }
            return View();
        }
        //--------------------------------------------------------------------------------

        //// GET: Users
        //public ActionResult Index()
        //{
        //    var user = db.User.Include(u => u.Role);
        //    return View(user.ToList());
        //}

        //// GET: Users/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = db.User.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}
        //// GET : Login/

        //public ActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login([Bind(Include = "Mail,Password")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //       var data = repository.GetByLogin(user.Mail, user.Password);
        //        // data ya bakıcalak
        //        if (data == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }
        //    return View(user);
        //}
        //// GET: Users/Create
        //public ActionResult Create()
        //{
        //    ViewBag.RoleId = new SelectList(db.Role, "Id", "Name");
        //    return View();
        //}

        //// POST: Users/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,Surname,UserName,Mail,Password,Biography,Photo,IsStatus,RoleId")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        repository.Add(user);
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.RoleId = new SelectList(db.Role, "Id", "Name", user.RoleId);
        //    return View(user);
        //}

        //// GET: Users/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = db.User.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.RoleId = new SelectList(db.Role, "Id", "Name", user.RoleId);
        //    return View(user);
        //}

        //// POST: Users/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Surname,UserName,Mail,Password,Biography,Photo,IsStatus,RoleId")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(user).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.RoleId = new SelectList(db.Role, "Id", "Name", user.RoleId);
        //    return View(user);
        //}

        //// GET: Users/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = db.User.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        //// POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    User user = db.User.Find(id);
        //    db.User.Remove(user);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

    }
}
