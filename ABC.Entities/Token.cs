using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABC.Entities
{
    public class Token : TEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ActualToken {get;set;}

        [Required]
        public string ClientToken {get;set;}

        [Required]
        public string RefreshToken {get;set;}
        
    }
}