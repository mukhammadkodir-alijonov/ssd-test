using Microsoft.EntityFrameworkCore;
using ssd_task.Models;
using System.Collections.Generic;

public class EmployeeDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }

}
