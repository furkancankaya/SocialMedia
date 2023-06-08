using SocialMedia.Models;
using SocialMedia.Repositories;
using StockManagementProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Controllers
{   
    public class HomePageController : Controller
    {

        UserRepository userRepository = new UserRepository();
        FollowRepository followRepository = new FollowRepository();
        // GET: HomePage
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["id"]);  //giriş yapan userın idsi
            User user = userRepository.GetById(id);

            List<User> followers = followRepository.GetAllMyFollowers(id);  //userın followerları

            return View();
        }
    }
}