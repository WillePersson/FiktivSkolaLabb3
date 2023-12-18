using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiktivSkolaLabb._2.Models
{
    [Keyless]
    [Table("Course_Grades")]
    public partial class CourseGrade
    {
        [Column("FkStudent_Id")]
        public int FkStudentId { get; set; }
        [Column("FkCourse_Id")]
        public int FkCourseId { get; set; }
        [Column("FkTeacher_Id")]
        public int FkTeacherId { get; set; }
        [Column("CourseGrade")]
        [StringLength(10)]
        public string CourseGrade1 { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime CourseDate { get; set; }

        [ForeignKey("FkCourseId")]
        public virtual Course FkCourse { get; set; } = null!;
        [ForeignKey("FkStudentId")]
        public virtual Student FkStudent { get; set; } = null!;
        [ForeignKey("FkTeacherId")]
        public virtual Teacher FkTeacher { get; set; } = null!;
    }
}
