using EmployeePayroll_ADO.net;
using System;

namespace EmployeePayroll_ADO.net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Employee Payroll Service Program");
            EmployeeRepo emprepo = new EmployeeRepo();
            Console.WriteLine(emprepo.EstablishConnection());
            Console.WriteLine(emprepo.GetAllRecords());
            Console.WriteLine(emprepo.UpdateEmployee());
        }
    }
}