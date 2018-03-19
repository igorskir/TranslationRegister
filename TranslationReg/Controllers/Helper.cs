using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    public static class Helper
    {
        public const string uploadDir = "Uploads";
        public const string reportsDir = "Reports";
        public const string avatarsDir = "UserAvatars";
        public const string defaultAvatar = "default.jpg";

        public static async Task<DocFile> SetFile(HttpPostedFileBase file, IRepository Rep, HttpServerUtilityBase Server)
        {
            DocFile docfile = new DocFile
            {
                Date = DateTime.Now,
                Path = GetValidPath(file, Server)
            };
            await Rep.AddDocFile(docfile);
            file.SaveAs(docfile.Path);

            return docfile;
        }

        public static string GetValidPath(HttpPostedFileBase file, HttpServerUtilityBase Server, string subDirectories = null)
        {
            try
            {
                var fileName = Path.GetFileName(file.FileName);
                string path;
                if (subDirectories == null)
                    path = Path.Combine(Server.MapPath($"~/{uploadDir}"), fileName);
                else
                    path = Path.Combine(Server.MapPath($"~/{uploadDir}"), subDirectories, fileName);

                if (File.Exists(path))
                {
                    Guid guid = Guid.NewGuid();
                    fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    fileName += guid.ToString() + extension;
                    
                    path = Path.Combine(Path.GetDirectoryName(path), fileName);
                }
                return path;
            }
            catch (Exception)
            {
                return null;
            }
           
        }
    }
}