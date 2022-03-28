﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayroll_ADO.net
{
    public class EmployeeModel
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public double BasicPay { get; set; }
        public DateTime start_date { get; set; }
        public char gendre { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public double Deduction { get; set; }
        public Single TaxablePay { get; set; }
        public Single NetPay { get; set; }
        public double Tax { get; set; }
    }
}
