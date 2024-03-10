using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Entities
{
    public class Class
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string ClassNameAr { get; set; }
        [MaxLength(50)]
        public string? ClassNameEn { get; set; }
    }
}
