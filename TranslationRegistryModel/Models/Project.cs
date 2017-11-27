using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationRegistryModel
{
    public class Project
    {
        public Project()
        {
            this.Workers = new HashSet<User>();
            this.Documents = new HashSet<Document>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<User> Workers { get; set; }

        public int? OriginalLanguageId { get; set; }
        public virtual Language OriginalLanguage { get; set; }

        public int? FinalLanguageId { get; set; }
        public virtual Language FinalLanguage { get; set; }

        public int WordsNumber { get; set; }

        public ICollection<Document> Documents { get; set; }
    }
}
