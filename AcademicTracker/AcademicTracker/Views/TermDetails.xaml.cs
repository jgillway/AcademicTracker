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
    public partial class TermDetails : ContentPage
    {
        private Terms term;
        public TermDetails(Terms tempTerm)
        {
            term = tempTerm; 

            InitializeComponent();

            TermName.Text = term.Title;
            TermStartDate.Date = term.StartDate;
            TermEndDate.Date = term.EndDate;
        }



        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(); 
        }

        private void DeleteTerm_Clicked(object sender, EventArgs e)
        {
            DBH.DeleteTerm(term);

            Navigation.PopAsync();
            Navigation.PopAsync();
        }

        private void SaveTerm_Clicked(object sender, EventArgs e)
        {
            bool isValid = validateForm();
            if (isValid == true)
            {
                term.Title = TermName.Text;
                term.StartDate = TermStartDate.Date;
                term.EndDate = TermEndDate.Date;

                DBH.UpdateTerm(term);

                Navigation.PopAsync();
            }
        }

        private bool validateForm()
        {
            bool validation = false;
            if (string.IsNullOrWhiteSpace(TermName.Text))
            {
                DisplayAlert("Invalid Entry", "The Term Name Must Be Valid", "OK");
                validation = false;
            }
            else if (TermStartDate.Date > TermEndDate.Date)
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