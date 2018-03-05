using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationReg.Models;
using TranslationRegistryModel;
using System.Web.Security;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Specialized;
using FormsAuthenticationExtensions;
using ImageResizer;
using ImageResizer.Resizing;

namespace TranslationReg.Controllers
{
    public class AccountController : RepositoryController
    {
        public AccountController(IRepository repository) : base(repository) { }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = await Rep.GetUser(model.Name, model.Password);
                if (user != null)
                {
                    // дополнительные данные задавать здесь
                    var ticketData = new NameValueCollection{{ "avatarPath", user.AvatarPath }};
                    new FormsAuthentication().SetAuthCookie(model.Name, true, ticketData);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model, [Bind(Include = "avatar")] HttpPostedFileBase avatar)
        {
            if (ModelState.IsValid)
            {
                User user = await Rep.GetUser(model.Name, model.Password);
                if (user == null)
                {
                    // создаем нового пользователя
                    user = new User { Name = model.Name, Password = model.Password };
                    await Rep.AddUser(user);
                    user = await Rep.GetUser(model.Name, model.Password);
                    // если пользователь удачно добавлен в бд - добавляем авку, аутентифицируем, 
                    if (user != null)
                    {
                        await SaveSmallAvatar(avatar, user);
                        // дополнительные данные задавать здесь
                        var ticketData = new NameValueCollection { { "avatarPath", user.AvatarPath } };
                        new FormsAuthentication().SetAuthCookie(model.Name, true, ticketData);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public async Task SaveSmallAvatar(HttpPostedFileBase file, User user, int width = 70, int height = 70)
        {
            try
            {
                var filePath = Helper.GetValidPath(file, Server, Helper.avatarsDir);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                file.SaveAs(filePath);

                var relativePath = Path.Combine(Helper.uploadDir, Helper.avatarsDir, Path.GetFileName(filePath));

                // свойства аватарки
                // todo фон задать
                Instructions instructions = new Instructions() {
                    Width = 50,
                    Height = 50,
                    Format = "png",
                    RoundCorners = new double[] { 50.0 },
                    BackgroundColor = "Default",

                };
                ImageJob imageJob = new ImageJob(filePath, filePath, instructions, true, false);
                imageJob.Build();

                user.AvatarPath = relativePath;
                await Rep.PutUser(user);
            }
            catch (Exception)
            {

            }

        }
    }
}