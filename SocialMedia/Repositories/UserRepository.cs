using Microsoft.Ajax.Utilities;
using SocialMedia.InterFaces;
using SocialMedia.Models;
using StockManagementProject.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementProject.Repositories
{
    internal class UserRepository : IRepository<User>
    {
        DataContext db = new DataContext();

        public bool Add(User entity)
        {
            var exists = db.User.Any(x => x.Mail == entity.Mail);
            if (exists == false)
            { 
                entity.RoleId = 2;
                entity.IsStatus = true;
                db.User.Add(entity);
                db.SaveChanges();
                return true;
            }
            return false;

        }

        public bool Delete(int id)
        {
            User user = db.User.FirstOrDefault(x => x.Id == id && x.IsStatus == true);
            if (user != null)
            {
                try
                {
                    user.IsStatus = false;

                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;

                }
            }
            return false;
        }

        public List<User> GetAll()
        {
            return db.User.Where(x => x.IsStatus == true).ToList();
        }

        public User GetById(int id)
        {
            User user = db.User.FirstOrDefault(x => x.Id == id && x.IsStatus == true);
            return user;
        }

        public User GetByLogin(string mail, string password)
        {
            User user = db.User.Where(x => x.Mail == mail && x.Password == password && x.IsStatus == true).FirstOrDefault();

            return user;
        }
        public bool Update(User entity)
        {
            var user = db.User.FirstOrDefault(x => x.Id == entity.Id && x.IsStatus == true);
            if (user != null && entity.RoleId > 0)
            {
                user.Name = !String.IsNullOrWhiteSpace(entity.Name)?entity.Name:user.Name;
                user.Surname = !String.IsNullOrWhiteSpace(entity.Surname) ? entity.Surname : user.Surname;
                user.Mail = !String.IsNullOrWhiteSpace(entity.Mail) ? entity.Mail : user.Mail;
                user.Password = !String.IsNullOrWhiteSpace(entity.Password) ? entity.Password : user.Password;
                user.Biography = !String.IsNullOrWhiteSpace(entity.Biography) ? entity.Biography : user.Biography;
                user.UserName = !String.IsNullOrWhiteSpace(entity.UserName) ? entity.UserName : user.UserName;
                user.Photo = !String.IsNullOrWhiteSpace(entity.Photo) ? entity.Photo : user.Photo;
                
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}