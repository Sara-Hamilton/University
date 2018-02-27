using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace University.Models
{
  public class Course
  {
    private int _id;
    private string _name;
    private string _number;

    public Course(string name, string number, int id = 0)
    {
      _name = name;
      _number = number;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetNumber()
    {
      return _number;
    }

    public void SetName(string name)
    {
      _name = name;
    }

    public void SetNumber(string number)
    {
      _number = number;
    }

    public override bool Equals(System.Object otherCourse)
    {
      if (!(otherCourse is Course))
      {
        return false;
      }
      else
      {
        Course newCourse = (Course) otherCourse;
        return _id == newCourse._id && _name == newCourse._name && _number == newCourse._number;
      }
    }

    public override int GetHashCode()
    {
      return _id.GetHashCode();
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"INSERT INTO courses (name, number) VALUES (@name, @number)";
      cmd.Parameters.Add(new MySqlParameter("@name", _name));
      cmd.Parameters.Add(new MySqlParameter("@number", _number));
      cmd.ExecuteNonQuery();

      _id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }

    public static List<Course> GetAll()
    {
      List<Course> allCourses = new List<Course>();

      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM courses";
      MySqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int courseId = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
        Course newCourse = new Course(courseName, courseNumber, courseId);
        allCourses.Add(newCourse);
      }

      conn.Close();
      if (conn != null)
        conn.Dispose();
      return allCourses;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"TRUNCATE TABLE courses;TRUNCATE TABLE students_courses;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }

    public static Course Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM `courses` WHERE id = @thisId;";

      cmd.Parameters.Add(new MySqlParameter("@thisId", id));

      MySqlDataReader rdr = cmd.ExecuteReader();

      int courseId = 0;
      string courseName = "";
      string courseNumber = "";

      while (rdr.Read())
      {
        courseId = rdr.GetInt32(0);
        courseName = rdr.GetString(1);
        courseNumber = rdr.GetString(2);
      }

      Course foundCourse = new Course(courseName, courseNumber, courseId);

      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }

      return foundCourse;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM courses WHERE id = @thisId; DELETE from students_courses WHERE course_id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = this._id;
      cmd.Parameters.Add(thisId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void AddStudent(Student student)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"INSERT INTO students_courses (student_id, course_id) VALUES (@StudentId, @CourseId)";
      cmd.Parameters.Add(new MySqlParameter("@CourseId", _id));
      cmd.Parameters.Add(new MySqlParameter("@StudentId", student.GetId()));
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }

    public List<Student> GetStudents()
    {
      List<Student> students = new List<Student>();

      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"
        SELECT students.* FROM courses
        JOIN students_courses ON (courses.id = students_courses.course_id)
        JOIN students ON (students_courses.student_id = students.id)
        WHERE courses.id = @ThisId;";
      cmd.Parameters.Add(new MySqlParameter("@ThisId", _id));
      MySqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        DateTime studentDate = rdr.GetDateTime(2);
        Student newStudent = new Student(studentName, studentDate, studentId);
        students.Add(newStudent);
      }

      conn.Close();
      if (conn != null)
        conn.Dispose();

      return students;
    }
  }
}
