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
    public partial class TermPage : ContentPage
    {
        private Terms term;
        public TermPage(Terms tempTerm)
        {
            term = tempTerm; 

            InitializeComponent();

            Header.Text = term.Title.ToString();
            Details.Text = term.StartDate.ToString("MM/dd/yyyy") + " - " + term.EndDate.ToString("MM/dd/yyyy");

            var courseList = DBH.GetCourses(term);
            var courseNotificationList = DBH.GetCourseNotification(term);

            foreach (Courses _course in courseNotificationList)
            {
                if (_course.EndDate.Date == DateTime.Now.Date)
                {
                    CrossLocalNotifications.Current.Show(_course.Name, "Is Due Today!");
                }
            }

            CourseList.ItemsSource = courseList;

            if(courseList.Count() >= 6)
            {
                AddCourse.IsEnabled = false;
                AddCourse.IsVisible = false;
            }
            else
            {
                AddCourse.IsEnabled = true;
                AddCourse.IsVisible = true;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Header.Text = term.Title.ToString();
            Details.Text = term.StartDate.ToString("MM/dd/yyyy") + " - " + term.EndDate.ToString("MM/dd/yyyy");
            var courseList = DBH.GetCourses(term);
            CourseList.ItemsSource = courseList;
            if (courseList.Count() >= 6)
            {
                AddCourse.IsEnabled = false;
                AddCourse.IsVisible = false;
            }
            else
            {
                AddCourse.IsEnabled = true;
                AddCourse.IsVisible = true;
            }

        }

        private void EditTerm_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TermDetails(term)); 
        }

        private void AddCourse_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CourseAdd(term));
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(); 
        }
        private void CourseList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Courses course = (Courses)e.Item;
            Navigation.PushAsync(new CoursePage(course));
        }
    }
}