using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Entities
{
    public class Author : TEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string PhotoName { get; set; }

        public string PhotoContent { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
