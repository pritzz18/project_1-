using BuisnessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_1_.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeBuisnessLayer employeeBuisnessLayer = new EmployeeBuisnessLayer();

        // GET: EmployeeController
        public ActionResult Index()
        {
            string s = Convert.ToString(HttpContext.Session.GetString("SearchTerm"));
            SearchModel searchmodel = new SearchModel();
            if(string.IsNullOrEmpty(s))
            {
                searchmodel.SearchTerm = "";
            }
           
            IEnumerable<Employee> employees = employeeBuisnessLayer.GetEmployees(searchmodel.SearchTerm,searchmodel.StateId);
            searchmodel.List = employees.ToList();
            if(searchmodel.List == null)
            {
                searchmodel.List = new List<Employee>();
            }
            searchmodel.states = employeeBuisnessLayer.GetStates();
            return View(searchmodel);
        }
        [HttpPost]
        public ActionResult Index(SearchModel t)
        {
            HttpContext.Session.SetString("SearchTerm",string.IsNullOrEmpty(t.SearchTerm)?"":t.SearchTerm);
            HttpContext.Session.SetInt32("StateId", t.StateId); 
            SearchModel searchModel = new SearchModel();
            searchModel.SearchTerm = t.SearchTerm;
            searchModel.StateId = t.StateId;
            var res = employeeBuisnessLayer.GetEmployees(t.SearchTerm,t.StateId);
            searchModel.List = res.ToList();
            searchModel.states = employeeBuisnessLayer.GetStates();
            return View(searchModel);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            Employee emp = employeeBuisnessLayer.GetEmployeeDetails(id);
            return View(emp);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            Employee emp = new Employee();
            emp.states = new List<States>();
            emp.states = employeeBuisnessLayer.GetStates().ToList();
            return View(emp);
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if(ModelState.IsValid)
            {
                var res= employeeBuisnessLayer.CheckEmail(employee);
                if(res > 0)
                {
                    ModelState.AddModelError("Email_id", "Email Allready Exist");
                    employee.states = employeeBuisnessLayer.GetStates().ToList();
                    return View(employee);
                }
                employeeBuisnessLayer.AddEmployee(employee);   
                employee.states = employeeBuisnessLayer.GetStates().ToList();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
            
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            Employee employee = employeeBuisnessLayer.GetEmployeeDetails(id);
            
            if(employee == null)
            {
                return NotFound();
            }
            if (employee.states == null)
            {
                employee.states = new List<States>();
            }
            employee.states = employeeBuisnessLayer.GetStates().ToList();
            return View(employee);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var res = employeeBuisnessLayer.CheckEmail(employee);
                    if (res > 0)
                    {
                        ModelState.AddModelError("Email_id", "Email Allready Exist");
                        employee.states = employeeBuisnessLayer.GetStates().ToList();
                        return View(employee);
                    }
                    employeeBuisnessLayer.Updateemployee(employee);
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    return View();
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "error while updateing");
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            Employee emp = employeeBuisnessLayer.GetEmployeeDetails(id);
            return View(emp);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteC(int id)
        {
            try
            {
                employeeBuisnessLayer.DeleteEmployee(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
