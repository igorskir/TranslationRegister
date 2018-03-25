﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TranslationReg.Models;
using TranslationRegistryModel;
using Word = Microsoft.Office.Interop.Word;

namespace TranslationReg.Controllers
{
    // Контроллер для ВЕДЕНИЯ АНАЛИТИКИ - генерации отчетов
    [Authorize]
    public class AnalyticsController : RepositoryController
    {
        public AnalyticsController(IRepository repository) : base(repository) { } // Конструктор

        // GET: Analytics
        public async Task<ActionResult> Index()
        {
            return View("Report", await ReportModel.GetModel(Rep));
        }
        // POST: Analytics
        public async Task<FileResult> FormReport([Bind(Prefix = "filters")]
            ChosenFilters filters)
        {
            var filteredWorks = await ReportModel.ApplyFiltersToWorksAsync(Rep, filters);
            var groupedWorks = ReportModel.GroupReportData(filteredWorks);
            if (groupedWorks == null) return null; //todo EXCEPTION
            string filepath = await ComFileGeneration(filters, groupedWorks);

            FileInfo file = new FileInfo(filepath);
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            string fileName = file.Name;
            System.IO.File.Delete(filepath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        //COM 
        public Task<string> ComFileGeneration(ChosenFilters filters, List<IEnumerable<IGrouping<int, User_Stage>>> groupedWorks)
        {
            return Task.Run(() =>
            {
                Word.Application app = new Word.Application { Visible = false };

                Word.Document doc = new Word.Document();
                object missing = System.Reflection.Missing.Value;
                doc = app.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                foreach (Word.Section section in doc.Sections)
                {
                    //Хедер документа
                    Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                    headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    headerRange.Font.ColorIndex = Word.WdColorIndex.wdAuto;
                    headerRange.Font.Size = 10;
                    headerRange.Text = "Отчет сформирован " + DateTime.Now.ToString();
                }

                // Вывод фильтров
                doc.Content.SetRange(0, 0);
                if (!filters.ForAllTime)
                    doc.Content.Text = "Отчет c " + filters.PeriodFrom.ToString() + " по " + filters.PeriodTo.ToString() + Environment.NewLine;
                else
                    doc.Content.Text = "Отчет за все время" + Environment.NewLine;

                Word.Paragraph para1 = doc.Content.Paragraphs.Add(ref missing);
                para1.Range.Text = "Таблица работ:";
                para1.Range.InsertParagraphAfter();

                //Вывод таблицы
                Word.Table ReportTable = doc.Tables.Add(para1.Range, 1, 3, ref missing, ref missing);
                ReportTable.Rows[1].Cells[1].Range.Text = "Пользователь";
                ReportTable.Rows[1].Cells[2].Range.Text = "Тип работ";
                ReportTable.Rows[1].Cells[3].Range.Text = "Сумма";

                if (groupedWorks != null && groupedWorks.Count != 0)
                {
                    foreach (var userWorks in groupedWorks)
                    {
                        var userName = userWorks.First().First().User.Name;
                        foreach (var typedWorks in userWorks)
                        {
                            ReportTable.Borders.Enable = 1;
                            var type = typedWorks.First().Stage.WorkType.Name;
                            var summ = typedWorks.Sum(x => x.Amount);
                            var row = ReportTable.Rows.Add();
                            row.Cells[1].Range.Text = userName;
                            row.Cells[2].Range.Text = type;
                            row.Cells[3].Range.Text = summ.ToString();
                        }
                    }
                }

                // Путь сохранения отчета на сервере
                var saveDir = Server.MapPath(Path.Combine($"~/{Helper.uploadDir}", Helper.reportsDir)) + "//";
                Directory.CreateDirectory(saveDir);
                string filepath = saveDir + "Отчет " + DateTime.Now.ToString().Replace(":", "-") + ".docx";

                // Завершение работы
                object filename = filepath;
                doc.SaveAs2(ref filename);
                doc.Close(ref missing, ref missing, ref missing);
                doc = null;
                app.Quit(ref missing, ref missing, ref missing);
                app = null;

                return filepath;
            });
        }
    }
}