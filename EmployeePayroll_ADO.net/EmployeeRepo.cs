using System;
using System.Collections.Generic;
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
    }
}

