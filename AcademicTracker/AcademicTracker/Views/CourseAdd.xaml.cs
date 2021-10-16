using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AcademicTracker.Classes;
using AcademicTracker.Persistance;
using AcademicTracker.Views;
using Plugin.LocalNotifications;

namespace AcademicTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class CourseAdd : ContentPage
    {
        private Terms term;
        public CourseAdd(Terms tempTerm)
        {
            term = tempTerm;
            var termId = term.Id; 
            InitializeComponent();
           
        }

        private void SubmitAddCourse_Clicked(object sender, EventArgs e)
        {
            bool validate = validateForm();

            if (validate == true)
            {
                bool notificationsYN;
                if (CourseNotification.IsToggled)
                {
                    notificationsYN = true;
                }
                else
                {
                    notificationsYN = false;
                }
                Courses course = new Courses()
                {
                    TermId = term.Id,
                    Name = CourseName.Text,
                    StartDate = CourseStartDate.Date,
                    EndDate = CourseEndDate.Date,
                    Status = CourseStatus.SelectedItem.ToString(),
                    InstructorName = CourseInstructorName.Text,
                    InstructorPhone = CourseInstructorPhone.Text,
                    InstructorEmail = CourseInstructorEmail.Text,
                    Notes = CourseNotes.Text,
                    Notifications = notificationsYN
                };
                DBH.InsertCourse(course);
                
                Navigation.PopAsync();
            }
        }

        private void CancelAddCourse_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(); 
        }

        public bool validateForm()
        {
            bool validation = false;

            if (string.IsNullOrWhiteSpace(CourseName.Text))
            {
                DisplayAlert("Invalid Entry", "The Course Name Must Be Valid", "OK");
                validation = false;
            }
            else if (CourseStartDate.Date > CourseEndDate.Date)
            {
                DisplayAlert("Invalid Entry", "The Course End Date Cannot Be Before The Start Date", "OK");
                validation = false;
            }
            else if (CourseStatus.SelectedItem == null)
            {
                DisplayAlert("Invalid Entry", "Course Status Is Required", "OK");
                validation = false;
            }
            else if (string.IsNullOrWhiteSpace(CourseInstructorName.Text))
            {
                DisplayAlert("Invalid Entry", "The Instructors Name Must Be Valid", "OK");
                validation = false;
            }
            else if (string.IsNullOrWhiteSpace(CourseInstructorPhone.Text))
            {
                DisplayAlert("Invalid Entry", "The Instructors Phone Must Be Valid", "OK");
                validation = false;
            }
            else if (string.IsNullOrWhiteSpace(CourseInstructorEmail.Text))
            {
                DisplayAlert("Invalid Entry", "The Instructors Email Must Be Valid", "OK");
                validation = false;
            }
            else if (IsValidEmail(CourseInstructorEmail.Text) == false)
            {
                DisplayAlert("Invalid Entry", "The Instructors Email Is Not a Valid Email", "OK");
                validation = false;
            }
            else
            {
                validation = true; 
            }
            return validation;
        }
        private bool IsValidEmail(string checkEmail)
        {
            try
            {
                MailAddress m = new MailAddress(checkEmail);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}