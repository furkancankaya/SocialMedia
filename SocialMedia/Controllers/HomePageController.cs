using Microsoft.Ajax.Utilities;
using SocialMedia.Models;
using SocialMedia.Repositories;
using StockManagementProject.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
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
                List<ContentLike> countLikes = contentLikeRepository.GetAllById(content.Id);

                List<ContentComment> comments = contentCommentRepository.GetAllById(content.Id);


                ContentViewModel contentViewModel = new ContentViewModel
                {
                    Content = content,
                    Likes = countLikes,
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
            List<Content> contents = contentRespository.GetAllMyContents(id);  //contentlerim
            List<User> followers = followRepository.GetAllMyFollowers(id);  //userın followerları
            int countFollowers = followers.Count();
            List<ContentViewModel> contentViewModels = new List<ContentViewModel>();
            foreach (Content content in contents)
            {
                List<ContentLike> countLikes = contentLikeRepository.GetAllById(content.Id);

                List<ContentComment> comments = contentCommentRepository.GetAllById(content.Id);


                ContentViewModel contentViewModel = new ContentViewModel
                {
                    Content = content,
                    Likes = countLikes,
                    ContentComment = comments
                };

                contentViewModels.Add(contentViewModel);
            }




            dynamic mymodel = new ExpandoObject();
            mymodel.User = user;
            mymodel.ContentViewModel = contentViewModels;
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
        public ActionResult AddContent(Content content, HttpPostedFileBase ObjectPath)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Users");
            }

            int id = Convert.ToInt32(Session["id"]);  //giriş yapan userın idsi

            


            if (content != null)
            {
                if (ObjectPath != null && ObjectPath.ContentLength > 0 && (ObjectPath.ContentType == "image/png" || ObjectPath.ContentType == "image/jpeg" || ObjectPath.ContentType == "image/jpg" || ObjectPath.ContentType == "video/mp4"))
                {
                    string objectPath = "";
                    string objectName = "";

                    objectName = Guid.NewGuid().ToString() + Path.GetFileName(ObjectPath.FileName);
                    objectPath = Path.Combine(Server.MapPath("~/Content/Images/Contents"), objectName);
                    ObjectPath.SaveAs(objectPath);
                    content.ObjectPath = objectName;
                    content.Type = "File";
                }
                else
                {
                    content.Type = "Text";
                }
                

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
        //------------------------------İçerik Silme------------------------------------
        [HttpPost]
        public JsonResult DeleteContent(Content content)
        {



            bool status = contentRespository.Delete(content.Id);


            if (status)
            {
                return Json("Paylaşım Silindi");
            }
            else
            {
                return Json("Paylaşım Silinemedi!");
            }


        }


        //--------------------------------------------------------------------------------
        //------------------------------İçerik Güncelleme------------------------------------
        [HttpGet]
        public ActionResult UpdateContent(int id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
           

            int contentId = Convert.ToInt32(id);

            Content content = contentRespository.GetAllById(contentId);


            return View(content);

        }
        [HttpPost]
        public ActionResult UpdateContent(Content content, HttpPostedFileBase ObjectPath)
        {
            if (content != null)
            {

                if (Session["id"] == null)
                {
                    return RedirectToAction("Login", "Users");
                }
                int id = Convert.ToInt32(Session["id"]);

                




                if (content != null)
                {
                    if (ObjectPath != null && ObjectPath.ContentLength > 0 && (ObjectPath.ContentType == "image/png" || ObjectPath.ContentType == "image/jpeg" || ObjectPath.ContentType == "image/jpg" || ObjectPath.ContentType == "video/mp4"))
                    {
                        string objectPath = "";
                        string objectName = "";

                        objectName = Guid.NewGuid().ToString() + Path.GetFileName(ObjectPath.FileName);
                        objectPath = Path.Combine(Server.MapPath("~/Content/Images/Contents"), objectName);
                        ObjectPath.SaveAs(objectPath);
                        content.ObjectPath = objectName;
                        content.Type = "File";
                    }
                    else
                    {
                        content.Type = "Text";
                    }


                    content.UserId = id;
                    bool status = contentRespository.Update(content);
                    if (status == true)
                    {
                        ViewBag.Message = "Güncelleme başarılı.";
                        return RedirectToAction("Profile", "HomePage");
                    }
                    else
                    {
                        ViewBag.Message = "Güncelleme başarısız!";
                    }
                }

               
            }
            return View();
        }


        //--------------------------------------------------------------------------------

        //--------------------------------Arkadaş Bulma----------------------------------
        [HttpGet]
        public ActionResult FindFriends()
        { 

            return View();
        }
        [HttpPost]
        public ActionResult FindFriends(FinndFriends friends)
        {
            if (friends.FindOption == "Nick")
            {
                
                List<User> users = userRepository.GetAllByNick(friends.FindText);  //userın followerları
                Session["findedUsers"] = users;
                return RedirectToAction("FindedFriends", "HomePage", new { data = users });

            }
            else
            {
                List<User> users = userRepository.GetAllByMail(friends.FindText);
                Session["findedUsers"] = users;
                return RedirectToAction("FindedFriends", "HomePage", new { data = users });
            }

            
        }

        
        public ActionResult FindedFriends()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            int id = Convert.ToInt32(Session["id"]);
            List<User> takipEttiklerim = followRepository.GetAllMyFollowed(id);


            dynamic mymodel = new ExpandoObject();
            mymodel.Followed = takipEttiklerim;
            mymodel.Friends = Session["findedUsers"];
            
            return View(mymodel); 
        }

        [HttpPost]
        public JsonResult AddFriend(Follow follow)
        {

            int id = Convert.ToInt32(Session["id"]);  //giriş yapan userın idsi

            Follow userOfFollow = followRepository.GetByFollwerAndFollowedId(id, follow.FollowedUserId );
           

            if (userOfFollow == null)
            {
                Follow newFollow = new Follow
                {
                    FollowedUserId = follow.FollowedUserId,
                    FollowerUserId = id
                };
                followRepository.Add(newFollow);
            }
            else
            {
                followRepository.Delete(userOfFollow.Id);
            }


            return Json(0);
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

        //------------------------------Yorum yapmak---------------------------------

        [HttpPost]
        public JsonResult DeleteComment(ContentComment deleteComment)
        {
    
            contentCommentRepository.Delete(deleteComment.Id);   

            return Json("");
        }


    }
}