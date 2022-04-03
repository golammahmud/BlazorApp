using Data.BlazoorApp.DataModels;
using Data.BlazoorApp.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ViewModel.App.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IDepartmentsRepository departmentsRepository;

        public DepartmentController (IDepartmentsRepository _departmentsRepository)
        {
            this.departmentsRepository = _departmentsRepository;

        }

        // GET: api/<DepartmentController>
        [HttpGet]
        public IEnumerable<DepartmentsViewModel> Get()
        {
            try
            {
                var departments = departmentsRepository.GetDepartments().Select(s=> new DepartmentsViewModel
                {
                    DepartmentId=s.DepartmentId,
                    Name=s.Name,
                   Created_at=(DateTime)s.Created_at,
                
                    

                });

                return departments;


            }
            catch(Exception e)
            {
                throw e; 

                //return (IEnumerable<DepartmentsViewModel>)StatusCode(StatusCodes.Status500InternalServerError,
                //   "Error retrieving data from the database");
            }
        }

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentsViewModel>> Get(int id)
        {
            try
            {
                DepartmentsViewModel department = new DepartmentsViewModel();
                var dp = await departmentsRepository.GetDepartment(id);
                if (dp != null)
                {
                    {
                        department.DepartmentId = dp.DepartmentId;
                        department.Name = dp.Name;
                        department.Created_at = (DateTime)dp.Created_at;
                        
                    };
                    return Ok(department);
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            

            
        }

        // POST api/<DepartmentController>
        [HttpPost]
        public async Task<ActionResult<DepartmentsViewModel>> CreateEmployee(DepartmentsViewModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();


                // Add custom model validation error
                //Employee emp = await employeeRepository.GetEmployeeByEmail(model.Email);

                //if (emp != null)
                //{
                //    ModelState.AddModelError("email", "Employee email already in use");
                //    return BadRequest(ModelState);
                //}

                Department dept = new Department();

                {
                    dept.DepartmentId = model.DepartmentId;
                    dept.Name = model.Name;
                    dept.Created_at = model.Created_at;
                   
                }
                await departmentsRepository.AddDepartment(dept);

                return Ok(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }


        // PUT api/<DepartmentController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DepartmentsViewModel>> Put(int id, [FromBody] DepartmentsViewModel model)
        {
            try
            {
                if (id != model.DepartmentId)
                {
                    return BadRequest();
                }

                var getDepartment = await departmentsRepository.GetDepartment(id);

                if (getDepartment == null)
                {
                    return NotFound($"Department with Id = {id} not found");
                }


                await departmentsRepository.UpdateDepartment(new Department
                {
                    DepartmentId = model.DepartmentId,
                    Name = model.Name,
                    Created_at = (DateTime)model.Created_at,

                });

                return Ok(model);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error updating new department record");
            }
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                var deleteDepartment = departmentsRepository.GetDepartment(id);
                if (deleteDepartment != null)
                {
                    departmentsRepository.DeleteDepartment(id);
                }
                 NotFound($"Employee with Id = {id} not found");
            }
            catch(Exception e)
            {
                StatusCode(StatusCodes.Status500InternalServerError,
                   "Error updating new department record");
            }

        }
    }
}
