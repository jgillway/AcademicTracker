using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AcademicTracker.Classes;
using SQLite;

namespace AcademicTracker.Persistance
{
    public class DBH
    {
        public static void InitializeDB()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Terms>();
                conn.CreateTable<Courses>();
                conn.CreateTable<Assessments>(); 
            }
        }

        //Terms
        public static void InsertTerm(Terms term)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.Insert(term);
            }
        }
        public static void UpdateTerm(Terms term)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.Update(term);
            }
        }
        public static void DeleteTerm(Terms term)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.Delete(term); 
            }
        }
        public static List<Terms> GetTerms()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                return conn.Table<Terms>().ToList();
            }
        }

        //Courses
        public static void InsertCourse(Courses course)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.Insert(course);
            }
        }
        public static void UpdateCourse(Courses course)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.Update(course);
            }
        }
        public static void DeleteCourse(Courses course)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.Delete(course);
            }
        }
        public static List<Courses> GetCourses(Terms term)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                return conn.Query<Courses>("SELECT * FROM Courses WHERE TermId = ?", term.Id).ToList(); 
            }
        }

        //Assessments
        public static void InsertAssessment(Assessments assessment)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.Insert(assessment);
            }
        }
        public static void UpdateAssessment(Assessments assessment)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.Update(assessment);
            }
        }
        public static void DeleteAssessment(Assessments assessment)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.Delete(assessment);
            }
        }
        public static List<Assessments> GetAssessments(Courses course)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                return conn.Query<Assessments>("SELECT * FROM Assessments WHERE CourseId = ?", course.Id); 
            }
        }

        //Notifications
        public static List<Courses> GetCourseNotification(Terms term)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                return conn.Query<Courses>("SELECT * FROM Courses WHERE Notifications = true and TermId = ? ", term.Id); 
            }
        }
        public static List<Assessments> GetAssessmentNotification(Courses course)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                return conn.Query<Assessments>("SELECT * FROM Assessments WHERE Notifications = true and CourseId = ? ", course.Id);
            }
        }
    }
}
