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
using Xamarin.Essentials; 

namespace AcademicTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseDetails : ContentPage
    {
        private Courses course;
        public CourseDetails(Courses tempCourse)
        {
            course = tempCourse; 
            InitializeComponent();
            CourseName.Text = course.Name;
            CourseStartDate.Date = course.StartDate;
            CourseEndDate.Date = course.EndDate;
            CourseStatus.SelectedItem = course.Status;
            CourseInstructorName.Text = course.InstructorName;
            CourseInstructorPhone.Text = course.InstructorPhone;
            CourseInstructorEmail.Text = course.InstructorEmail;
            CourseNotes.Text = course.Notes;

            if (course.Status == "Active")
            {
                CourseStatus.SelectedIndex = 0;
            }
            else
            {
                CourseStatus.SelectedIndex = 1;
            }

            if (course.Notifications == true)
            {
                CourseNotification.IsToggled = true;
            }
            else
            {
                CourseNotification.IsToggled = false;
            }


        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(); 
        }

        private void DeleteCourse_Clicked(object sender, EventArgs e)
        {
            DBH.DeleteCourse(course);

            Navigation.PopAsync();
            Navigation.PopAsync();
        }

        private void SaveCourse_Clicked(object sender, EventArgs e)
        {

            bool validate = validateForm();

            if (validate == true)
            {
                bool notificationsYN;
                string status;

                if (CourseNotification.IsToggled)
                {
                    notificationsYN = true;
                }
                else
                {
                    notificationsYN = false;
                }

                if (CourseStatus.SelectedIndex == 0)
                {
                    status = "Active";
                }
                else
                {
                    status = "Inactive";
                }

                course.Name = CourseName.Text;
                course.StartDate = CourseStartDate.Date;
                course.EndDate = CourseEndDate.Date;
                course.Status = status;
                course.InstructorName = CourseInstructorName.Text;
                course.InstructorPhone = CourseInstructorPhone.Text;
                course.InstructorEmail = CourseInstructorEmail.Text;
                course.Notes = CourseNotes.Text;
                course.Notifications = notificationsYN;

                DBH.UpdateCourse(course);

                Navigation.PopAsync();
            }
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

        private void ShareNotes_Clicked(object sender, EventArgs e)
        {
            Share.RequestAsync(new ShareTextRequest { Title = "Share your notes about " + course.Name, Text = CourseNotes.Text });
        }
    }
}