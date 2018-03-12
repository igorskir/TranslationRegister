using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationReg.Models;
using TranslationRegistryModel;
using Word = Microsoft.Office.Interop.Word;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.IO;

namespace TranslationReg.Controllers
{
    [Authorize]
    public class AnalyticsController : RepositoryController
    {
        public AnalyticsController(IRepository repository) : base(repository) { }

        // GET: Analytics
        public async Task<ActionResult> Index()
        {
            return View("Report", await ReportModel.GetModel(Rep));
        }

        // POST: Analytics
        public async Task<FileResult> FormReport([Bind(Prefix = "filters")]
            ChosenFilters filters)
        {
            var result = await ReportModel.GetModel(Rep, filters);
            string filepath = await ComFileGeneration(filters, result);
            
            FileInfo file = new FileInfo(filepath);
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            string fileName = file.Name;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }


      
            public async Task<string> ComFileGeneration(ChosenFilters filters, ReportModel data) //COM 
        {
            
            Word.Application app = new Word.Application();
            app.Visible = false;

            Word.Document doc = new Word.Document();
            object missing = System.Reflection.Missing.Value;
            doc = app.Documents.Add(ref missing, ref missing, ref missing, ref missing);

            foreach (Word.Section section in doc.Sections)
            {
                //Хедер документа
                Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                headerRange.Font.ColorIndex = Word.WdColorIndex.wdBlue;
                headerRange.Font.Size = 10;
                headerRange.Text = "Отчет сформирован "  + DateTime.Now.ToString();
            }

            doc.Content.SetRange(0, 0);
            doc.Content.Text = "Отчет c " + filters.PeriodFrom.ToString() +  " по " + filters.PeriodTo.ToString() + Environment.NewLine;

            Word.Paragraph para1 = doc.Content.Paragraphs.Add(ref missing);
            para1.Range.Text = "Таблица:";
            para1.Range.InsertParagraphAfter();

            int TableRows = data.UserList.Count();
            Word.Table ReportTable = doc.Tables.Add(para1.Range, TableRows, 3, ref missing, ref missing);

            ReportTable.Borders.Enable = 1;
            foreach (Word.Row row in ReportTable.Rows)
            {
                foreach (Word.Cell cell in row.Cells)
                {
                    //Первая строка
                    if (cell.RowIndex == 1)
                    {
                        if (cell.ColumnIndex == 1)
                            cell.Range.Text = "Пользователь";
                        if (cell.ColumnIndex == 2)
                            cell.Range.Text = "Тип работ";
                        if (cell.ColumnIndex == 3)
                            cell.Range.Text = "Сумма";

                        //Стиль ячеек
                        cell.Range.Font.Bold = 1;
                        cell.Range.Font.Name = "verdana";
                        cell.Range.Font.Size = 10;
                        cell.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray25;
                        cell.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        cell.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                    }

                    //Данные таблицы
                    else
                    {
                        if (cell.ColumnIndex == 1)
                            cell.Range.Text = "Пользователь " + (cell.RowIndex -1).ToString(); 
                        if (cell.ColumnIndex == 2)
                            cell.Range.Text = "Тип работ " + (cell.RowIndex - 1).ToString();
                        if (cell.ColumnIndex == 3)
                            cell.Range.Text = "Сумма " + (cell.RowIndex - 1).ToString();
                        
                        cell.Range.Font.Size = 10;
                    } //todo данные отчета
                }
            }
            //путь сохранения отчета на сервере
            string filepath = @"D:\Report" + DateTime.Now.Date.ToString("d") + ".docx";
          
            object filename = filepath;
            doc.SaveAs2(ref filename);
            doc.Close(ref missing, ref missing, ref missing);
            doc = null;
            app.Quit(ref missing, ref missing, ref missing);
            app = null;

            return filepath;
        }






    }


}