namespace BlazorApps.Pages
{
    public class EmployeeListBase: Microsoft.AspNetCore.Components.ComponentBase
    {
       // public IEnumerable<Employee> Employees { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Task.Run(LoadEmployees);
        }

        private void LoadEmployees()
        {
            System.Threading.Thread.Sleep(2000);
            // Retrieve data from the server and initialize
            // Employees property which the View will bind
        }
    }
}
