using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationRegistryModel
{
    public class User
    {
        public User()
        {
            this.Projects = new HashSet<Project>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
