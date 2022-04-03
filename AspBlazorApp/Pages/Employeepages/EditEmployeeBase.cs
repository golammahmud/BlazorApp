using AspBlazorApp.Services;
using AutoMapper;
using BlazorApps.Services;
using Microsoft.AspNetCore.Components;
using ViewModel.App.ViewModels;

namespace AspBlazorApp.Pages.Employeepages
{
    public class EditEmployeeBase:ComponentBase
    {

        public EmployeeViewModel Employee { get; set; } = new EmployeeViewModel();

        [Inject]
        public IEmployesServices EmployeeService { get; set; }



        [Inject]
        public IDepartmentServices DepartmentService { get; set; }

        public List<DepartmentsViewModel> Departments { get; set; } = new List<DepartmentsViewModel>();

        public string DepartmentId { get; set; }


        [Inject]
        public NavigationManager NavigationManager { get; set; }


        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

       

        protected async Task HandleValidSubmit()
        {

          var result=  await EmployeeService.UpdateEmployee(Employee);
            Console.WriteLine("Result" + result + "!");
            if (result != null)
            {
                NavigationManager.NavigateTo("/" , true);
            }
        

        }

        protected async override Task OnInitializedAsync()
        {
            Employee = await EmployeeService.GetEmployee(int.Parse(Id));
            Departments = (await DepartmentService.GetAllDepartments()).ToList();
            //DepartmentId = Employee.DepartmentId.ToString();
        }



        protected async Task Delete_Click()
        {
            await EmployeeService.DeleteEmployee(Employee.EmployeeId);

            NavigationManager.NavigateTo("/employeelist", true);
        }
    }
}
