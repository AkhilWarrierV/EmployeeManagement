using EmployeeManagement.API.Models;
using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeApiController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        [HttpGet]
        [Route("{employeeId}")]
        public IActionResult GetEmployeeById([FromRoute] int employeeId)
        {
            try
            {
                var getEmployeeId = _employeeService.GetEmployeeById(employeeId);
                return Ok(MapToEmployeebyId(getEmployeeId));
                
                }
            catch (Exception)
            {
                throw;
            }
        }
        private EmployeeDetailedViewModel MapToEmployeebyId(EmployeeDto employee)
        {
            var employeeById = new EmployeeDetailedViewModel()
            {
                Id=employee.Id,
                Name=employee.Name,
                Department=employee.Department,
                Age=employee.Age,
                Address=employee.Address
            };
            return employeeById;
        }
        [HttpGet]
        [Route("get-all")]
        public IEnumerable<EmployeeDetailedViewModel> GetEmployees()
        {
            var getEmployees = _employeeService.GetEmployees();
            return MapToEmployeeeDetailedViewmodel(getEmployees);
        }
        private IEnumerable<EmployeeDetailedViewModel> MapToEmployeeeDetailedViewmodel(IEnumerable<EmployeeDto> getEmployees)
        {
            var DetailedModel = new List<EmployeeDetailedViewModel>();
            foreach (var employeeDetail in getEmployees)
            {
                var employeesDto = new EmployeeDetailedViewModel()
                {
                    Id = employeeDetail.Id,
                    Name = employeeDetail.Name,
                    Department = employeeDetail.Department,
                    Age=employeeDetail.Age,
                    Address=employeeDetail.Address

                };
                DetailedModel.Add(employeesDto);
            }
                return DetailedModel;
        }

        [HttpPost]
        [Route("insert-employee")]
        public IActionResult InsertEmployee([FromBody] EmployeeDetailedViewModel insert)
        {
            try
            {
                var insertData = _employeeService.InsertEmployee(MapToInsertDto(insert));
                return Ok(insertData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        private EmployeeDto MapToInsertDto(EmployeeDetailedViewModel employeeInsertion)
        {
            var employeeInsert = new EmployeeDto()
            {
                Name=employeeInsertion.Name,
                Department=employeeInsertion.Department,
                Age=employeeInsertion.Age,
                Address=employeeInsertion.Address
               
            };
            return employeeInsert;
        }

        [HttpPut]
        [Route("update-employee")]
        public IActionResult UpdateEmployee([FromBody] EmployeeDetailedViewModel update)
        {
            try
            {
                var updateData = _employeeService.UpdateEmployee(MapToUpdatetDto(update));
                return Ok(updateData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        private EmployeeDto MapToUpdatetDto(EmployeeDetailedViewModel employeeUpdation)
        {
            var employeeUpdate = new EmployeeDto()
            {
                Id = employeeUpdation.Id,
                Name = employeeUpdation.Name,
                Department = employeeUpdation.Department,
                Age = employeeUpdation.Age,
                Address = employeeUpdation.Address

            };
            return employeeUpdate;
        }
        [HttpDelete]
        [Route("deleteEmployees/{employeeId}")]
        public IActionResult DeleteEmployee(int employeeId)
        {
            try
            {
                _employeeService.DeleteEmployee(employeeId);
                return Ok();
            }
           
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
