using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DesafioLaNacion.DataModel
{
    [Table("contact_record")]
    public class ContactRecord : BasePerson
    {
        [Required]
        [Column("Company")]
        [MaxLength(255)]
        public string Company { get; set; }
        [Required]
        [Column("ProfileImage")]
        [MaxLength(255)]
        public string ProfileImage { get; set; }
        [Required]
        [Column("PersonalPhoneNumber")]
        [MaxLength(255)]
        public string PersonalPhoneNumber { get; set; }
        [Required]
        [Column("WorkPhoneNumber")]
        [MaxLength(255)]
        public string WorkPhoneNumber { get; set; }
        [Required]
        [Column("Adress")]
        [MaxLength(255)]
        public string Adress { get; set; }

        [Required]
        [Column("Ciudad")]
        [MaxLength(255)]
        public string Ciudad { get; set; }


    }
}
