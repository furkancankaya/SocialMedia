using SocialMedia.InterFaces;
using SocialMedia.Models;
using StockManagementProject.DataAccessLayer;
using StockManagementProject.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace SocialMedia.Repositories
{
    public class FollowRepository : IRepository<Follow>
    {
        DataContext db = new DataContext();
        UserRepository userRepository = new UserRepository();

        public bool Add(Follow entity)
        {
            db.Follow.Add(entity);
            db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {

            var data = db.Follow.Find(id);
            db.Follow.Remove(data);
            db.SaveChanges();
            return true;
        }

        public List<Follow> GetAll()
        {
            throw new NotImplementedException();
        }
        public List<User> GetAllMyFollowers(int userId)
        {
            var  followersIds = (from u in db.User
                               join f in db.Follow on u.Id equals f.Id
                               where u.Id == userId
                               select new
                               {
                                   FollowersIds = f.FollowerUserId
                               }).ToList();

            List <User> followers = new List<User>();
            foreach (var f in followersIds)
            {
                int followersId = Convert.ToInt32(f);
                followers.Add(userRepository.GetById(followersId));
            }

            return followers;
        }
        public List<Follow> GetAllMyFollowed(int userId)
        {
            var data = db.Follow.Where(x => x.FollowedUserId == userId).ToList();
            return data;
        }
        public Follow GetById(int id)
        {
            return db.Follow.Find(id);
        }

        public bool Update(Follow entity)
        {
            db.Follow.AddOrUpdate(entity);
            db.SaveChanges();
            return true;
        }
    }
}