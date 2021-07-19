using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NumberGo.Models.Tables
{
    public class Score
    {
        [Key]
        [Column("queryid")]
        public string QueryID { get; set; }

        [Column("nickname")]
        public string NickName { get; set; }

        [Column("level")]
        public short Level { get; set; }

        [Column("elapsedtime")]
        public string ElapsedTime { get; set; }

        [Column("postdate")]
        public DateTime PostDate { get; set; }
    }
}
