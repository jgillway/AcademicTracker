using System;
using System.Collections.Generic;
using System.Text;
using SQLite; 

namespace AcademicTracker.Classes
{
    [Table("Assessments")]
    public class Assessments
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Notifications { get; set; }
    }
}
