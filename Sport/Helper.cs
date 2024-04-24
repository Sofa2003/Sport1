using Sport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Sport
{
    public class Helper
    {
        private static SportEntities s_firstDBEntities;
        public static SportEntities GetContext()
        {   
            if(s_firstDBEntities== null)
            {
                s_firstDBEntities= new SportEntities();
            }
            return s_firstDBEntities;
        }
        public void CreateUsers(Models.Polizovateli user)
        {
            s_firstDBEntities.Polizovateli.Add(user);
            s_firstDBEntities.SaveChanges();
        }
        public static void UpdateUsers(Models.Polizovateli user)
        {
            s_firstDBEntities.Entry(user).State= EntityState.Modified;
            s_firstDBEntities.SaveChanges();
        }
        public void RemoveUsers(int KodPolizovatieli)
        {
            var user = s_firstDBEntities.Polizovateli.Find(KodPolizovatieli);
            s_firstDBEntities.Polizovateli.Remove(user);
            s_firstDBEntities.SaveChanges();

        }
        public void FilterUsers() 
        {
            var user = s_firstDBEntities.Polizovateli.Where(x => x.LoginPolizovateli.StartsWith("A") || x.LoginPolizovateli.StartsWith("V"));
        }
        public void SourUsers()
        {
            var user = s_firstDBEntities.Polizovateli.OrderBy(x => x.LoginPolizovateli);
        }
    }
}
