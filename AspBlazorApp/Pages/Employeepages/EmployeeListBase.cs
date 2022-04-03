using BlazorApps.Services;
using Microsoft.AspNetCore.Components;
using ViewModel.App.ViewModels;

namespace AspBlazorApp.Pages.Employeepages
{
    public class EmployeeListBase:ComponentBase
    {

        [Inject]
        public IEmployesServices employesService { get; set; }

        public IEnumerable<EmployeeViewModel> Employees { get; set; }

        public Enum? Gender { get; set; }
        public bool ShowFooter { get; set; } = true;


  

        public int CountSelection { get; set; } = 0;

        protected  void EmployeeSelectionChanged(bool isSelected)
        {
            if (isSelected)
            {
                CountSelection++;
            }
            else
            {
                CountSelection--;
            }
        }



        protected async override Task OnInitializedAsync()
        {
            Employees = (await employesService.GetEmployees()).ToList();
        }



        protected async Task DeleteEmployee()
        {
            Employees = (await employesService.GetEmployees()).ToList();
        }
    }
}
