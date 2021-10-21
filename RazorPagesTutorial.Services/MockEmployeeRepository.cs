﻿using System;
using System.Collections.Generic;
using RazorPagesTutorial.Models;

namespace RazorPagesTutorial.Services
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employees;

        public MockEmployeeRepository()
        {
            _employees = new List<Employee>()
            {
                new Employee()
                {
                    Id = 1, 
                    Name = "Mary", 
                    Department = Dept.HR,
                    Email = "mary@pragimtech.com",
                    PhotoPath = "mary.png"
                },
                new Employee()
                {
                    Id = 2, 
                    Name = "John", 
                    Department = Dept.IT,
                    Email = "john@pragimtech.com",
                    PhotoPath = "john.png"
                },
                new Employee()
                {
                    Id = 3, 
                    Name = "Sara", 
                    Department = Dept.IT,
                    Email = "sara@pragimtech.com",
                    PhotoPath = "sara.png"
                },
                new Employee()
                {
                    Id = 4,
                    Name = "David",
                    Department = Dept.Payroll,
                    Email = "david@pragimtech.com",
                    PhotoPath = "david.png"
                },
            };
        }
        public IEnumerable<Employee> getAllEmployees()
        {
            return _employees;
        }
    }
}
