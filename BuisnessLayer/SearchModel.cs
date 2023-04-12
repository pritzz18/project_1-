using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer
{
    public class SearchModel
    {
        public string SearchTerm { get; set; }
        public List<Employee> List { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public List<States> states { get; set; }
    }
}
