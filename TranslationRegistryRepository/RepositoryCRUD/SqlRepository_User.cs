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
        public async Task DeleteUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            //todo logs
            if (user == null)
                return;

            context.Users.Remove(user);
            context.SaveChanges();
        }

        public async Task<User> GetUser(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<User> GetUser(string login, string password)
        {
            return await context.Users.SingleOrDefaultAsync(
                m => m.Password == password && 
                (m.Name == login || m.Email == login));
        }

        public async  Task<List<User>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task PutUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { }
        }

        public async Task AddUser(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

    }
}
