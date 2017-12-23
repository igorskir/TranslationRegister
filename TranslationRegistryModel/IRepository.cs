using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationRegistryModel
{
    public interface IRepository
    {
        void Dispose();

        //---------------------------------- Language CRUD -----------------------------------------------

        Task<List<Language>> GetLanguages();
        Task<Language> GetLanguage(int id);
        Task PutLanguage(Language language);
        Task DeleteLanguage(int id);
        Task AddLanguage(Language language);
        //------------------------------------------------------------------------------------------------


        //---------------------------------- User CRUD ---------------------------------------------------
        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> GetUser(string login);
        Task<User> GetUser(string login, string password);
        Task PutUser(User user);
        Task DeleteUser(int id);
        Task AddUser(User user);
        //------------------------------------------------------------------------------------------------

        //---------------------------------- Document CRUD ---------------------------------------------------
        Task<List<Document>> GetDocuments();
        Task<Document> GetDocument(int id);
        Task PutDocument(Document doc);
        Task DeleteDocument(int id);
        Task AddDocument(Document doc);
        //------------------------------------------------------------------------------------------------

        //---------------------------------- Project CRUD ---------------------------------------------------
        Task<List<Project>> GetProjects();
        Task<Project> GetProject(int id);
        Task PutProject(Project doc);
        Task DeleteProject(int id);
        Task AddProject(Project doc);
        //------------------------------------------------------------------------------------------------

        //---------------------------------- LanguagePair CRUD -----------------------------------------------
        Task<List<LanguagePair>> GetLanguagePairs();
        Task<LanguagePair> GetLanguagePair(int id);
        Task PutLanguagePair(LanguagePair languagePair);
        Task DeleteLanguagePair(int id);
        Task AddLanguagePair(LanguagePair languagePair);
        //------------------------------------------------------------------------------------------------

        //---------------------------------- WorkType CRUD -----------------------------------------------
        Task<List<WorkType>> GetWorkTypes();
        Task<WorkType> GetWorkType(int id);
        Task PutWorkType(WorkType workType);
        Task DeleteWorkType(int id);
        Task AddWorkType(WorkType workType);
        //------------------------------------------------------------------------------------------------

        //---------------------------------- ProjectStatuse CRUD -----------------------------------------------
        Task<List<ProjectStatus>> GetProjectStatuses();
        Task<ProjectStatus> GetProjectStatus(int id);
        Task PutProjectStatus(ProjectStatus projectStatuse);
        Task DeleteProjectStatus(int id);
        Task AddProjectStatus(ProjectStatus projectStatuse);
        //------------------------------------------------------------------------------------------------
    }
}
