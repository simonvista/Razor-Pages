using System;
using System.Collections.Generic;
using System.Text;
using RazorPagesTutorial.Models;

namespace RazorPagesTutorial.Services
{
    interface IEmployeeRepository
    {
        IEnumerable<Employee> getAllEmployees();
    }
}
