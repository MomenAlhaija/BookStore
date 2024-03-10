using System.Collections.Generic;

namespace SchoolProj.DLL.Model
{
    public class ClassTeacherModel
    {
        public int? TeacherId { get; set; }     
        public string FullNameAr { get; set; }
        public string FullNameEn { get; set; }  
        public int Gender { get; set; } 
        public string MaterialFullNameAr { get; set; }  
        public string MaterialFullNameEn { get; set;}
        public List<ClassModel> Classes { get;set; }
        public int MaterialId { get; set; } 
    }
}
