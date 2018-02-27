using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace University.Models
{
  public class Student
  {
    private int _id;
    private string _name;
    private DateTime _enrollDate;

    public Student(string name, DateTime date, int id = 0)
    {
      _id = id;
      _name = name;
      _enrollDate = date;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName(string name)
    {
      _name = name;
    }

    public DateTime GetEnrollDate()
    {
      return _enrollDate;
    }

    public void SetEnrollDate(DateTime date)
    {
      _enrollDate = date;
    }

    public override bool Equals(System.Object otherStudent)
    {
      if (!(otherStudent is Student))
      {
        return false;
      }
      else
      {
        Student newStudent = (Student) otherStudent;
        return _id == newStudent._id && _name == newStudent._name && _enrollDate == newStudent._enrollDate;
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
      cmd.CommandText = @"INSERT INTO students (name, enroll_date) VALUES (@name, @date)";
      cmd.Parameters.Add(new MySqlParameter("@name", _name));
      cmd.Parameters.Add(new MySqlParameter("@date", _enrollDate));
      cmd.ExecuteNonQuery();

      _id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }

    public static List<Student> GetAll()
    {
      List<Student> allStudents = new List<Student>();

      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM students";
      MySqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        DateTime date = rdr.GetDateTime(2);
        Student newStudent = new Student(studentName, date, studentId);
        allStudents.Add(newStudent);
      }

      conn.Close();
      if (conn != null)
        conn.Dispose();
      return allStudents;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"TRUNCATE TABLE students;TRUNCATE TABLE students_courses;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }

    public static Student Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM `students` WHERE id = @thisId;";

      cmd.Parameters.Add(new MySqlParameter("@thisId", id));

      MySqlDataReader rdr = cmd.ExecuteReader();

      int studentId = 0;
      string studentName = "";
      DateTime studentEnrollDate = DateTime.Now;

      while (rdr.Read())
      {
        studentId = rdr.GetInt32(0);
        studentName = rdr.GetString(1);
        studentEnrollDate = rdr.GetDateTime(2);
      }

      Student foundStudent = new Student(studentName, studentEnrollDate, studentId);

      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }

      return foundStudent;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM students WHERE id = @thisId; DELETE from students_courses WHERE student_id = @thisId;";

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

    public List<Course> GetCourses()
    {
      List<Course> courses = new List<Course>();

      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"
        SELECT courses.* FROM students
        JOIN students_courses ON (students.id = students_courses.student_id)
        JOIN courses ON (students_courses.course_id = courses.id)
        WHERE students.id = @ThisId";
      cmd.Parameters.Add(new MySqlParameter("@ThisId", _id));
      MySqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int courseId = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
        Course newCourse = new Course(courseName, courseNumber, courseId);
        courses.Add(newCourse);
      }

      conn.Close();
      if (conn != null)
        conn.Dispose();

      return courses;
    }

    public void AddCourse(Course course)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"INSERT INTO students_courses (student_id, course_id) VALUES (@StudentId, @CourseId)";
      cmd.Parameters.Add(new MySqlParameter("@CourseId", course.GetId()));
      cmd.Parameters.Add(new MySqlParameter("@StudentId", _id));
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }
  }
}
