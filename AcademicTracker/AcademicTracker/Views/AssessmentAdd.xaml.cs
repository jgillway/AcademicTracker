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

namespace AcademicTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentAdd : ContentPage
    {
        private Courses course;
        public AssessmentAdd(Courses tempCourse)
        {
            course = tempCourse;
            InitializeComponent();
            List<Assessments> assessmentList = DBH.GetAssessments(course); 
            if(assessmentList.Count() > 0)
            {
                string currentType = ""; 
                foreach(Assessments assessment in assessmentList)
                {
                    currentType = assessment.Type; 
                }
                if(currentType == "Performance")
                {
                    AssessmentType.SelectedIndex = 1;
                    AssessmentType.IsEnabled = false;
                }else if(currentType == "Objective")
                {
                    AssessmentType.SelectedIndex = 0;
                    AssessmentType.IsEnabled = false;
                }
            }
        }

        private void CancelAddAssessment_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(); 
        }

        private void SubmitAddAssessment_Clicked(object sender, EventArgs e)
        {
            bool validate = validateForm();

            if (validate == true)
            {
                bool notificationYN;
                if (AssessmentNotification.IsToggled)
                {
                    notificationYN = true;
                }
                else
                {
                    notificationYN = false;
                }

                Assessments assessment = new Assessments()
                {
                    CourseId = course.Id,
                    Name = AssessmentName.Text,
                    Type = AssessmentType.SelectedItem.ToString(),
                    StartDate = AssessmentStartDate.Date,
                    EndDate = AssessmentEndDate.Date,
                    Notifications = notificationYN
                };

                DBH.InsertAssessment(assessment);

                Navigation.PopAsync();
            }
        }
        private bool validateForm()
        {
            bool validation = false;
            if (string.IsNullOrWhiteSpace(AssessmentName.Text))
            {
                DisplayAlert("Invalid Entry", "The Assessment Name Must Be Valid", "OK");
                validation = false;
            }
            else if (AssessmentStartDate.Date > AssessmentEndDate.Date)
            {
                DisplayAlert("Invalid Entry", "The Assessment End Date Cannot Be Before The Start Date", "OK");
                validation = false;
            }
            else if (AssessmentType.SelectedItem == null)
            {
                DisplayAlert("Invalid Entry", "Assessment Type Is Required", "OK");
                validation = false;
            }
            else
            {
                validation = true;
            }
            return validation;
        }
    }
}