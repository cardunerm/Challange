using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioLaNacion.DataModel
{
    public class BasePerson
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("first_name")]
        [MaxLength(255)]
        public string FirstName { get; set; }
        [Required]
        [Column("last_name")]
        [MaxLength(255)]
        public string LastName { get; set; }
        [Required]
        [Column("email")]
        [MaxLength(255)]
        public string Email { get; set; }
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

    }
}
