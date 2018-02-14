using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    public static class Helper
    {
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

        public static string GetValidPath(HttpPostedFileBase file, HttpServerUtilityBase Server)
        {
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
            if (System.IO.File.Exists(path))
            {
                Guid guid = Guid.NewGuid();
                fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                fileName += guid.ToString() + extension;
                path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
            }
            return path;
        }

    }
}