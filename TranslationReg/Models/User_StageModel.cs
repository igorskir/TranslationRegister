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

        public static async Task<User_StageModel> GetModel(IRepository Rep, string login, int stageId)
        {
            var users = await Rep.GetUsers();
            var me = await Rep.GetUser(login);

            User_StageModel model = new User_StageModel
            {
                work = new User_Stage{ StageId = stageId },
                usersSelectList = new SelectList(users, "Id", "Name", me.Id)
            };

            return model;
        }
    }
}