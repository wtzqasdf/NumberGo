using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NumberGo.Models.Tables 
{
    public class Profile
    {
        [Key]
        [Column("account")]
        public string Account { get; set; }

        [Column("point")]
        public int Point { get; set; }
    }
}
