using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationRegistryModel
{
    public interface IRepository
    {
        //---------------------------------- Language CRUD -----------------------------------------------

        Task<List<Language>> GetLanguages();
        Task<Language> GetLanguage(int id);
        Task PutLanguage(Language language);
        Task DeleteLanguage(int id);
        Task AddLanguage(Language language);
        //------------------------------------------------------------------------------------------------


        //---------------------------------- User CRUD ---------------------------------------------------
        List<User> GetUsers();
        User GetUser(int id);
        void PutUser(User user);
        void DeleteUser(int id);
        void AddUser(User user);
        //------------------------------------------------------------------------------------------------

        //---------------------------------- Document CRUD ---------------------------------------------------
        List<Document> GetDocuments();
        Document GetDocument(int id);
        void PutDocument(Document doc);
        void DeleteDocument(int id);
        void AddDocument(Document doc);
        //------------------------------------------------------------------------------------------------

        #region asyncUsr
        /*
        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);
        Task PutUser(User user);
        Task DeleteUser(int id);
        Task AddUser(User user);
        */
        #endregion

        #region asyncLang
        /*
        Task<List<Language>> GetLanguages();
        Task<Language> GetLanguage(int id);
        Task PutLanguage(Language language);
        Task DeleteLanguage(int id);
        Task AddLanguage(Language language);
        */
        #endregion
    }
}
