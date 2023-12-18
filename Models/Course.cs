using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiktivSkolaLabb._2.Models
{
    [Table("Course")]
    public partial class Course
    {
        [Key]
        [Column("Course_Id")]
        public int CourseId { get; set; }
        [StringLength(50)]
        public string CourseName { get; set; } = null!;
        [Column("FkClass_Id")]
        public int FkClassId { get; set; }
        [Column("FkTeacher_Id")]
        public int FkTeacherId { get; set; }
    }
}
