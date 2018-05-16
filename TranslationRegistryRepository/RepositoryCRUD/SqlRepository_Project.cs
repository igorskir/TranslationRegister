﻿using System;
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
        public async Task AddProject(Project doc)
        {
            context.Projects.Add(doc);
            await context.SaveChangesAsync();
        }

        public async Task<Project> GetProject(int id)
        {
            return await context.Projects.FindAsync(id);
        }

        public async Task<List<Project>> GetProjects()
        {
            return await context.Projects.OrderByDescending(x=>x.Date).ToListAsync();
        }

        public async Task<List<Project>> GetProjectsByStr(string searchToken)
        {
            return await context.Projects.
                OrderBy(x=>x.ProjectStatuseId).
                ThenByDescending(x => x.Date).
                Where(x=> x.Name.Contains(searchToken) || 
                    x.Customer.Contains(searchToken) ||
                    x.Description.Contains(searchToken)).
                ToListAsync();
        }

        public async Task<List<Project>> GetMyProjects(string userName)
        {
            var userId = (await context.Users.FirstOrDefaultAsync(x => x.Name == userName)).Id;
            return await context.Projects
                .Where(x => x.CreatorId == userId)
                .ToListAsync();
        }

        public async Task<List<Project>> GetProjectsInWork()
        {
            return await context.Projects
                .Where(x => x.ProjectStatuseId == Seeding.Seeder.InWorkStatuseId)
                .OrderByDescending(x=>x.Date)
                .ToListAsync();
        }

        public async Task<List<Project>> GetDoneProjects()
        {
            return await context.Projects
                .Where(x => x.ProjectStatuseId == Seeding.Seeder.DoneStatuseId)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task PutProject(Project doc)
        {
            context.Entry(doc).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { }
        }

        public async Task DeleteProject(int id)
        {
            var doc = await context.Projects.FindAsync(id);
            if (doc == null)
                return;

            var a = context.Deadlines.Where(x => x.ProjectId == id);
            context.Deadlines.RemoveRange(a);
            context.Projects.Remove(doc);
            await context.SaveChangesAsync();
        }
    }
}
