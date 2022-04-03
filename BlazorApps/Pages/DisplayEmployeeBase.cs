using Microsoft.AspNetCore.Components;
using ViewModel.App.ViewModels;

namespace BlazorApps.Pages
{
    public class DisplayEmployeeBase:ComponentBase
    {

        [Parameter]
        public EmployeeViewModel Employee { get; set; }



        [Parameter]
        public bool ShowFooter { get; set; }


        protected bool IsSelected { get; set; }

        [Parameter]
        public EventCallback<bool> OnEmployeeSelection { get; set; }
        protected  async   Task CheckBoxChanged(ChangeEventArgs e)
        {
            IsSelected = (bool)e.Value;

            await OnEmployeeSelection.InvokeAsync(IsSelected);
            //await OnEmployeeSelection.InvokeAsync((bool)e.Value);
        }
    }
}
