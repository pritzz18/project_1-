using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer
{
    public class Employee
    {
        
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeGender { get; set; }
        public string EmployeeDesignation { get; set; }
        
        public string Email_id { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public List<States> states { get; set; }
    }
}