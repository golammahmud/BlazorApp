using ViewModel.App.ViewModels;

namespace BlazorApps.Services
{
    public interface IEmployesServices
    {
        Task<IEnumerable<EmployeeViewModel>> GetEmployees();
        Task<EmployeeViewModel> GetEmployee(int id);
    }
    public class EmployeesServices : IEmployesServices
    {
        private readonly HttpClient httpclient;

        public EmployeesServices(HttpClient _httpclient)
        {
            this.httpclient = _httpclient;
        }


        public async Task<IEnumerable<EmployeeViewModel>> GetEmployees()
        {
            try
            {
                return await httpclient.GetFromJsonAsync<EmployeeViewModel[]>("api/employee");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeViewModel> GetEmployee(int id)
        {
            try
            {
                return await httpclient.GetFromJsonAsync<EmployeeViewModel>($"api/employee/{id}");
            }
            catch
            {
                throw new Exception($"product {id} Not Found");
            }
        }


    }
}
