using System.Collections.Generic;

namespace SchoolProj.DLL.Model
{
    public class StaticsModel
    {
        public int TotalStudents { get; set; }
        public int TotalTeachers { get; set; }
        public int TotalClasses { get; set; }
        public int TotalUsers { get; set; }
        public string AdminName { get; set; }
        public IEnumerable<TeacherModel> Teachers { get; set; }
    }
}
