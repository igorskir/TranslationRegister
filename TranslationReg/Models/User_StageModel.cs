using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Models
{
    public class User_StageModel
    {
        public SelectList usersSelectList;
        public User_Stage work;
        public static async Task<User_StageModel> GetModel(IRepository Rep, string login, int stageId, User_Stage stage = null)
        {
            var users = await Rep.GetUsers();
            if (stage == null)
                stage = new User_Stage { StageId = stageId };
            User user;
            if (stage.User == null)
                user = await Rep.GetUser(login);
            else
                user = await Rep.GetUser(stage.User.Id);
            User_StageModel model = new User_StageModel
            {
                work = stage,
                usersSelectList = new SelectList(users, "Id", "Name", user.Id)
            };
            return model;
        }
    }
}