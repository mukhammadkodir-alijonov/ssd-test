using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ssd_task.Models;
public class Employee
{
    [Key]
    [Column(TypeName = "varchar(450)")]
    public string Personnel_Records { get; set; }
    [Column(TypeName = "varchar(450)")]
    public string Forenames { get; set; }
    [Column(TypeName = "varchar(450)")]
    public string Surname { get; set; }
    public DateTime Date_of_Birth { get; set; }
    [Column(TypeName = "varchar(450)")]
    public string Telephone { get; set; }
    [Column(TypeName = "varchar(450)")]
    public string Mobile { get; set; }
    [Column(TypeName = "varchar(450)")]
    public string Address { get; set; }
    [Column(TypeName = "varchar(450)")]
    public string Address_2 { get; set; }
    [Column(TypeName = "varchar(450)")]
    public string Postcode { get; set; }
    [Column(TypeName = "varchar(450)")]
    public string EMail_Home { get; set; }
    public DateTime Start_Date { get; set; }
}

