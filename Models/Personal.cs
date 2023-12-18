using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiktivSkolaLabb._2.Models
{
    [Table("Personal")]
    public partial class Personal
    {
        public Personal()
        {
            Admins = new HashSet<Admin>();
            Janitors = new HashSet<Janitor>();
            Principals = new HashSet<Principal>();
            Teachers = new HashSet<Teacher>();
        }

        [Key]
        [Column("Personal_Id")]
        public int PersonalId { get; set; }
        [StringLength(50)]
        public string Fname { get; set; } = null!;
        [StringLength(50)]
        public string Lname { get; set; } = null!;
        [StringLength(50)]
        public string Profession { get; set; } = null!;
        public int Salary { get; set; }

        [InverseProperty("FkPersonal")]
        public virtual ICollection<Admin> Admins { get; set; }
        [InverseProperty("FkPersonal")]
        public virtual ICollection<Janitor> Janitors { get; set; }
        [InverseProperty("FkPersonal")]
        public virtual ICollection<Principal> Principals { get; set; }
        [InverseProperty("FkPersonal")]
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
