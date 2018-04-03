using FormsAuthenticationExtensions;
using ImageResizer;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TranslationReg.Models;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    // Контроллер УЧЕТНЫХ ЗАПИСЕЙ пользователей
    [AllowAnonymous]
    public class AccountController : RepositoryController
    {
        public AccountController(IRepository repository) : base(repository) { } // Конструктор

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
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
                    Authentificate(user);
                    return RedirectToAction("Index", "Projects");
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
                    // если пользователь удачно добавлен в бд - добавляем авку, аутентифицируем
                    if (user != null)
                    {
                        if (avatar != null && avatar.ContentLength != 0)
                            await SaveSmallAvatar(avatar, user);
                        else
                            user.AvatarPath = Path.Combine(Helper.uploadDir, Helper.avatarsDir, Helper.defaultAvatar);

                        Authentificate(user);
                        return RedirectToAction("Index", "Projects");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }

        private void Authentificate(User user)
        {
            // дополнительные данные задавать здесь - в ticketData
            var ticketData = new NameValueCollection { { "avatarPath", user.AvatarPath } };
            new FormsAuthentication().SetAuthCookie(user.Name, true, ticketData);
        }
        public async Task SaveSmallAvatar(HttpPostedFileBase file, User user, int width = 48, int height = 48)
        {
            var filePath = Helper.GetValidPath(file, Server, Helper.avatarsDir);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                // сохранение оригинала
                file.SaveAs(filePath);

                var relativePath = Path.Combine(Helper.uploadDir, Helper.avatarsDir, Path.GetFileName(filePath));

                // свойства аватарки
                Instructions instructions = new Instructions() {
                    Width = width,
                    Height = height,
                    Format = "png",
                    RoundCorners = new double[] {55.0 },
                    BackgroundColor = "Default",
                    Mode = FitMode.Stretch
                };
                // построение авки и сохранение
                ImageJob imageJob = new ImageJob(filePath, filePath, instructions, true, false);
                imageJob.Build();

                // сохранение в бд
                user.AvatarPath = relativePath;
                await Rep.PutUser(user);
            }
            catch (Exception)
            {
                // удаление неудачно-загруженного файла
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                user.AvatarPath = Path.Combine(Helper.uploadDir, Helper.avatarsDir, Helper.defaultAvatar);
                await Rep.PutUser(user);
            }
        }
    }
}