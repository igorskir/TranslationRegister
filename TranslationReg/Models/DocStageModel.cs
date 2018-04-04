using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Models
{
    public class StageForProjModel
    {
        public int projectId;
        public int worktypeId;
        public SelectList worktypes;

        public static async Task<StageForProjModel> GetModel(IRepository Rep, Project project)
        {
            //список доступных стадий
            List<WorkType> availableWorkTypes = await Rep.GetWorkTypes();
            List<WorkType> takenWorktypes = project.Documents.SelectMany(x => x.Stages).Select(x => x.WorkType).ToList();
            availableWorkTypes = availableWorkTypes.Except(takenWorktypes).ToList();
            if (availableWorkTypes.Any())
            {
                var selectList = new SelectList(availableWorkTypes, "Id", "Name", availableWorkTypes[0].Id);
                StageForProjModel model = new StageForProjModel
                {
                    projectId = project.Id,
                    worktypes = selectList
                };
                return model;
            }
            return null;
        }
    }
}