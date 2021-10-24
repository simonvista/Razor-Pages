﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RazorPagesTutorial.Models;

namespace RazorPagesTutorial.Services
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public SqlEmployeeRepository(AppDbContext context)
        {
            _context = context;
        }
        //FromSqlRaw: execute sql query or stored procedure and return entities
        //ExecuteSqlRaw: execute sql query or stored procedure to do DB operations but with no returns
        public IEnumerable<Employee> GetAllEmployees()
        {
            //LINQ
            //return _context.Employees;
            //FromSqlRaw runs sql
            return _context.Employees
                .FromSqlRaw<Employee>("select * from employees")
                .ToList();
        }

        public Employee GetEmployee(int id)
        {
            //SqlParameter obj
            SqlParameter parameter = new SqlParameter("@Id", id);
            //LINQ query
            //return _context.Employees.Find(id);
            //FromSqlRaw runs SQL stored procedure
            //placeholder syntax
            //return _context.Employees
            //                .FromSqlRaw<Employee>("spGetEmployeeById {0}",id)
            //                .ToList()
            //                .FirstOrDefault();
            //SqlParameter obj
            return _context.Employees
                .FromSqlRaw<Employee>("spGetEmployeeById @Id", parameter)
                .ToList()
                .FirstOrDefault();
        }

        public Employee Update(Employee updatedEmployee)
        {
            var entityEntry = _context.Employees.Attach(updatedEmployee);
            entityEntry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return updatedEmployee;
        }

        public Employee Add(Employee newEmployee)
        {
            //LINQ
            //_context.Employees.Add(newEmployee);
            //_context.SaveChanges();
            //return newEmployee;
            //ExecuteSqlRaw
            _context.Database.ExecuteSqlRaw(
                "spInsertEmployee {0},{1},{2},{3}",
                newEmployee.Name,
                newEmployee.Email,
                newEmployee.PhotoPath,
                newEmployee.Department);
            return newEmployee;
        }

        public Employee Delete(int id)
        {
            Employee employee= _context.Employees.Find(id);
            if (employee!=null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }

            return employee;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept)
        {
            IEnumerable<Employee> query = _context.Employees;
            if (dept.HasValue)
            {
                query = query.Where(e => e.Department == dept.Value);
            }
            return query.GroupBy(e => e.Department)
                .Select(g => new DeptHeadCount()
                {
                    Department = g.Key.Value,
                    Count = g.Count()
                }).ToList();
        }

        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return _context.Employees;
            }
            return _context.Employees.Where(e => e.Name.Contains(searchTerm) || e.Email.Contains(searchTerm));
        }
    }
}
