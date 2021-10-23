﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RazorPagesTutorial.Models
{
    public class Employee
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "name is required")]
        //[MinLength(3,ErrorMessage = "name should be at least 3 characters")]
        [Required(ErrorMessage = "name is required"),
         MinLength(3, ErrorMessage = "name should be at least 3 characters")]
        public string Name { get; set; }
        [Required()]
        [RegularExpression(
            @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid email format")]
        [Display(Name = "office email")]
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public Dept? Department { get; set; }
    }
}
