using Microsoft.AspNetCore.Components;
using ViewModel.App.ViewModels;

namespace AspBlazorApp.Pages.Employeepages.EmployeeComponents
{
    public class EmployeeCardBase:ComponentBase
    {
        [Parameter]
        public EmployeeViewModel Employee { get; set; }



        [Parameter]
        public bool ShowFooter { get; set; }
    }
}
