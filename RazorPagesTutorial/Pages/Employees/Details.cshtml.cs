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
    public class DetailsModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DetailsModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Employee Employee { get; private set; }
        //[BindProperty] works for OnPost by default
        //[BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        //public void OnGet(int id=1)
        public void OnGet(int id)
        {
            Id = id;
            Employee =_employeeRepository.GetEmployee(id);
        }
    }
}
