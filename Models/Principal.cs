using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiktivSkolaLabb._2.Models
{
    [Table("Principal")]
    public partial class Principal
    {
        [Key]
        [Column("Principal_Id")]
        public int PrincipalId { get; set; }
        [StringLength(50)]
        public string Fname { get; set; } = null!;
        [StringLength(50)]
        public string Lname { get; set; } = null!;
        [Column("FkPersonal_Id")]
        public int FkPersonalId { get; set; }

        [ForeignKey("FkPersonalId")]
        [InverseProperty("Principals")]
        public virtual Personal FkPersonal { get; set; } = null!;
    }
}
