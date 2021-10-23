﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RazorPagesTutorial.Models;
using RazorPagesTutorial.Services;

namespace RazorPagesTutorial.ViewComponents
{
    public class HeadCountViewComponent : ViewComponent
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HeadCountViewComponent(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IViewComponentResult Invoke()
        {
            var result=_employeeRepository.EmployeeCountByDept();
            return View(result);
        }
    }
}
