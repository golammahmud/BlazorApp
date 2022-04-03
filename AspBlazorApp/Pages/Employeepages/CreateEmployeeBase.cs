using AspBlazorApp.Services;
using BlazorApps.Services;
using Microsoft.AspNetCore.Components;
using ViewModel.App.ViewModels;

namespace AspBlazorApp.Pages.Employeepages
{
    public class CreateEmployeeBase:ComponentBase
    {
        public EmployeeViewModel Employee { get; set; } = new EmployeeViewModel();

        [Inject]
        public IEmployesServices EmployeeService { get; set; }



        [Inject]
        public IDepartmentServices DepartmentService { get; set; }




        [Inject]
        public NavigationManager NavigationManager { get; set; }



        public List<DepartmentsViewModel> Departments { get; set; } = new List<DepartmentsViewModel>();



        protected async Task HandleValidSubmit()
        {
            if (Employee != null)
            {
                var result = await EmployeeService.CreateEmployee(Employee);
                if (result != null)
                {
                    NavigationManager.NavigateTo("/employeelist", true);
                }
            }
        }

        protected async override Task OnInitializedAsync()
        {
            Employee = await EmployeeService.CreateEmployee(Employee);

            Departments = (await DepartmentService.GetAllDepartments()).ToList();


        }




    }
}
