using System;
using System.Collections.Generic;
using System.Text;
using SQLite; 

namespace AcademicTracker.Classes
{
    [Table("Terms")]
    public class Terms
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
