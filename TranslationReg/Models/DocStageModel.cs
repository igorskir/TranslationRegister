using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Models
{
    public class StageForDocModel
    {
        public int docId;
        public SelectList worktypes;
        public bool available;

        public static async Task<StageForDocModel> GetModel(IRepository Rep, Document doc)
        {
            //список доступных стадий
            List<WorkType> availableWorkTypes = await Rep.GetWorkTypes();
            List<WorkType> takenWorktypes = doc.Stages.Select(x => x.WorkType).ToList();
            availableWorkTypes = availableWorkTypes.Except(takenWorktypes).ToList();
            var selectList = new SelectList(availableWorkTypes, "Id", "Name", availableWorkTypes[0].Id);
            StageForDocModel model = new StageForDocModel
            {
                docId = doc.Id,
                worktypes = selectList,
                available = availableWorkTypes.Any()
            };
            return model;
        }
    }
}