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
    public class ContentLikeRepository : IRepository<ContentLike>
    {
        DataContext db = new DataContext();
        public bool Add(ContentLike entity)
        {
            db.ContentLike.Add(entity);
            db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {

            var data = db.ContentLike.Find(id);
            db.ContentLike.Remove(data);
            db.SaveChanges();
            return true;
        }

        public List<ContentLike> GetAll()
        {
            throw new NotImplementedException();
        }
        public List<ContentLike> GetAllById(int id)
        {
            var data = db.ContentLike.Where(x => x.ContentId == id).ToList();
            return data;
        }
        public int GetLikeCountByContentId(int id)
        {
            var data = db.ContentLike.Where(x => x.ContentId == id).Count();
            return data;
        }
        public ContentLike GetById(int id)
        {
            return db.ContentLike.Find(id);
        }

        public bool Update(ContentLike entity)
        {
            db.ContentLike.AddOrUpdate(entity);
            db.SaveChanges();
            return true;
        }

        public bool CheckLike(int userid, int conentid)
        {
            var status = db.ContentLike.Any(x => (x.OwnerLikeId == userid && x.ContentId == conentid));
            return status;
            
        }

        public ContentLike GetByContentAndOwner(int userid, int conentid)
        {
            var like = db.ContentLike.FirstOrDefault(x => (x.OwnerLikeId == userid && x.ContentId == conentid));
            return like;
        }
    }
}