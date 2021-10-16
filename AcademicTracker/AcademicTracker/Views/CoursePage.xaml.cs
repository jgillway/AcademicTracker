using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AcademicTracker.Classes;
using AcademicTracker.Persistance;
using AcademicTracker.Views;
using Plugin.LocalNotifications; 

namespace AcademicTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        private Courses course; 
        public CoursePage(Courses tempCourse)
        {
            course = tempCourse; 

            InitializeComponent();

            Header.Text = course.Name;
            Details.Text = course.StartDate.ToString("MM/dd/yyyy") + " - " + course.EndDate.ToString("MM/dd/yyyy");

            var assessmentList = DBH.GetAssessments(course);

            var assessmentNotificationList = DBH.GetAssessmentNotification(course);

            foreach (Assessments _assessment in assessmentNotificationList)
            {
                if (_assessment.EndDate.Date == DateTime.Now.Date)
                {
                    CrossLocalNotifications.Current.Show(_assessment.Name, "Is Due Today!");
                }
            }

            AssessmentList.ItemsSource = assessmentList; 

            if (assessmentList.Count() >= 2)
            {
                AddAssessment.IsEnabled = false;
                AddAssessment.IsVisible = false;
            }
            else
            {
                AddAssessment.IsEnabled = true;
                AddAssessment.IsVisible = true;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Header.Text = course.Name;
            Details.Text = course.StartDate.ToString("MM/dd/yyyy") + " - " + course.EndDate.ToString("MM/dd/yyyy");
            var assessmentList = DBH.GetAssessments(course);
            AssessmentList.ItemsSource = assessmentList;
            if (assessmentList.Count() >= 2)
            {
                AddAssessment.IsEnabled = false;
                AddAssessment.IsVisible = false;
            }
            else
            {
                AddAssessment.IsEnabled = true;
                AddAssessment.IsVisible = true;
            }

        }

        private void EditCourse_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CourseDetails(course)); 
        }

        private void AddAssessment_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AssessmentAdd(course)); 
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(); 
        }
        private void AssessmentList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Assessments assessment = (Assessments)e.Item;
            Navigation.PushAsync(new AssessmentDetails(assessment));
        }
    }
}