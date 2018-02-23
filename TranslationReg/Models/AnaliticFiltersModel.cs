using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Models
{
    public class AnaliticsFiltersModel
    {

        public DateTime periodFrom;
        public DateTime periodTo;

        public SelectList ProjectList;
        public SelectList UserList;
        public SelectList WorkTypeList;

        public static async Task<AnaliticsFiltersModel> GetModel(IRepository rep)
        {
            int selectedItemId = 0;
            var projects = await rep.GetProjects();
            projects.Insert(0, new Project { Id = selectedItemId, Name = "Все проекты" });

            var users = await rep.GetUsers();
            users.Insert(0, new User { Id = selectedItemId, Name = "Все работники" });

            var workTypes = await rep.GetWorkTypes();
            workTypes.Insert(0, new WorkType { Id = selectedItemId, Name = "Все типы работ" });

            AnaliticsFiltersModel model = new AnaliticsFiltersModel
            {
                periodFrom = DateTime.Now - TimeSpan.FromDays(30),
                periodTo = DateTime.Now,
                ProjectList = new SelectList(projects, "Id", "Name", selectedItemId),
                UserList = new SelectList(users, "Id", "Name", selectedItemId),
                WorkTypeList = new SelectList(workTypes, "Id", "Name", selectedItemId)
            };

            return model;
        }
    }
}