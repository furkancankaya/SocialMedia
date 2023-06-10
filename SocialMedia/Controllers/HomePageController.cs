using Microsoft.Ajax.Utilities;
using SocialMedia.Models;
using SocialMedia.Repositories;
using StockManagementProject.Repositories;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Controllers
{
    public class HomePageController : Controller
    {

        UserRepository userRepository = new UserRepository();
        FollowRepository followRepository = new FollowRepository();
        ContentRespository contentRespository = new ContentRespository();
        // GET: HomePage
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["id"]);  //giriş yapan userın idsi

            User user = userRepository.GetById(id);

            // List<User> followers = followRepository.GetAllMyFollowers(id);  //userın followerları

            List<Content> contents = contentRespository.GetAllFollowedContents(id);  //takip ettiklerimin contentleri

            dynamic mymodel = new ExpandoObject();
            mymodel.User = user;
            mymodel.Contents = contents;
            return View(mymodel);


        }
        //--------------------------------------------------------------------------------
        
        //--------------------------------Profil Sayfası----------------------------------
        public ActionResult Profile()
        {
            int id = Convert.ToInt32(Session["id"]);  //giriş yapan userın idsi

            User user = userRepository.GetById(id);
            List<Content> contents = contentRespository.GetAllMyContents(id);  //takip ettiklerimin contentleri
            List<User> followers = followRepository.GetAllMyFollowers(id);  //userın followerları
            int countFollowers = followers.Count();

     
            dynamic mymodel = new ExpandoObject();
            mymodel.User = user;
            mymodel.Contents = contents;
            mymodel.CountFollowers = countFollowers;
            return View(mymodel);
        }

        //--------------------------------------------------------------------------------
        //----------------------Profil DÜzenleme Sayfası----------------------------------


        [HttpGet]
        public ActionResult ProfileEdit()
        {
            int id = Convert.ToInt32(Session["id"]);  //giriş yapan userın idsi

            User userr = userRepository.GetById(id);

            return View(userr);
        }

        [HttpPost]
        public ActionResult ProfileEdit(User user)
        {
            if (user != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                user.Id = id;
                bool status = userRepository.Update(user);
                if (status == true)
                {
                    ViewBag.Message = "Güncelleme başarılı.";
                    
                }
                else
                {
                    ViewBag.Message = "Güncelleme başarısız!";
                }
            }
            return View(user);
        }
        //--------------------------------------------------------------------------------

        //----------------------------Arkadaşlar Sayfası----------------------------------
        public ActionResult Friends() 
        {
            int id = Convert.ToInt32(Session["id"]);  //giriş yapan userın idsi
            List<User> followers = followRepository.GetAllMyFollowers(id);  //userın followerları
            int countFollowers = followers.Count();
            List<User> followeds = followRepository.GetAllMyFollowed(id);  //userın followerları
            int countFolloweds = followeds.Count();

            dynamic mymodel = new ExpandoObject();
            mymodel.Followers = followers;
            mymodel.CountFollowers = countFollowers;
            mymodel.Followeds = followeds;
            mymodel.CountFolloweds = countFolloweds;
            return View(mymodel);

        }


    }
}