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
        public List<ContentComment> GetAllByContentId(int contentId)
        {
            var data = db.ContentComment
                .Include("Owner")
                .Where(x => x.ContentId == contentId).ToList();
            foreach( ContentComment comment in data)
            {
                comment.CreateDateHumanReadable = this.FormatTimeAgo(comment.CreateDate);
            }
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

        // Function to format time in a relative format
        public string FormatTimeAgo(DateTime timestamp)
        {
            TimeSpan timeDifference = DateTime.Now - timestamp;

            if (timeDifference.TotalSeconds < 60)
            {
                return "Just now";
            }
            else if (timeDifference.TotalMinutes < 60)
            {
                int minutes = (int)timeDifference.TotalMinutes;
                return $"{minutes} {(minutes == 1 ? "minute" : "minutes")} ago";
            }
            else if (timeDifference.TotalHours < 24)
            {
                int hours = (int)timeDifference.TotalHours;
                return $"{hours} {(hours == 1 ? "hour" : "hours")} ago";
            }
            else if (timeDifference.TotalDays < 30)
            {
                int days = (int)timeDifference.TotalDays;
                return $"{days} {(days == 1 ? "day" : "days")} ago";
            }
            else if (timeDifference.TotalDays < 365)
            {
                int months = (int)(timeDifference.TotalDays / 30);
                return $"{months} {(months == 1 ? "month" : "months")} ago";
            }
            else
            {
                int years = (int)(timeDifference.TotalDays / 365);
                return $"{years} {(years == 1 ? "year" : "years")} ago";
            }
        }
    }
}