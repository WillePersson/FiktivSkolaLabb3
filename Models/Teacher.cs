using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiktivSkolaLabb._2.Models
{
    [Table("Teacher")]
    public partial class Teacher
    {
        public Teacher()
        {
            Classes = new HashSet<Class>();
        }

        [Key]
        [Column("Teacher_Id")]
        public int TeacherId { get; set; }
        [StringLength(50)]
        public string Fname { get; set; } = null!;
        [StringLength(50)]
        public string Lname { get; set; } = null!;
        [Column("FkClass_Id")]
        public int FkClassId { get; set; }
        [Column("FkPersonal_Id")]
        public int FkPersonalId { get; set; }

        [ForeignKey("FkClassId")]
        [InverseProperty("Teachers")]
        public virtual Class FkClass { get; set; } = null!;
        [ForeignKey("FkPersonalId")]
        [InverseProperty("Teachers")]
        public virtual Personal FkPersonal { get; set; } = null!;
        [InverseProperty("FkTeacher")]
        public virtual ICollection<Class> Classes { get; set; }
    }
}
