using AspBlazorApp.Services;
using BlazorApps.Services;
using Microsoft.AspNetCore.Components;
using ViewModel.App.ViewModels;

namespace AspBlazorApp.Pages.Employeepages
{
    public class AddEmployeeBase:ComponentBase
    {
        public EmployeeViewModel Employee { get; set; } = new EmployeeViewModel();


        [Inject]
        public IEmployesServices EmployeeService { get; set; }



        [Inject]
        public IDepartmentServices DepartmentService { get; set; }




        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string DepartmentId { get; set; }

        public List<DepartmentsViewModel> Departments { get; set; } = new List<DepartmentsViewModel>();

        //public DepartmentsViewModel Department { get; set; }

        public int EmpDepartmentId { get; set; }
        protected async Task HandleValidSubmit()
        {
            
            if (Employee != null)
            {
                Console.WriteLine();


                var result = await EmployeeService.CreateEmployee(Employee);
      
                if (result != null)
                {
                    NavigationManager.NavigateTo("/employeelist");
                }
            }
        }


        protected async override Task OnInitializedAsync()
        {
            Employee = Employee;
            Departments = (await DepartmentService.GetAllDepartments()).ToList();
         

        }



    }
}
