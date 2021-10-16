using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AcademicTracker.Classes
{
    [Table("Courses")]
    public class Courses
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TermId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        public string Notes { get; set; }
        public bool Notifications { get; set; }


    }
}
