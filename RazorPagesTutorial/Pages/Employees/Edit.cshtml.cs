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
        
        [BindProperty]
        public IFormFile Photo { get; set; }
        [BindProperty]
        public bool Notify { get; set; }
        public string Message { get; set; }

        //public IActionResult OnPost(Employee employee)
        //{
        //    Employee = _employeeRepository.Update(employee);
        //    return RedirectToPage("/Index");
        //}
        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Employee = _employeeRepository.GetEmployee(id.Value);
                if (Employee == null)
                {
                    return RedirectToPage("/NotFound");
                }
            }
            else
            {
                Employee = new Employee();
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            //delete & upload new photo
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (Employee.PhotoPath != null)
                    {
                        string filePath = Path.Combine(
                            _webHostEnvironment.WebRootPath,
                            "images", Employee.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    Employee.PhotoPath = ProcessingUploadedFile();
                }

                if (Employee.Id>0)
                {
                    Employee = _employeeRepository.Update(Employee);
                }
                else
                {
                    Employee = _employeeRepository.Add(Employee);
                }
                return RedirectToPage("Index");
            }
            else
            {
                return Page();  //stay on current page if input is not valid
            }
        }

        //public void OnPostUpdateNotificationPreferences(int id)
        //{
        //    Message = Notify ? "Email notification was on" : "Email notification was off";
        //    Employee = _employeeRepository.GetEmployee(id);
        //}
        public IActionResult OnPostUpdateNotificationPreferences(int id)
        {
            Message = Notify ? "Email notification was on" : "Email notification was off";
            //Employee = _employeeRepository.GetEmployee(id);
            //TempData will be sent to Details page
            TempData["message"] = Message;
            //return RedirectToPage("Details",new {id=id,message=Message});
            return RedirectToPage("Details", new { id = id });
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
