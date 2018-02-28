using Microsoft.AspNetCore.Mvc;
using University.Models;
using System;
using System.Collections.Generic;

namespace University.Controllers
{
  public class DepartmentsController : Controller
  {

    [HttpGet("/departments")]
    public ActionResult Index()
    {
        List<Department> allDepartments = Department.GetAll();
        return View(allDepartments);
    }

    [HttpGet("/departments/new")]
    public ActionResult CreateForm()
    {
        return View();
    }

    [HttpPost("/departments")]
    public ActionResult Create()
    {
        Department newDepartment = new Department(Request.Form["department-name"]);
        newDepartment.Save();
        return RedirectToAction("Success", "Home");
    }
  }
}
