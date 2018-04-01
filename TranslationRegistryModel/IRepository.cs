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

        //---------------------------------- Document CRUD -----------------------------------------------
        Task<List<Document>> GetDocuments();
        Task<List<Document>> GetDocumentsByStr(string searchToken);
        Task<List<Document>> GetMyDocuments(string login);
        Task<List<Document>> GetMyWorkDocuments(string login);
        Task<Document> GetDocument(int id);
        Task PutDocument(Document doc);
        Task DeleteDocument(int id);
        Task AddDocument(Document doc);
        //------------------------------------------------------------------------------------------------

        //---------------------------------- Project CRUD ------------------------------------------------
        Task<List<Project>> GetProjects();
        Task<List<Project>> GetProjectsByStr(string searchToken);
        Task<List<Project>> GetMyProjects(string login);
        Task<List<Project>> GetProjectsInWork();
        Task<List<Project>> GetDoneProjects();
        Task<Project> GetProject(int id);
        Task PutProject(Project doc);
        Task DeleteProject(int id);
        Task AddProject(Project doc);
        //------------------------------------------------------------------------------------------------

        //---------------------------------- LanguagePair CRUD -------------------------------------------
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

        //---------------------------------- ProjectStatuse CRUD -----------------------------------------
        Task<List<ProjectStatus>> GetProjectStatuses();
        Task<ProjectStatus> GetProjectStatus(int id);
        Task PutProjectStatus(ProjectStatus projectStatuse);
        Task DeleteProjectStatus(int id);
        Task AddProjectStatus(ProjectStatus projectStatuse);
        //-----------------------------------------------------------------------------------------------

        //---------------------------------- DocFile CRUD -----------------------------------------------
        Task<List<DocFile>> GetDocFiles();
        Task<DocFile> GetDocFile(int id);
        Task PutDocFile(DocFile docFile);
        Task DeleteDocFile(int id);
        Task AddDocFile(DocFile docFile);
        //------------------------------------------------------------------------------------------------

        //---------------------------------- DocStage CRUD -----------------------------------------------
        Task<List<DocStage>> GetDocStages();
        Task<DocStage> GetDocStage(int id);
        Task PutDocStage(DocStage docStage);
        Task DeleteDocStage(int id);
        Task AddDocStage(DocStage docStage);
        //------------------------------------------------------------------------------------------------

        //---------------------------------- User_Stage CRUD ---------------------------------------------
        Task<List<User_Stage>> GetUser_Stages();
        Task<List<User_Stage>> GetMyTasks(string login);
        Task<User_Stage> GetUser_Stage(int id);
        Task PutUser_Stage(User_Stage User_Stage);
        Task DeleteUser_Stage(int id);
        Task AddUser_Stage(User_Stage User_Stage);
        //------------------------------------------------------------------------------------------------

        //---------------------------------- UnitOfMeasure CRUD ------------------------------------------
        Task<List<UnitOfMeasure>> GetUnitsOfMeasure();
        Task<UnitOfMeasure> GetUnitOfMeasure(int id);
        Task PutUnitOfMeasure(UnitOfMeasure UnitOfMeasure);
        Task DeleteUnitOfMeasure(int id);
        Task AddUnitOfMeasure(UnitOfMeasure UnitOfMeasure);
        //------------------------------------------------------------------------------------------------

    }
}
