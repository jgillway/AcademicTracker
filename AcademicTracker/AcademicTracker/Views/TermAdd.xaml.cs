using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using AcademicTracker.Classes;
using AcademicTracker.Persistance;
using Xamarin.Forms.Xaml;

namespace AcademicTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermAdd : ContentPage
    {
        public TermAdd()
        {
            InitializeComponent(); 
        }

        private void SubmitAddTerm_Clicked(object sender, EventArgs e)
        {
            bool isValid = validateForm();
            if (isValid == true)
            {
                Terms term = new Terms()
                {
                    Title = TermName.Text,
                    StartDate = TermStartDate.Date,
                    EndDate = TermEndDate.Date
                };

                DBH.InsertTerm(term);

                Navigation.PopAsync();
            }
        }

        private void CancelAddTerm_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(); 
        }
        private bool validateForm()
        {
            bool validation = false;
            if (string.IsNullOrWhiteSpace(TermName.Text))
            {
                DisplayAlert("Invalid Entry", "The Term Name Must Be Valid", "OK");
                validation = false; 
            }else if(TermStartDate.Date > TermEndDate.Date)
            {
                DisplayAlert("Invalid Entry", "The Term End Date Cannot Be Before The Start Date", "OK");
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