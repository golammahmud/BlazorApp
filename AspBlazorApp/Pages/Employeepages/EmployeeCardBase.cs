using BlazorApps.Services;
using Microsoft.AspNetCore.Components;
using ViewModel.App.ViewModels;

namespace AspBlazorApp.Pages.Employeepages
{
    public class EmployeeCardBase : ComponentBase
    {


        [Parameter]
        public EmployeeViewModel Employee { get; set; }



        [Parameter]
        public bool ShowFooter { get; set; }


        [Parameter]
        public EventCallback<bool> OnEmployeeSelection { get; set; }


        [Parameter]
        public EventCallback<int> DeleteEmployee { get; set; }

        protected bool IsSelected { get; set; }


        [Parameter]
        public Enum Gender { get; set; }



        [Inject]
        public IEmployesServices EmployesService { get; set; }






        protected Custom_Components.ConfirmationModalBase DeleteConfirmation { get; set; }

        protected void Delete_Click()
        {
            DeleteConfirmation.Show();
        }

      
        protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await EmployesService.DeleteEmployee(Employee.EmployeeId);
                await DeleteEmployee.InvokeAsync(Employee.EmployeeId);
            }
        }



        //protected async Task Delete_employee()
        //{
        //    await EmployesService.DeleteEmployee(Employee.EmployeeId);
        //    await DeleteEmployee.InvokeAsync(Employee.EmployeeId);
        //}


        protected async Task CheckBoxChanged(ChangeEventArgs e)
        {
            IsSelected = (bool)e.Value;
            await OnEmployeeSelection.InvokeAsync(IsSelected);
           
        }

    }
}
