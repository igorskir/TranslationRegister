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
    public class ChosenFilters
    {
        [DataType(DataType.Date)]
        public DateTime PeriodFrom { get; set; }
        [DataType(DataType.Date)]
        public DateTime PeriodTo { get; set; }

        public bool ForAllTime { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public int WorkTypeId { get; set; }
    } 
    public class ReportModel
    {
        // выбранный элемент списка, который отменяет фильтрацию по полю 
        // и отображается первым, будет иметь этот id 
        static int skipFilterId = -1;


        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }

        public SelectList ProjectList { get; set; }
        public SelectList UserList { get; set; }
        public SelectList WorkTypeList { get; set; }

        public List<User_Stage> FilteredWorks { get; set; }

        public static async Task<ReportModel> GetModel(IRepository Rep, ChosenFilters filters = null)
        {
            // получаю данные для списков и вставляю элементы, отменяющие фильтрацию "Все ..."
            var projects = await Rep.GetProjects();
            projects.Insert(0, new Project { Id = skipFilterId, Name = "Все проекты" });
            var users = await Rep.GetUsers();
            users.Insert(0, new User { Id = skipFilterId, Name = "Все работники" });
            var workTypes = await Rep.GetWorkTypes();
            workTypes.Insert(0, new WorkType { Id = skipFilterId, Name = "Все типы работ" });

            // выбираю работы по фильтрам
            var works = new List<User_Stage>();
            if (filters != null)
                works = await ApplyFiltersToWorksAsync(filters, Rep);

            return new ReportModel
            {
                DateFrom = DateTime.Now - TimeSpan.FromDays(30),
                DateTo = DateTime.Now,
                ProjectList = new SelectList(projects, "Id", "Name", skipFilterId),
                UserList = new SelectList(users, "Id", "Name", skipFilterId),
                WorkTypeList = new SelectList(workTypes, "Id", "Name", skipFilterId),
                FilteredWorks = works
            };
        }

        private static async Task<List<User_Stage>> ApplyFiltersToWorksAsync(ChosenFilters filters, IRepository Rep)
        {
            // говнофильтрация по выбранным ограничениям
            var filteredWorks = (await Rep.GetUser_Stages()).
                Where(x => filters.ForAllTime ||
                // но при ограничении даты проверяем нахождение работы в рамках периода
                   x.Date >= filters.PeriodFrom && x.Date <= filters.PeriodTo).
                // если нужно брать всех юзеров
                Where(x => filters.UserId == skipFilterId ||
                // если нужно выбрать юзера по id
                    x.UserId == filters.UserId).
                // любой проект
                Where(x => filters.ProjectId == skipFilterId ||
                // проект по id
                    x.Stage.Document.ProjectId.Value == filters.ProjectId).
                // любой тип работ
                Where(x => filters.WorkTypeId == skipFilterId ||
                // тип по id
                    x.Stage.WorkTypeId == filters.WorkTypeId);

            return filteredWorks.ToList();
        }
    }
}