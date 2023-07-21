using System.ComponentModel.DataAnnotations;

namespace WebBlazorApp.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string AuthorName { get; set; }

        [Required]
        [Range(1, 100,ErrorMessage ="Please select the Author")]
        public int AuthorId { get; set; }
    }
}
