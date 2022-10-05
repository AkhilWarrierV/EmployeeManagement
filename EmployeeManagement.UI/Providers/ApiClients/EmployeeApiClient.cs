using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Models.Provider;
using EmployeeManagement.UI.Providers.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EmployeeManagement.UI.Providers.ApiClients
{
    public class EmployeeApiClient : IEmployeeApiClient
    {
        private readonly HttpClient _httpClient;

        public EmployeeApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IEnumerable<EmployeeViewModel> GetAllEmployee()
        {
            using (var response = _httpClient.GetAsync("https://localhost:5001/api/employee/get-all").Result)//Consume /employee endpoint in the EmployeeManagementApi using _httpClient
            {
                var employee = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(response.Content.ReadAsStringAsync().Result);
                return employee;
            }
        }

        public EmployeeDetailedViewModel GetEmployeeById(int id)
        {
            using (var response = _httpClient.GetAsync("https://localhost:5001/api/employee/"+id).Result)//Consume /employee endpoint in the EmployeeManagementApi using _httpClient
            {
                var employee = JsonConvert.DeserializeObject<EmployeeDetailedViewModel>(response.Content.ReadAsStringAsync().Result);
                return employee;
            }
           
        }

        public bool InsertEmployee(EmployeeDetailedViewModel employeeDetailedViewModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(employeeDetailedViewModel),Encoding.UTF8,"application/json");


            using (var response = _httpClient.PostAsync("https://localhost:5001/api/employee/insert-employee", stringContent).Result)//Consume /employee endpoint in the EmployeeManagementApi using _httpClient
            {
                response.Content.ReadAsStringAsync();
                return true;
            }

        }

        public bool UpdateEmployee(EmployeeDetailedViewModel employeeDetailedViewModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(employeeDetailedViewModel), Encoding.UTF8, "application/json");


            using (var response = _httpClient.PutAsync("https://localhost:5001/api/employee/update-employee", stringContent).Result)//Consume /employee endpoint in the EmployeeManagementApi using _httpClient
            {
                response.Content.ReadAsStringAsync();
                return true;
            }

        }

        public bool DeleteEmployee(int id)
        {
            using (var response = _httpClient.DeleteAsync("https://localhost:5001/api/employee/deleteEmployees/" + id).Result)//Consume /employee endpoint in the EmployeeManagementApi using _httpClient
            {
                response.Content.ReadAsStringAsync();
                return true;
            }

        }

    }
}
