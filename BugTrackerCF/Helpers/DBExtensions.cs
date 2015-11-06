using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using BugTrackerCF.Models;

namespace BugTrackerCF.Helpers
{
    public static class DbExtensions
    {
        public static bool Update<T>(this ApplicationDbContext db, T item, string[] changedProperties ) where T:class,new()
        {
            db.Set<T>().Attach(item);
            foreach (var propertyName in changedProperties)
            {
                db.Entry(item).Property(propertyName).IsModified = true;
            }
            db.SaveChanges();
            return true;
        }
    }
}