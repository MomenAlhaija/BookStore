using SchoolProj.DLL.Shared;
using System.ComponentModel.DataAnnotations;

namespace SchoolProj.DLL.Model
{
    public class MarkModel
    {
        public int? ClassId { get; set; }
        public int? MaterialId { get; set; }
        public  int? StudentId { get; set; }
        [Range(GradeConst.MinGrade,GradeConst.MaxGrade)]
        public decimal? Grade { get; set; }
        public string MaterialNameAr { get; set; }=string.Empty;
        public string MaterialNameEn { get; set; } = string.Empty;

    }
}
