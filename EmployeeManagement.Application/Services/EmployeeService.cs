using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public EmployeeDto GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            return MapToEmployeeDatatoDto(employee);
        }

        private EmployeeDto MapToEmployeeDatatoDto(EmployeeData employee)
        {
            
            var employeeDto= new EmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Department=employee.Department,
                Age=employee.Age,
                Address=employee.Address
            };
            return employeeDto;
        }

        public IEnumerable<EmployeeDto> GetEmployees()
        {
            var employee = _employeeRepository.GetEmployees();
            return MapToGetEmployeeDto(employee);
        }
    
        private IEnumerable<EmployeeDto> MapToGetEmployeeDto(IEnumerable<EmployeeData> employee)
        {
            var employeeDtos = new List<EmployeeDto>();
            foreach(var employeeDto in employee)
            {
                var employeesDto = new EmployeeDto()
                {
                    Id=employeeDto.Id,
                    Name=employeeDto.Name,
                    Department=employeeDto.Department

                };
                employeeDtos.Add(employeesDto);
            }
            return employeeDtos;
        }

        public bool InsertEmployee(EmployeeDto insertion )
        {
            var employeeInsert = _employeeRepository.InsertData(MapToInsertDto(insertion));
            return employeeInsert;
        }
        private EmployeeData MapToInsertDto(EmployeeDto employee)
        {
            var insertData = new EmployeeData()
            {
                Name=employee.Name,
                Department=employee.Department,
                Age=employee.Age,
                Address=employee.Address
            };
            return insertData;
        }

        public bool UpdateEmployee(EmployeeDto employee)
        {
            var employeeUpdate = _employeeRepository.UpdateData(MapToUpdateDto(employee));
            return employeeUpdate;
        }
        private EmployeeData MapToUpdateDto(EmployeeDto employee)
        {
            var updateData = new EmployeeData()
            {
                Id=employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Age = employee.Age,
                Address = employee.Address
            };
            return updateData;
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                _employeeRepository.DeleteData(id);
                return true;
            }
           
            catch (Exception )
            {
                throw;
               
            }

        }
                
    }

      
}

