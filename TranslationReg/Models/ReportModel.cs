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
        public const int skipFilterId = -1;

        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }

        public SelectList ProjectList { get; set; }
        public SelectList UserList { get; set; }
        public SelectList WorkTypeList { get; set; }

        public List<IEnumerable<IGrouping<int, User_Stage>>> ReportSections { get; set; }

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
            var filteredWorks = new List<User_Stage>();
            if (filters != null)
                filteredWorks = await ApplyFiltersToWorksAsync(Rep, filters );

            // группирую данные для представления
            var reportData = GroupReportData(filteredWorks.ToList());

            return new ReportModel
            {
                ProjectList = new SelectList(projects, "Id", "Name", skipFilterId),
                UserList = new SelectList(users, "Id", "Name", skipFilterId),
                WorkTypeList = new SelectList(workTypes, "Id", "Name", skipFilterId),
                ReportSections = reportData
            };
        }

        public static async Task<List<User_Stage>> ApplyFiltersToWorksAsync(IRepository Rep, ChosenFilters filters)
        {
            //период выбираю включительно относительно выбранного дня от N числа 00:00:00 + 24 часа)
            if (!filters.ForAllTime)
                filters.PeriodTo += TimeSpan.FromDays(1);

            // фильтрация по выбранным ограничениям
            var filteredWorks = (await Rep.GetUser_Stages()).
                // для начала нужно брать выполненные работы. по таким закреплен файл, по файлу и выберем
                Where(x=>x.DocFileId!=null).
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
                // тип работ по id
                    x.Stage.WorkTypeId == filters.WorkTypeId);


            return filteredWorks.ToList();
        }

        // данные нужно магически сгруппировать чтобы хорошо представить в отчете
        public static List<IEnumerable<IGrouping<int, User_Stage>>> GroupReportData(List<User_Stage> filteredWorks)
        {
            if (filteredWorks != null && filteredWorks.Count != 0)
            {
                List<IEnumerable<IGrouping<int, User_Stage>>> typedUsersWorks = new List<IEnumerable<IGrouping<int, User_Stage>>>();
                // работы, сгруппированые по юзерам
                var usersWorks = filteredWorks.GroupBy(x => x.UserId);
                // каждый список работ юзера
                foreach (var userWorks in usersWorks)
                    //группируем по типу работ
                    typedUsersWorks.Add(userWorks.GroupBy(x => x.Stage.WorkTypeId));

                return typedUsersWorks;
            }
            else
                return null;
        }

    }
}