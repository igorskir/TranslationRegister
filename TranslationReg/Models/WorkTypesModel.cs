using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Models
{
    public class WorkTypesModel
    {
        public List<WorkType> workTypes;
        public List<UnitOfMeasure> unitsOfMeasure;
        public List<ProjectStatus> projectStatuses;

        public static async Task<WorkTypesModel> GetModel(IRepository Rep)
        {
            WorkTypesModel model = new WorkTypesModel
            {
                workTypes = await Rep.GetWorkTypes(),
                unitsOfMeasure = await Rep.GetUnitsOfMeasure(),
                projectStatuses = await Rep.GetProjectStatuses()
            };
            return model;
        }
    }
}