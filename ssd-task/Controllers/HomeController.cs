using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ssd_task.Models;
using System.Data;
using System.Text.RegularExpressions;

namespace EmployeeImportApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeDbContext _context;

        public HomeController(EmployeeDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employees = _context.Employees.OrderBy(e => e.Surname).ToList();
            return View(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile fileUpload)
        {
            if (fileUpload == null || fileUpload.Length == 0)
            {
                ViewBag.Feedback = "Please select a file.";
                return View("Index");
            }

            DataTable dt = new DataTable();

            try
            {
                dt = ProcessCSV(fileUpload);

                int rowsInserted = await ProcessBulkInsert(dt);
                ViewBag.Feedback = $"{rowsInserted} rows successfully added.";
            }
            catch (Exception ex)
            {
                ViewBag.Feedback = $"Error: {ex.Message}";
            }

            var employees = _context.Employees.OrderBy(e => e.Surname).ToList();
            return View("Index", employees);
        }
        private DataTable ProcessCSV(IFormFile file)
        {
            DataTable dt = new DataTable();
            string line;
            string[] headers;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                // Read the header line
                line = reader.ReadLine();
                headers = line.Split(',');
                foreach (var header in headers)
                {
                    dt.Columns.Add(header.Trim());
                }

                // Read the rest of the file
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    dt.Rows.Add(values);
                }
            }

            return dt;
        }

        private async Task<int> ProcessBulkInsert(DataTable dt)
        {
            int rowsInserted = 0;

            foreach (DataRow row in dt.Rows)
            {
                var employee = new Employee
                {
                    Personnel_Records = row["Personnel_Records"].ToString(), // Adjust column name here
                    Forenames = row["Personnel_Records.Forenames"].ToString(),
                    Surname = row["Personnel_Records.Surname"].ToString(),
                    Date_of_Birth = ParseDate(row["Personnel_Records.Date_of_Birth"].ToString()),
                    Telephone = row["Personnel_Records.Telephone"].ToString(),
                    Mobile = row["Personnel_Records.Mobile"].ToString(),
                    Address = row["Personnel_Records.Address"].ToString(),
                    Address_2 = row["Personnel_Records.Address_2"].ToString(),
                    Postcode = row["Personnel_Records.Postcode"].ToString(),
                    EMail_Home = row["Personnel_Records.EMail_Home"].ToString(),
                    Start_Date = ParseDate(row["Personnel_Records.Start_Date"].ToString())
                };


                var existingEmployee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.Personnel_Records == employee.Personnel_Records);

                if (existingEmployee != null)
                {
                    _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
                }
                else
                {
                    _context.Employees.Add(employee);
                }

                rowsInserted++;
            }

            await _context.SaveChangesAsync();
            return rowsInserted;
        }

        private DateTime ParseDate(string dateString)
        {
            string[] formats = { "dd/MM/yyyy", "MM/dd/yyyy", "yyyy-MM-dd" }; // Add more formats as needed
            DateTime date;
            DateTime.TryParseExact(dateString, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date);
            return date;
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                ViewBag.Feedback = "Employee not found.";
            }
            else
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                ViewBag.Feedback = "Employee deleted successfully.";
            }

            var employees = _context.Employees.OrderBy(e => e.Surname).ToList();
            return View("Index", employees);
        }
        //[HttpPost]
        //public async Task<IActionResult> Update(Employee updatedEmployee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var existingEmployee = await _context.Employees
        //            .FirstOrDefaultAsync(e => e.Personnel_Records == updatedEmployee.Personnel_Records);

        //        if (existingEmployee != null)
        //        {
        //            _context.Entry(existingEmployee).CurrentValues.SetValues(updatedEmployee);
        //            await _context.SaveChangesAsync();
        //            ViewBag.Feedback = "Employee updated successfully.";
        //        }
        //        else
        //        {
        //            ViewBag.Feedback = "Employee not found.";
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Feedback = "Invalid data.";
        //    }

        //    var employees = _context.Employees.OrderBy(e => e.Surname).ToList();
        //    return View("Index", employees);
        //}
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(employee);
        //}

    }

}
