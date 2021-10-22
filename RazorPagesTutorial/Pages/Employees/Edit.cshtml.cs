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
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EditModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        //public Employee Employee { get; private set; }
        [BindProperty] 
        public Employee Employee { get; set; }
        public IActionResult OnGet(int id)
        {
            Employee= _employeeRepository.GetEmployee(id);
            if (Employee==null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }

        //public IActionResult OnPost(Employee employee)
        //{
        //    Employee = _employeeRepository.Update(employee);
        //    return RedirectToPage("/Index");
        //}
        public IActionResult OnPost()
        {
            Employee = _employeeRepository.Update(Employee);
            return RedirectToPage("/Index");
        }
    }
}
