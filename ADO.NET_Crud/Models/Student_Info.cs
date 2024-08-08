using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADO.NET_Crud.Models
{
    public class Student_Info
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Std_Id { get; set; }
        [Required]
        [StringLength(255)]
        [DisplayName("Name")]
        public string Std_Name { get; set;}
        [Required]
        [StringLength(255)]
        [EmailAddress]
        [DisplayName("Email")]
        public string Std_Email { get; set;}
        [Required]
        [StringLength(255)]
        [DisplayName("Phone")]
        public string Std_Phone { get; set;}
        [Required]
        [DisplayName("Age")]
        public int age { get; set; }
        [Required]
        [DisplayName("Image")]
        public string Std_Image { get; set;}
    }
}
