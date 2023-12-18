using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiktivSkolaLabb._2.Models
{
    [Table("Class")]
    public partial class Class
    {
        public Class()
        {
            Students = new HashSet<Student>();
            Teachers = new HashSet<Teacher>();
        }

        [Key]
        [Column("Class_Id")]
        public int ClassId { get; set; }
        [StringLength(50)]
        public string ClassName { get; set; } = null!;
        [Column("FkTeacher_Id")]
        public int? FkTeacherId { get; set; }

        [ForeignKey("FkTeacherId")]
        [InverseProperty("Classes")]
        public virtual Teacher? FkTeacher { get; set; }
        [InverseProperty("FkClass")]
        public virtual ICollection<Student> Students { get; set; }
        [InverseProperty("FkClass")]
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
