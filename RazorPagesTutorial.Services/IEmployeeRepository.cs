﻿using System;
using System.Collections.Generic;
using System.Text;
using RazorPagesTutorial.Models;

namespace RazorPagesTutorial.Services
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> getAllEmployees();
        Employee GetEmployee(int id);
    }
}
