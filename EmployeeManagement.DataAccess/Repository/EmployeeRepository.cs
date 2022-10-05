using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.DataAccess.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private SqlConnection _sqlConnection;
        public EmployeeRepository()
        {
            _sqlConnection = new SqlConnection("data source=(localdb)\\mssqllocaldb;database=EmployeeManagement");

        }

        public EmployeeData GetEmployeeById(int id)
        {
            try
            {
                _sqlConnection.Open();
                var sqlCommand = new SqlCommand(cmdText: "select * from Employee where ID=@Id", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", id);
                var sqlReader = sqlCommand.ExecuteReader();
                var employee = new EmployeeData();
                while (sqlReader.Read())
                {
                    employee.Id = (int)sqlReader["ID"];
                    employee.Name = (string)sqlReader["Name"];
                    employee.Department = (String)sqlReader["DEPARTMENT"];
                    employee.Age = (int)sqlReader["AGE"];
                    employee.Address = (String)sqlReader["ADDRESS"];
                    

                }
                return employee;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public IEnumerable<EmployeeData> GetEmployees()
        {
            try
            {
                _sqlConnection.Open();
                var sqlCommand = new SqlCommand(cmdText: "select ID,NAME,DEPARTMENT from Employee", _sqlConnection);
                var sqlReader = sqlCommand.ExecuteReader();
                var listOfStudent = new List<EmployeeData>();

                while (sqlReader.Read())
                {
                    listOfStudent.Add(new EmployeeData
                    {
                        Id = (int)sqlReader["ID"],
                        Name = (string)sqlReader["NAME"],
                        Department = (string)sqlReader["DEPARTMENT"]
                    });
                }
                return listOfStudent;

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                _sqlConnection.Close();
            }

        }
        public bool InsertData(EmployeeData employees)
        {
            try
            {
                _sqlConnection.Open();
                var sqlCommand = new SqlCommand(cmdText: "insert into Employee(NAME,AGE,DEPARTMENT,ADDRESS)values(@Name,@Age,@Department,@Address)", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("Name", employees.Name);
                sqlCommand.Parameters.AddWithValue("Age", employees.Age);
                sqlCommand.Parameters.AddWithValue("Department", employees.Department);
                sqlCommand.Parameters.AddWithValue("Address", employees.Address);
                sqlCommand.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public bool UpdateData(EmployeeData employees)
        {
            try
            {
                _sqlConnection.Open();
                var sqlCommand = new SqlCommand(cmdText: "update Employee set NAME=@Name,AGE=@Age,DEPARTMENT=@Department,ADDRESS=@Address where ID=@Id", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", employees.Id);
                sqlCommand.Parameters.AddWithValue("Name", employees.Name);
                sqlCommand.Parameters.AddWithValue("Age", employees.Age);
                sqlCommand.Parameters.AddWithValue("Department", employees.Department);
                sqlCommand.Parameters.AddWithValue("Address", employees.Address);
                sqlCommand.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public bool DeleteData(int id)
        {
            try
            {
                _sqlConnection.Open();
                var sqlCommand = new SqlCommand(cmdText: "delete from Employee where ID=@id", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", id);
                sqlCommand.ExecuteNonQuery();
                return true;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }


    }
}
