using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace University.Models
{
  public class Department
  {
    private int _id;
    private string _name;

    public Department(string name, int id = 0)
    {
      _id = id;
      _name = name;
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

    public override bool Equals(System.Object otherDepartment)
    {
      if (!(otherDepartment is Department))
      {
        return false;
      }
      else
      {
        Department newDepartment = (Department) otherDepartment;
        return _id == newDepartment._id && _name == newDepartment._name;
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
      cmd.CommandText = @"INSERT INTO departments (name) VALUES (@name)";
      cmd.Parameters.Add(new MySqlParameter("@name", _name));
      cmd.ExecuteNonQuery();

      _id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }

    public static List<Department> GetAll()
    {
      List<Department> allDepartments = new List<Department>();

      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM departments";
      MySqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int departmentId = rdr.GetInt32(0);
        string departmentName = rdr.GetString(1);
        Department newDepartment = new Department(departmentName, departmentId);
        allDepartments.Add(newDepartment);
      }

      conn.Close();
      if (conn != null)
        conn.Dispose();

      return allDepartments;
    }

    public void Delete()
    {

    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"TRUNCATE TABLE departments;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }
  }
}
