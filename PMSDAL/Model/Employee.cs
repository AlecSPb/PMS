﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL.Model
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string EmployeeName { get; set; }
        public string State { get; set; }
    }
}
