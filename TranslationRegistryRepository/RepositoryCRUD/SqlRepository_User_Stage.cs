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
        public async Task<User_Stage> GetUser_Stage(int id)
        {
            return await context.User_Stage.FindAsync(id);
        }

        public async Task<List<User_Stage>> GetUser_Stages()
        {
            return await context.User_Stage.ToListAsync();
        }

        public async Task<List<User_Stage>> GetMyCurrentTasks(string login)
        {
            var userId = (await context.Users.FirstOrDefaultAsync(x => x.Name == login)).Id;
            return await context.User_Stage.
                Where(x => x.UserId == userId).
                Where(x => x.DocFileId == null).
                ToListAsync();
        }

        public async Task<List<User_Stage>> GetMyDoneTasks(string login)
        {
            var userId = (await context.Users.FirstOrDefaultAsync(x => x.Name == login)).Id;
            return await context.User_Stage.
                Where(x => x.UserId == userId).
                Where(x => x.DocFileId != null).
                ToListAsync();
        }

        public async Task PutUser_Stage(User_Stage User_Stage)
        {
            context.Entry(User_Stage).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { }
        }

        public async Task AddUser_Stage(User_Stage User_Stage)
        {
            context.User_Stage.Add(User_Stage);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUser_Stage(int id)
        {
            var User_Stage = await GetUser_Stage(id);
            if (User_Stage == null)
                return;

            context.User_Stage.Remove(User_Stage);
            await context.SaveChangesAsync();
        }
    }
}
