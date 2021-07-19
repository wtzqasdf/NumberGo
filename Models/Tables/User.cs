using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NumberGo.Models.Tables
{
    public class User
    {
        [Key]
        [Column("account")]
        public string Account { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("regdate")]
        public DateTime RegDate { get; set; }
    }
}
