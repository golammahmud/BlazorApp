using BlazorApps.Services;
using Microsoft.AspNetCore.Components;
using ViewModel.App.ViewModels;

namespace BlazorApps.Pages
{
    public class CreateEmployeeBase :ComponentBase
    {
        public EmployeeViewModel Employee { get; set; } = new EmployeeViewModel();

        [Inject]
        public IEmployesServices EmployeeService { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Employee = await EmployeeService.GetEmployee(int.Parse(Id));
        }
    }
}
