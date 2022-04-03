using BlazorApps.Services;
using Microsoft.AspNetCore.Components;
using ViewModel.App.ViewModels;

namespace BlazorApps.Pages
{
    public class EmployeeDetailsBase : ComponentBase
    {


        public EmployeeViewModel Employee { get; set; } = new EmployeeViewModel();



        [Inject]
        public IEmployesServices EmployeeService { get; set; }


        [Parameter]
        public string Id { get; set; }

        
     
      
       


        protected override async Task OnInitializedAsync()
        {
            Id = Id ?? "1002";
            Employee =(await EmployeeService.GetEmployee(Convert.ToInt32(Id)));
        }


    }
}
