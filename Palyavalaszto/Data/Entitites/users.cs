        using System;
        using System.Collections.Generic;
        using System.ComponentModel.DataAnnotations;
        using System.ComponentModel.DataAnnotations.Schema;
namespace Palyavalaszto.Data.Entitites
{
           [Table("users")]
    public class user
    {
        [Key]
        [Required]
        public int UserID { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
       public string? salt { get; set; }

        //     public byte[] ProfilePicture { get; set; }

        public int? RoleID { get; set; } = 2;

        public role role { get; set; }

        public ICollection<employee> employees { get; set; }

        public ICollection<employer> employers { get; set; }

    }
}