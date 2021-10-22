using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesTutorial.Models;
using RazorPagesTutorial.Services;

namespace RazorPagesTutorial.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(
            IEmployeeRepository employeeRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _employeeRepository = employeeRepository;
            _webHostEnvironment = webHostEnvironment;
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
        [BindProperty]
        public IFormFile Photo { get; set; }

        //public IActionResult OnPost(Employee employee)
        //{
        //    Employee = _employeeRepository.Update(employee);
        //    return RedirectToPage("/Index");
        //}
        public IActionResult OnPost()
        {
            //delete & upload new photo
            if (Photo != null)
            {
                if (Employee.PhotoPath!=null)
                {
                    string filePath = Path.Combine(
                        _webHostEnvironment.WebRootPath,
                        "images", Employee.PhotoPath);
                    System.IO.File.Delete(filePath);
                }

                Employee.PhotoPath = ProcessingUploadedFile();
            }
            Employee = _employeeRepository.Update(Employee);
            return RedirectToPage("/Index");
        }

        private string ProcessingUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream=new FileStream(filePath,FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;

        }
    }
}
