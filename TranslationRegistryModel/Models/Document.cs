using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationRegistryModel
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Path { get; set; }

        public int? UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }

        public int WordsNumber { get; set; }

        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
