using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationRegistryModel;

namespace SqlRepository
{
    public partial class SqlRep : IRepository
    {
        public void DeleteUser(int id)
        {
            var user = context.Users.SingleOrDefault(m => m.Id == id);
            //todo log?
            if (user == null)
                return;

            context.Users.Remove(user);
            context.SaveChanges();
        }

        public User GetUser(int id)
        {
            return context.Users.SingleOrDefault(m => m.Id == id);
        }

        public List<User> GetUsers()
        {
            return context.Users.ToList();
        }

        public void PutUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) { }
        }

        public void AddUser(User user)
        {
            context.Users.Add(user);
            context.SaveChangesAsync();
        }

    }
}
