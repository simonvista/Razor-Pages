using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesTutorial.Models;
using RazorPagesTutorial.Services;

namespace RazorPagesTutorial.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;
        public IEnumerable<Employee> Employees { get; set; }

        public IndexModel(IEmployeeRepository employeeRepository)
        {
            //DI
            _employeeRepository = employeeRepository;
        }
        public void OnGet()
        {
            Employees = _employeeRepository.getAllEmployees();
        }
    }
}
