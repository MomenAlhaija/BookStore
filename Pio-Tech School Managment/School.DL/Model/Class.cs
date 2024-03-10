using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DL.Model
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        [Required]
        [MaxLength(50)]
        public string ClassNameAr { get; set; }

        [MaxLength(50)]
        public string classNameEn { get; set; }
    }
}
