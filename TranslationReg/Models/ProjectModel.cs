using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Models
{
    public class ProjectModel
    {
        public Project Project;
        public SelectList LanguagePairs;
        public SelectList ProjectStatus;

        public static async Task<ProjectModel> GetModel(IRepository rep, Project project = null)
        {
            var languagePairs = await rep.GetLanguagePairs();
            var projectStatuses = await rep.GetProjectStatuses();

            if (project == null)
                project = new Project {
                    LanguagePairId = languagePairs[0].Id,
                    ProjectStatuseId = projectStatuses[0].Id
                };

            ProjectModel model = new ProjectModel
            {
                Project = project,
                LanguagePairs = new SelectList(languagePairs, "Id", "Name", project.LanguagePairId),
                ProjectStatus = new SelectList(projectStatuses, "Id", "Name", project.ProjectStatuseId)
            };
            return model;
        }
    }
}