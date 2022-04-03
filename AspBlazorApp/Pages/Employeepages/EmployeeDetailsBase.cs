using BlazorApps.Services;
using Microsoft.AspNetCore.Components;
using ViewModel.App.ViewModels;

namespace AspBlazorApp.Pages.Employeepages
{
    public class EmployeeDetailsBase:ComponentBase
    {
        [Parameter]
        public string Id { get; set; }



        [Inject]
        public IEmployesServices EmployesService { get; set; }

        public Enum Gender { get; set; }
        protected EmployeeViewModel Employee { get; set; }


        protected string Department { get; set; }
        protected async override Task OnInitializedAsync()
        {
            Employee = await EmployesService.GetEmployee(int.Parse(Id));
            Gender = Employee.Gender;
            Department = Employee.Department.Name;
        }
    }
}
