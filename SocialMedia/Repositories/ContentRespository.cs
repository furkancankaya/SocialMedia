using SocialMedia.InterFaces;
using SocialMedia.Models;
using StockManagementProject.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace SocialMedia.Repositories
{
    public class ContentRespository : IRepository<Content>
    {
        DataContext db = new DataContext();
        FollowRepository followRepository = new FollowRepository();

        public bool Add(Content entity)
        {
            db.Content.Add(entity);
            db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {

            var data = db.Content.Find(id);
            db.Content.Remove(data);
            db.SaveChanges();
            return true;
        }

        public List<Content> GetAll()
        {
            throw new NotImplementedException();
        }
        public List<Content> GetAllByUserId(int userId)
        {
            var data = db.Content.Where(x => x.UserId == userId).ToList();
            return data;
        }
        public Content GetAllById(int id)
        {
            var data = db.Content.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }
        public List<Content> GetAllMyContents(int userId)
        {
           
            var data = db.Content.Where(x => x.UserId==userId).ToList();
            return data;
        }
        public List<Content> GetAllFollowedContents(int userId)
        {
            var myFollowedUserIds = followRepository.GetAllMyFollowed(userId).Select(x => x.Id).ToList();
            var data = db.Content.Where(x => myFollowedUserIds.Contains(x.UserId)).ToList();
            return data;
        }
        public Content GetById(int id)
        {
            return  db.Content.Find(id);
        }

        public bool Update(Content entity)
        {
            db.Content.AddOrUpdate(entity);
            db.SaveChanges();
            return true;
        }
    }
}