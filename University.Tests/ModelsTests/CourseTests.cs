using Microsoft.VisualStudio.TestTools.UnitTesting;
using University.Models;
using System;
using System.Collections.Generic;

namespace University.Models.Tests
{
  [TestClass]
  public class CourseTests : IDisposable
  {
    public CourseTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=university_test;";
    }

    public void Dispose()
    {
      Student.DeleteAll();
      Course.DeleteAll();
    }

    [TestMethod]
    public void GetAll_ReturnAllCourses_ListCourse()
    {
      Course testCourse = new Course("intro to cs", "CS101");
      testCourse.Save();
      List<Course> testList = new List<Course>{testCourse};
      List<Course> result = Course.GetAll();

      Assert.AreEqual(1, result.Count);
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void DeleteAll_DeleteAllCourses_Void()
    {
      Course testCourse = new Course("intro to cs", "CS101");
      testCourse.Save();

      Course.DeleteAll();
       int result = Course.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Find_FindsCourseInDatabase_Course()
    {
      //Arrange
      Course testCourse = new Course("intro to cs", "CS101");
      testCourse.Save();

      //Act
      Course foundCourse = Course.Find(testCourse.GetId());

      //Assert
      Assert.AreEqual(testCourse, foundCourse);
    }

    [TestMethod]
    public void Delete_RemovesCourseFromDatabase_Void()
    {
      //Arrange
      Course testCourse = new Course("intro to cs", "CS101");
      Course testCourse2 = new Course("intermediate cs", "CS102");
      testCourse.Save();
      testCourse2.Save();

      //Act

      testCourse.Delete();
      List<Course> allCourses = Course.GetAll();

      //Assert
      Assert.AreEqual(1, allCourses.Count);
    }


  }
}
