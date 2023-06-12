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
    public class ContentCommentRepository:IRepository<ContentComment>
    {
        DataContext db = new DataContext();

        public bool Add(ContentComment entity)
        {
            db.ContentComment.Add(entity);
            db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {

            var data = db.ContentComment.Find(id);
            db.ContentComment.Remove(data);
            db.SaveChanges();
            return true;
        }

        public List<ContentComment> GetAll()
        {
            throw new NotImplementedException();
        }
        public List<ContentComment> GetAllById(int id)
        {
            var data = db.ContentComment.Where(x => x.ContentId == id).ToList();
            return data;
        }
        public ContentComment GetById(int id)
        {
            return db.ContentComment.Find(id);
        }

        public bool Update(ContentComment entity)
        {
            db.ContentComment.AddOrUpdate(entity);
            db.SaveChanges();
            return true;
        }
    }
}