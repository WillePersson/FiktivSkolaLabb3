using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiktivSkolaLabb._2.Models
{
    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        [Column("Admin_Id")]
        public int AdminId { get; set; }
        [StringLength(50)]
        public string Fname { get; set; } = null!;
        [StringLength(50)]
        public string Lname { get; set; } = null!;
        [Column("FkPersonal_Id")]
        public int FkPersonalId { get; set; }

        [ForeignKey("FkPersonalId")]
        [InverseProperty("Admins")]
        public virtual Personal FkPersonal { get; set; } = null!;
    }
}
