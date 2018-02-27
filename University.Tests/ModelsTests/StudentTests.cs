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

    [TestMethod]
    public void Find_FindsStudentInDatabase_Student()
    {
      //Arrange
      Student testStudent = new Student("Kevin Jones", new DateTime());
      testStudent.Save();

      //Act
      Student foundStudent = Student.Find(testStudent.GetId());

      //Assert
      Assert.AreEqual(testStudent, foundStudent);
    }

    [TestMethod]
    public void DeleteAll_DeleteAllStudents_Void()
    {
      Student testStudent = new Student("Kevin Jones", new DateTime());
      testStudent.Save();

      Student.DeleteAll();
       int result = Student.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Delete_RemovesStudentFromDatabase_Void()
    {
      //Arrange
      Student testStudent = new Student("Kevin Jones", new DateTime());
      Student testStudent2 = new Student("Joe Smith", new DateTime());
      testStudent.Save();
      testStudent2.Save();

      //Act

      testStudent.Delete();
      List<Student> allStudents = Student.GetAll();

      //Assert
      Assert.AreEqual(1, allStudents.Count);
    }
  }
}
