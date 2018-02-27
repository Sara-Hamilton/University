using Microsoft.VisualStudio.TestTools.UnitTesting;
using University.Models;
using System;
using System.Collections.Generic;

namespace University.Models.Tests
{
  [TestClass]
  public class StudentTests : IDisposable
 {
    public StudentTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=university_test;";
    }

    public void Dispose()
    {
      Student.DeleteAll();
    }

    [TestMethod]
    public void GetAll_ReturnAllStudents_ListStudent()
    {
      Student testStudent = new Student("cam", new DateTime());
      testStudent.Save();
      List<Student> testList = new List<Student>{testStudent};
      List<Student> result = Student.GetAll();

      CollectionAssert.AreEqual(testList, result);
    }
  }
}
