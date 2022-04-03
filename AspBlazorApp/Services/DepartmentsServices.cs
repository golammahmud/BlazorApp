using ViewModel.App.ViewModels;

namespace AspBlazorApp.Services
{   
    public interface IDepartmentServices
    {
        Task<IEnumerable<DepartmentsViewModel>> GetAllDepartments();

        Task<DepartmentsViewModel> GetDepartment(int Id);
    }


    public class DepartmentsServices : IDepartmentServices
    {

        private readonly HttpClient httpClient;

        public DepartmentsServices(HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
        }


        public async Task<IEnumerable<DepartmentsViewModel>> GetAllDepartments()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<DepartmentsViewModel[]>("api/department");
            }
            catch(BadHttpRequestException e)
            {
                throw e;
            }
        }

      public async Task<DepartmentsViewModel> GetDepartment(int Id)
        {
            try
            {
                var dept = await httpClient.GetFromJsonAsync<DepartmentsViewModel>($"api/department/{Id}");
                return dept;
            }
            catch(Exception ex)
            {
                throw new Exception($"Department {Id} Not Found");
            }
        }

    }
}
