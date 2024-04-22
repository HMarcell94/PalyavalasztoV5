using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Palyavalaszto.Data.Entitites
{
    [Table("employees")]
 
    public  class employee
    {
        [Key]
        [Required]
        public int EmployeeID { get; set; }

        public int? UserID { get; set; }

        public user user { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string ContactNumber { get; set; }

        public string? Portfolio { get; set; }

        public string? Resume { get; set; }

        public string? Picture { get; set; }
        public string? HighestEducation { get; set; }
        public string? Profession { get; set; }
        public string? Introduction { get; set; }
        public string? Language { get; set; }

        public string? NameofSchool { get; set; }
        public string Address {  get; set; }
public string PreviousWork { get; set; }
public string Position { get; set; }
        
        public ICollection<applications> applications { get; set; }

    }
}