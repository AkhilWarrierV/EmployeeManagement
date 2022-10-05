using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Providers.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.UI.Controllers.InternalAPI
{
    [Route("api/internal/employee")]
    [ApiController]
    public class EmployeeInternalApiController : ControllerBase
    {
        private readonly IEmployeeApiClient _employeeApiClient;

        public EmployeeInternalApiController(IEmployeeApiClient employeeApiClient)
        {
            _employeeApiClient = employeeApiClient;
        }

        [HttpGet]
        [Route("{employeeId}")]
        public IActionResult GetEmployeeById([FromRoute] int employeeId)
        {
            try
            {
                var employee = _employeeApiClient.GetEmployeeById(employeeId);

                return Ok(employee);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpPost]
        [Route("insert-employee")]
        public IActionResult InsertEmployee([FromBody] EmployeeDetailedViewModel employeeDetailedViewModel)
        {
            try
            {
                var employee = _employeeApiClient.InsertEmployee(employeeDetailedViewModel);

                return Ok(employee);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpPut]
        [Route("update-employee")]
        public IActionResult UpdateEmployee([FromBody] EmployeeDetailedViewModel employeeDetailedViewModel)
        {
            try
            {
                var employee = _employeeApiClient.UpdateEmployee(employeeDetailedViewModel);

                return Ok(employee);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpDelete]
        [Route("deleteEmployees/{employeeId}")]
        public IActionResult DeleteEmployee(int employeeId)
        {
            try
            {
                _employeeApiClient.DeleteEmployee(employeeId);
                return Ok();
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
