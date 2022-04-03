using ViewModel.App.ViewModels;

namespace BlazorApps.Services
{
    public interface IEmployesServices
    {
        Task<IEnumerable<EmployeeViewModel>> GetEmployees();
        Task<EmployeeViewModel> GetEmployee(int id);

        Task<EmployeeViewModel> CreateEmployee(EmployeeViewModel updatedEmployee);

        Task<EmployeeViewModel> UpdateEmployee(EmployeeViewModel updatedEmployee);

        Task DeleteEmployee(int id);
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


        public async Task<EmployeeViewModel> CreateEmployee(EmployeeViewModel AddEmployee)
        {
            try
            {
                var response = await httpclient.PostAsJsonAsync("api/Employee", AddEmployee);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<EmployeeViewModel>();
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                throw new Exception($"Employee {AddEmployee.FirstName} Not created !");
            }
        }


        public async Task<EmployeeViewModel> UpdateEmployee(EmployeeViewModel updatedEmployee)
        {
            try
            {
                var response = await httpclient.PutAsJsonAsync($"api/Employee/{updatedEmployee.EmployeeId}" ,updatedEmployee);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<EmployeeViewModel>();

                    // return JsonConvert.DeserializeObject<AppsViewModel>(response.Content.ReadAsStringAsync().Result);

                    //return await result.Content.ReadFromJsonAsync<UserToken>();
                }
                else
                {
                    return null;
                }


            }
            catch
            {
                throw new Exception($"Employee {updatedEmployee.EmployeeId} Not Found");
            }
        }

        public async Task DeleteEmployee(int id)
        {
            await httpclient.DeleteAsync($"api/employee/{id}");
        }


    }
}
