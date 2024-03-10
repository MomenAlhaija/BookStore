using System.Collections.Generic;

namespace SchoolProj.DLL.Model
{
    public class StudentMarksModel
    {
        public string FullNameAr { get; set; }
        public string FullNameEn { get; set; }
        public int StudentId { get; set; }  
        public int ClasseId { get; set; }   
        public string ClassNameAr { get; set; }
        public string ClassNameEn { get; set; }
        public StudentModel student { get; set; }   
        public List<MarkModel> Marks { get; set; }  
    }
}
