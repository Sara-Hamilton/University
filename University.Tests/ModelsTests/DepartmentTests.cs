using Microsoft.VisualStudio.TestTools.UnitTesting;
using University.Models;
using System;
using System.Collections.Generic;

namespace University.Models.Tests
{
  [TestClass]
  public class DepartmentTests : IDisposable
  {
    public DepartmentTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=university_test;";
    }

    public void Dispose()
    {
      Student.DeleteAll();
      Course.DeleteAll();
      Department.DeleteAll();
    }

    [TestMethod]
    public void GetAll_ReturnAllDepartments_ListDepartment()
    {
      Department testDepartment = new Department("math");
      testDepartment.Save();
      List<Department> testList = new List<Department>{testDepartment};
      List<Department> result = Department.GetAll();

      Assert.AreEqual(1, result.Count);
      CollectionAssert.AreEqual(testList, result);

    }
  }
}
