using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AcademicTracker.Persistance;
using AcademicTracker.Classes;

namespace AcademicTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var termlist= DBH.GetTerms();
            
            if (termlist.Count() == 0)
            {
                var sampleTerm = new Terms();
                sampleTerm.Title = "Sample Term";
                sampleTerm.StartDate = DateTime.Now;
                sampleTerm.EndDate = DateTime.Now;
                DBH.InsertTerm(sampleTerm);

                var sampleCourse = new Courses();
                sampleCourse.TermId = sampleTerm.Id; 
                sampleCourse.Name = "Sample Course";
                sampleCourse.StartDate = DateTime.Now;
                sampleCourse.EndDate = DateTime.Now;
                sampleCourse.Status = "Active";
                sampleCourse.InstructorName = "Jonathan Gillway";
                sampleCourse.InstructorEmail = "jgillw1@wgu.edu";
                sampleCourse.InstructorPhone = "2073232189";
                sampleCourse.Notes = "Sample Note";
                sampleCourse.Notifications = true;
                DBH.InsertCourse(sampleCourse);

                var sampleAssessment = new Assessments();
                sampleAssessment.CourseId = sampleCourse.Id;
                sampleAssessment.Name = "Sample Assessment";
                sampleAssessment.Type = "Performance"; 
                sampleAssessment.StartDate = DateTime.Now;
                sampleAssessment.EndDate = DateTime.Now;
                sampleAssessment.Notifications = true;
                DBH.InsertAssessment(sampleAssessment);

                var sampleAssessment2 = new Assessments();
                sampleAssessment2.CourseId = sampleCourse.Id;
                sampleAssessment2.Name = "Sample Assessment";
                sampleAssessment2.Type = "Objective";
                sampleAssessment2.StartDate = DateTime.Now;
                sampleAssessment2.EndDate = DateTime.Now;
                sampleAssessment2.Notifications = true;
                DBH.InsertAssessment(sampleAssessment);

                termlist = DBH.GetTerms();
                TermList.ItemsSource = termlist;
            }
            else
            {
                TermList.ItemsSource = termlist;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            TermList.ItemsSource = DBH.GetTerms();

        }

        private void AddTerm_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TermAdd());
        }

        private void TermList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Terms term = (Terms)e.Item;
            Navigation.PushAsync(new TermPage(term)); 
        }

    }
}