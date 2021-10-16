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
    public partial class AssessmentDetails : ContentPage
    {
        private Assessments assessment;
        public AssessmentDetails(Assessments tempAssessment)
        {
            assessment = tempAssessment;

            InitializeComponent();

            AssessmentName.Text = assessment.Name;
            AssessmentStartDate.Date = assessment.StartDate;
            AssessmentEndDate.Date = assessment.EndDate;

            if (assessment.Type == "Performance")
            {
                AssessmentType.SelectedIndex = 0;
            }
            else
            {
                AssessmentType.SelectedIndex = 1;
            }

            if (assessment.Notifications == true)
            {
                AssessmentNotification.IsToggled = true;
            }
            else
            {
                AssessmentNotification.IsToggled = false;
            }
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            Navigation.PopAsync();
        }

        private void DeleteAssessment_Clicked(object sender, EventArgs e)
        {
            DBH.DeleteAssessment(assessment);

            Navigation.PopAsync(); 
        }

        private void SaveAssessment_Clicked(object sender, EventArgs e)
        {
            bool validate = validateForm();

            if (validate == true)
            {
                bool notificationYN;
                string type;

                if (AssessmentNotification.IsToggled)
                {
                    notificationYN = true;
                }
                else
                {
                    notificationYN = false;
                }

                if (AssessmentType.SelectedIndex == 0)
                {
                    type = "Performance";
                }
                else
                {
                    type = "Objective";
                }

                assessment.Name = AssessmentName.Text;
                assessment.Type = type;
                assessment.StartDate = AssessmentStartDate.Date;
                assessment.EndDate = AssessmentEndDate.Date;
                assessment.Notifications = notificationYN;


                DBH.UpdateAssessment(assessment);

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