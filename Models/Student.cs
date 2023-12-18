using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiktivSkolaLabb._2.Models
{
    [Table("Student")]
    public partial class Student
    {
        [Key]
        [Column("Student_Id")]
        public int StudentId { get; set; }
        [StringLength(50)]
        public string Fname { get; set; } = null!;
        [StringLength(50)]
        public string Lname { get; set; } = null!;
        public int PersonalIdentityNumber { get; set; }
        public int ZipCode { get; set; }
        [StringLength(50)]
        public string Street { get; set; } = null!;
        public int Housenumber { get; set; }
        [Column("FkClass_Id")]
        public int? FkClassId { get; set; }

        [ForeignKey("FkClassId")]
        [InverseProperty("Students")]
        public virtual Class? FkClass { get; set; }
    }
}
