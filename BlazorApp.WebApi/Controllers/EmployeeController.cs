using Data.BlazoorApp.DataModels;
using Data.BlazoorApp.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ViewModel.App.ViewModels;
using Gender = ViewModel.App.ViewModels.Gender;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {


        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository _employeeRepository)
        {
            this.employeeRepository = _employeeRepository;
        }


        //search employee controller

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> Search(string? name, int? gender)
        {
            try
            {
                var result = await employeeRepository.Search(name, gender);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }




        // GET: api/<EmployeeController>
        [HttpGet]
        public IEnumerable<EmployeeViewModel> Get()
        {
            var model = employeeRepository.GetAllEmployees().Select(x => new EmployeeViewModel
            {
                EmployeeId = x.EmployeeId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                DateOfBirth = x.DateOfBirth,
                Gender = (Gender)x.Gender,
                DepartmentId = x.DepartmentId,
                Created_at = x.Created_at,
                Departments = new List<SelectListItem>() {
                    new SelectListItem(){
                        Text=x.Department?.Name,
                        Value=x.Department?.DepartmentId+""
                }}.ToList()

            });

            return model;

            // return new string[] { "value1", "value2" };
        }



        // GET api/<EmployeeController>/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployee(int id)
        {
            try
            {
                EmployeeViewModel model = new EmployeeViewModel();
                var result = await employeeRepository.GetEmployee(id);

                if (result == null) { return NotFound(); }
                {
                    model.EmployeeId = result.EmployeeId;
                    model.FirstName = result.FirstName;
                    model.LastName = result.LastName;
                    model.Email = result.Email;
                    model.DateOfBirth = result.DateOfBirth;
                    model.Gender = (Gender)result.Gender;
                    model.DepartmentId = result.DepartmentId;
                    model.Created_at = result.Created_at;
                    model.Departments = new List<SelectListItem>() {
                    new SelectListItem(){
                        Text=result.Department?.Name,
                        Value=result.Department?.DepartmentId+""
                }}.ToList();
                };


                return Ok(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<ActionResult<EmployeeViewModel>> CreateEmployee(EmployeeViewModel model)
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

                Employee employee = new Employee();

                {
                    employee.FirstName = model.FirstName;
                    employee.LastName = model.LastName;
                    employee.Email = model.Email;
                    employee.DateOfBirth =(DateTime) model.DateOfBirth;
                    employee.Gender = (int)(Gender)model.Gender;
                    employee.DepartmentId = (int)model.DepartmentId;
                    employee.Created_at = model.Created_at;
                }
                await employeeRepository.AddEmployee(employee);

                return Ok(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.EmployeeId)
                    return BadRequest("Employee ID mismatch");

                var employeeToUpdate = await employeeRepository.GetEmployee(id);

                if (employeeToUpdate == null)
                    return NotFound($"Employee with Id = {id} not found");

              await  employeeRepository.UpdateEmployee(new Employee
                {
                    EmployeeId = (int)employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Gender= employee.Gender,
                    DateOfBirth = employee.DateOfBirth,
                    DepartmentId = employee.DepartmentId,
                    Created_at = employee.Created_at,

                });

                return Ok(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // DELETE api/<EmployeeController>/5
       
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<EmployeeViewModel>> DeleteEmployee(int id)
        {
            try
            {
                var employeeToDelete = await employeeRepository.GetEmployee(id);

                if (employeeToDelete == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }
                var d= await employeeRepository.DeleteEmployee(id);
                return Ok(d);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
