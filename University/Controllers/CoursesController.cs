using Microsoft.AspNetCore.Mvc;
using University.Models;
using System;
using System.Collections.Generic;

namespace University.Controllers
{
  public class CoursesController : Controller
  {

    [HttpGet("/courses")]
    public ActionResult Index()
    {
        List<Course> allCourses = Course.GetAll();
        return View(allCourses);
    }

    [HttpGet("/courses/new")]
    public ActionResult CreateForm()
    {
      List<Department> departments = Department.GetAll();
      return View(departments);
    }

    [HttpPost("/courses")]
    public ActionResult Create()
    {
        Course newCourse = new Course(Request.Form["course-name"], Request.Form["course-number"], Int32.Parse(Request.Form["department-id"]));
        newCourse.Save();
        return RedirectToAction("Success", "Home");
    }

    [HttpGet("/courses/{id}")]
    public ActionResult Details(int id)
    {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Course selectedCourse = Course.Find(id);
        List<Student> courseStudents = selectedCourse.GetStudents();
        List<Student> allStudents = Student.GetAll();
        model.Add("selectedCourse", selectedCourse);
        model.Add("courseStudents", courseStudents);
        model.Add("allStudents", allStudents);
        return View(model);
    }

    //ADD STUDENT TO COURSE
    [HttpPost("/courses/{courseId}/students/new")]
    public ActionResult AddStudent(int courseId)
    {
        Course course = Course.Find(courseId);
        Student student = Student.Find(Int32.Parse(Request.Form["student-id"]));
        course.AddStudent(student);
        return RedirectToAction("Details",  new { id = courseId });
    }
  }
}
