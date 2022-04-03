using BlazorApps.Services;
using Microsoft.AspNetCore.Components;
using ViewModel.App.ViewModels;

namespace BlazorApps.Pages
{
    public class EmployeeListBase:ComponentBase
    {

        [Inject]
        public IEmployesServices employesServices { get; set; }

        protected  string cordinate { get; set; }


        public bool ShowFooter { get; set; } = true;

        protected string buttonText { get; set; } = "Hide Footer";

        protected string cssClass { get; set; } = null;

        public IEnumerable<EmployeeViewModel> Employees { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employees = (await employesServices.GetEmployees()).ToList();
            
        }
        protected int SelectedEmployeesCount { get; set; } = 0;


        protected void EmployeeSelectionChanged(bool isSelected)
        {
            if (isSelected)
            {
                SelectedEmployeesCount++;
            }
            else
            {
                SelectedEmployeesCount--;
            }
        }

       protected void button_click()
        {
            if(buttonText=="Hide Footer")
            {
                buttonText = "Show Footer";
                cssClass = "HideFooter";
            }
            else
            {
                buttonText = "Hide Footer";
                cssClass = null;
            }

        }

        //protected void mouse_move(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        //{
        //    cordinate = $"x={e.ClientX} y={e.ClientY}";
        //}

    }
}
