using Microsoft.AspNetCore.Mvc;
using University.Models;
using System;
using System.Collections.Generic;

namespace University.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View("Index", "Hello World");
    }

    [HttpGet("/success")]
    public ActionResult Success()
    {
      return View();
    }
  }
}
