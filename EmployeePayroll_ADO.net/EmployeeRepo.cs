﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayroll_ADO.net
{
    public class EmployeeRepo
    {
        public static string connectionString = @"Data Source=(localdb)\DESKTOP-PRGQ24S\SQLEXPRESS;
                                                Initial Catalog=payroll_service;
                                                Integrated Security=True;
                                                Connect Timeout=30;
                                                Encrypt=False;
                                                TrustServerCertificate=False;
                                                ApplicationIntent=ReadWrite;
                                                MultiSubnetFailover=False";
        SqlConnection connection = new SqlConnection(connectionString);

        public bool EstablishConnection()
        {
            try
            {
                DateTime now = DateTime.Now; //create object DateTime class //DateTime.Now class access system date and time 
                connection.Open(); // open connection
                using (connection)  //using SqlConnection
                {
                    Console.WriteLine($"Connection is created Successful on {now}"); //print msg

                }
                connection.Close(); //close connection
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }

        public int GetAllRecords()
        {
            try
            {
                int count = 0;
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"select * from Employee_Payroll";

                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            count++;
                            employeeModel.EmployeeID = dr.GetInt32(0);
                            employeeModel.EmployeeName = dr.GetString(1);
                            employeeModel.BasicPay = Convert.ToDouble(dr.GetDecimal(2));
                            employeeModel.start_date = dr.GetDateTime(3);
                            //employeeModel.Gender = Convert.ToChar(dr.GetString(4));
                            employeeModel.PhoneNumber = dr.GetString(5);
                            employeeModel.Address = dr.GetString(6);
                            employeeModel.Department = dr.GetString(7);
                            employeeModel.Deduction = dr.GetDouble(8);
                            employeeModel.TaxablePay = (float)dr.GetSqlSingle(9);
                            //employeeModel.NetPay = (float)dr.GetSqlSingle(10);
                            employeeModel.IncomeTax = (float)dr.GetSqlSingle(11);

                            Console.WriteLine(employeeModel.EmployeeID + " , " + employeeModel.EmployeeName + " , " + employeeModel.Address + " , " + employeeModel.Gender + " , " + employeeModel.Department + " , " + employeeModel.NetPay + " , " + employeeModel.start_date + " , " + employeeModel.PhoneNumber
                                                + " , " + employeeModel.BasicPay + " , " + employeeModel.Address + " , " + employeeModel.Deduction + " , " + employeeModel.TaxablePay + " , " + employeeModel.IncomeTax);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Table is Empty....");
                    }
                    dr.Close();
                    this.connection.Close();
                }
                return count;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public double UpdateEmployee()
        {
            EmployeeModel employeeModel = new EmployeeModel();
            try
            {
                using (this.connection)
                {
                    string query = @"update employee_payroll set basic_pay=3000000 where name='Billi';";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        query = @"Select basic_pay from employee_payroll where name='Billi';";
                        cmd = new SqlCommand(query, connection);
                        object res = cmd.ExecuteScalar();
                        connection.Close();
                        employeeModel.BasicPay = (double)(decimal)res;
                        return (employeeModel.BasicPay);
                    }
                    else
                    {
                        connection.Close();
                        return 0;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }


        public int getEmployeeDataWithGivenRange()
        {
            try
            {
                int count = 0;
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"select count(name) from employee_payroll where start between cast('2015-01-01' as date) and CAST('2019-12-12' as date)";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            count = sqlDataReader.GetInt32(0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlDataReader.Close();
                    this.connection.Close();
                    return count;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        public int getAggrigateSumSalary()
        {
            try
            {
                int sum = 0;
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"select sum(basic_pay) from  employee_payroll GROUP BY gender;";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            sum = (int)Convert.ToDouble(sqlDataReader.GetDecimal(0));

                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlDataReader.Close();
                    this.connection.Close();
                    return sum;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int getAvragSalary()
        {
            try
            {
                int avg = 0;
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"select avg(basic_pay) from  employee_payroll GROUP BY gender;";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            avg = (int)Convert.ToDouble(sqlDataReader.GetDecimal(0));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlDataReader.Close();
                    this.connection.Close();
                    return avg;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int getMinSalary()
        {
            try
            {
                int min = 0;
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"select min(basic_pay) from  employee_payroll GROUP BY gender;";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            min = (int)Convert.ToDouble(sqlDataReader.GetDecimal(0));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlDataReader.Close();
                    this.connection.Close();
                    return min;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int getMaxSalary()
        {
            try
            {
                int max = 0;
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"select max(basic_pay) from  employee_payroll GROUP BY gender;";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            max = (int)Convert.ToDouble(sqlDataReader.GetDecimal(0));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlDataReader.Close();
                    this.connection.Close();
                    return max;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Gets the maximum salary.
        public int getCountSalary()
        {
            try
            {
                int count = 0;
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"select count(basic_pay) from  employee_payroll GROUP BY gender";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            count = sqlDataReader.GetInt32(0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlDataReader.Close();
                    this.connection.Close();
                    return count;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool InsertEmployee(EmployeeModel empModel)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("payrollProcedure", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.AddWithValue("@EmployeeID", empModel.EmployeeID);
                    command.Parameters.AddWithValue("@EmployeeName", empModel.EmployeeName);
                    command.Parameters.AddWithValue("@BasicPay", empModel.BasicPay);
                    command.Parameters.AddWithValue("@StartDate", empModel.start_date);
                    command.Parameters.AddWithValue("@Gender", empModel.Gender);
                    command.Parameters.AddWithValue("@PhoneNumber", empModel.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", empModel.Address);
                    command.Parameters.AddWithValue("@Department", empModel.Department);
                    command.Parameters.AddWithValue("@Deductions", empModel.Deduction);
                    command.Parameters.AddWithValue("@TaxablePay", empModel.TaxablePay);
                    command.Parameters.AddWithValue("@NetPay", empModel.NetPay);
                    command.Parameters.AddWithValue("@IncomeTax", empModel.IncomeTax);
                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}
    

    


