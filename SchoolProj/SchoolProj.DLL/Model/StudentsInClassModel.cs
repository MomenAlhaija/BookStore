namespace SchoolProj.DLL.Model
{
    public class StudentsInClassModel
    {
        public string ClassNemeAr { get; set; }
        public string ClassNameEn { get; set; }
        public int ClassId { get; set; }    
        public string StudentNameAr { get; set; } 
        public string StudentNameEn { get; set; }
        public int StudentId { get; set;}
        public int UserId { get; set;}  
        public int Gender { get; set;}  
        public int Age { get; set;}
        public decimal? Grade { get; set;}
    }
}
