using Registr.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Registr
{
    internal class Helper
    {
        private static SportEntities s_firstDBEntities;
        public static SportEntities GetContext()
        {
            if (s_firstDBEntities == null)
            {
                s_firstDBEntities = new SportEntities();
            }
            return s_firstDBEntities;
        }
        public void CreateUser(Model.Polizovateli users)
        {
            GetContext().Polizovateli.Add(users);
            GetContext().SaveChanges();
        }
        public void UpdateUser(Model.Polizovateli users)
        {
            s_firstDBEntities.Entry(users).State = EntityState.Modified;
            s_firstDBEntities.SaveChanges();
        }
        public void RemoveUser(int idUsers)
        {
            var users = s_firstDBEntities.Polizovateli.Find(idUsers);
            s_firstDBEntities.Polizovateli.Remove(users);
            s_firstDBEntities.SaveChanges();
        }
        public void FiltrUser()
        {
            var users = s_firstDBEntities.Polizovateli.Where(x => x.LoginPolizovateli.StartsWith("М") || x.LoginPolizovateli.StartsWith("А"));
        }
        public void SortUser()
        {
            var users = s_firstDBEntities.Polizovateli.OrderBy(x => x.LoginPolizovateli);
        }
    }

}
