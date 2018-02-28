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
      Course.DeleteAll();
      Department.DeleteAll();
    }

    [TestMethod]
    public void GetAll_ReturnAllStudents_ListStudent()
    {
      Student testStudent = new Student("cam", new DateTime(), 1);
      testStudent.Save();
      List<Student> testList = new List<Student>{testStudent};
      List<Student> result = Student.GetAll();

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Find_FindsStudentInDatabase_Student()
    {
      //Arrange
      Student testStudent = new Student("Kevin Jones", new DateTime(), 1);
      testStudent.Save();

      //Act
      Student foundStudent = Student.Find(testStudent.GetId());

      //Assert
      Assert.AreEqual(testStudent, foundStudent);
    }

    [TestMethod]
    public void DeleteAll_DeleteAllStudents_Void()
    {
      Student testStudent = new Student("Kevin Jones", new DateTime(), 1);
      testStudent.Save();

      Student.DeleteAll();
       int result = Student.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Delete_RemovesStudentFromDatabase_Void()
    {
      //Arrange
      Student testStudent = new Student("Kevin Jones", new DateTime(), 1);
      Student testStudent2 = new Student("Joe Smith", new DateTime(), 1);
      Course testCourse = new Course("intro to cs", "CS101", 1);
      testStudent.Save();
      testStudent2.Save();
      testCourse.Save();
      testCourse.AddStudent(testStudent);

      //Act
      testStudent.Delete();
      List<Student> allStudents = Student.GetAll();
      List<Student> testCourseStudents = testCourse.GetStudents();

      //Assert
      Assert.AreEqual(1, allStudents.Count);
      Assert.AreEqual(0, testCourseStudents.Count);
    }

    [TestMethod]
    public void AddCourse_AddCourseToStudent_Void()
    {
      Student testStudent = new Student("Kevin Jones", new DateTime(), 1);
      Course testCourse = new Course("intro to cs", "CS101", 1);
      testStudent.Save();
      testCourse.Save();
      testStudent.AddCourse(testCourse);

      List<Course> result = testStudent.GetCourses();
      Assert.AreEqual(1, result.Count);
      CollectionAssert.AreEqual(new List<Course>{testCourse}, result);
    }
  }
}
