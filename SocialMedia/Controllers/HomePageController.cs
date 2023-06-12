using Microsoft.Ajax.Utilities;
using SocialMedia.Models;
using SocialMedia.Repositories;
using StockManagementProject.Repositories;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

using System.Xml.Linq;

namespace SocialMedia.Controllers
{
    public class HomePageController : Controller
    {

        UserRepository userRepository = new UserRepository();
        FollowRepository followRepository = new FollowRepository();
        ContentRespository contentRespository = new ContentRespository();
        ContentLikeRepository contentLikeRepository = new ContentLikeRepository();
        ContentCommentRepository contentCommentRepository = new ContentCommentRepository();

        // GET: HomePage
        public ActionResult Index()
        {

            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            int id = Convert.ToInt32(Session["id"]);  //giriş yapan userın idsi

           

            User user = userRepository.GetById(id);

            // List<User> followers = followRepository.GetAllMyFollowers(id);  //userın followerları
            

            List<Content> contents = contentRespository.GetAllFollowedContents(id);  //takip ettiklerimin contentleri

            contents.ForEach((content) =>
            {
                int countLikes = contentLikeRepository.GetAllById(content.Id).Count;
            });
            List<ContentViewModel> contentViewModels = new List<ContentViewModel>();
            foreach (Content content in contents)
            {
                int countLikes = contentLikeRepository.GetAllById(content.Id).Count;

                List<ContentComment> comments = contentCommentRepository.GetAllById(content.Id);


                ContentViewModel contentViewModel = new ContentViewModel
                {
                    Content = content,
                    LikeCount = countLikes,
                    ContentComment = comments
                };

                contentViewModels.Add(contentViewModel);
            }


            
            dynamic mymodel = new ExpandoObject();
            mymodel.User = user;
            mymodel.ContentViewModel = contentViewModels;
            
            return View(mymodel);


        }
       

        //--------------------------------------------------------------------------------

        //--------------------------------Profil Sayfası----------------------------------
        public ActionResult Profile()
        {

            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
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

            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            int id = Convert.ToInt32(Session["id"]);  //giriş yapan userın idsi

           

            User userr = userRepository.GetById(id);

            return View(userr);
        }

        [HttpPost]
        public ActionResult ProfileEdit(User user)
        {
            if (user != null)
            {

                if (Session["id"] == null)
                {
                    return RedirectToAction("Login", "Users");
                }
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

            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            int id = Convert.ToInt32(Session["id"]);  //giriş yapan userın idsi


            List<User> followers = followRepository.GetAllMyFollowers(id);  //userın followerları
            int countFollowers = followers.Count();
            List<User> followeds = followRepository.GetAllMyFollowed(id);  //userın followedları
            int countFolloweds = followeds.Count();

            dynamic mymodel = new ExpandoObject();
            mymodel.Followers = followers;
            mymodel.CountFollowers = countFollowers;
            mymodel.Followeds = followeds;
            mymodel.CountFolloweds = countFolloweds;
            return View(mymodel);

        }

        //--------------------------------------------------------------------------------

        //--------------------------------İçerik ekleme----------------------------------

        [HttpGet]
        public ActionResult AddContent()
        {


            return View();
        }

        [HttpPost]
        public ActionResult AddContent(Content content)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Users");
            }

            int id = Convert.ToInt32(Session["id"]);  //giriş yapan userın idsi

            


            if (content != null)
            {
                content.UserId = id;
                bool status = contentRespository.Add(content);
                if (status == true)
                {
                    ViewBag.Message = "İçerik paylaşıldı.";
                    return RedirectToAction("Profile", "HomePage", new { data = content });
                }
                else
                {
                    ViewBag.Message = "Paylaşım başarısız!";
                }
            }

            return View();
        }
        //--------------------------------------------------------------------------------



        //--------------------------------İçerik ekleme----------------------------------
        [HttpGet]
        public ActionResult FindFriends()
        { 

            return View();
        }
        [HttpPost]
        public ActionResult FindFriends(String friend)
        {


            return View();
        }

        //--------------------------------------------------------------------------------

        
        //------------------------------Beğeni ALma---------------------------------

        [HttpPost]
        public JsonResult LikeContent(ContentLike postLike)
        {

            int id = Convert.ToInt32(Session["id"]);  //giriş yapan userın idsi

            var like = contentLikeRepository.GetByContentAndOwner(id, postLike.ContentId);

            if (like == null)
            {
                ContentLike contentLike = new ContentLike
                {
                    OwnerLikeId = id,
                    ContentId = postLike.ContentId

                };
                contentLikeRepository.Add(contentLike);
            }
            else
            {
                contentLikeRepository.Delete(like.Id);
            }


            return Json(contentLikeRepository.GetLikeCountByContentId(postLike.ContentId));
        }

        //------------------------------Yorum yapmak---------------------------------

        [HttpPost]
        public JsonResult AddComment(ContentComment addComment)
        {

            int id = Convert.ToInt32(Session["id"]);  //giriş yapan userın idsi

           
            


            
            
            ContentComment comment = new ContentComment
            {
                OwnerId = id,
                ContentId = addComment.ContentId,
                Comment = addComment.Comment,

            };
            contentCommentRepository.Add(comment); ;
            
            


            return Json(contentCommentRepository.GetAllById(addComment.ContentId).Count);
        }
    }
}