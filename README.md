# University

#### .NET MVC app that tracks students and courses in a database, 2-23-18

#### _By Sara Hamilton and Cameron Anderson_

## Description
_This is an Epicodus practice project for week 4 of the C# course. Its purpose is to demonstrate understanding of SQL and databases._

#### _University_
* Allows the user to add a new department
* Allows the user to add a new course and assign it to a department
* Allows the user to add a new student and assign it to a department
* Allows the user to assign students to courses
* Allows the user to view all departments
* Allows the user to view all courses and see which students are enrolled in a course
* Allows the user to view all students and see which courses a student is enrolled in

### User Stories
* As a registrar, I want to enter a student, so I can keep track of all students enrolled at this University. I should be able to provide a name and date of enrollment.
* As a registrar, I want to enter a course, so I can keep track of all of the courses the University offers. I should be able to provide a course name and a course number (ex. HIST100).
* As a registrar, I want to be able to assign students to a course, so that teachers know which students are in their course. A course can have many students and a student can take many courses at the same time.
* As a registrar, I want to be able to create departments. A student can be assigned to a department when they declare their major and a course can be assigned to a department when it is created.

  ## Setup/Installation Requirements

  * _Clone this GitHub repository_

  ```
  git clone https://github.com/Sara-Hamilton/University.Solution.git
  ```

  * _Install the .NET Framework and MAMP_

    .NET Core 1.1 SDK (Software Development Kit)

    .NET runtime.

    MAMP

    See https://www.learnhowtoprogram.com/c/getting-started-with-c/installing-c for instructions and links.

* _Start the Apache and MySql Servers in MAMP_

*   _Setup the database_

  Either type the following commands into SQL on the command line or download the zipfile of the database that is included in this Github repository.  
  ```
  CREATE DATABASE university;
  USE university;
  CREATE TABLE departments (id serial PRIMARY KEY, name VARCHAR(255));
  CREATE TABLE courses (id serial PRIMARY KEY, name VARCHAR(255), number VARCHAR(255), department_id INT);
  CREATE TABLE students (id serial PRIMARY KEY, name VARCHAR(255), enroll_date DATETIME, department_id INT);
  CREATE TABLE students_courses (id serial PRIMARY KEY, student_id INT, course_id INT);
  ```

    See https://www.learnhowtoprogram.com/c/database-basics-ee7c9fd3-fcd9-4fff-8b1d-5ff7bfcbf8f0/database-practice-and-world-data for instructions and links explaining how to download the zipfile that is located inside this github repository.

  * _Run the program_
    1. In the command line, cd into the project folder.
    ```
    cd University.Solution
    cd University
    ```
    2. In the command line, type dotnet restore. Enter.  It make take a few minutes to complete this process.
    ```
    dotnet restore
    ```
    3. In the command line, type dotnet build. Enter. Any errror messages will be displayed in red.  Errors will need to be corrected before the app can be run. After correcting errors and saving changes, type dotnet build again.  When message says Build succeeded in green, proceed to the next step.
    ```
    dotnet build
    ```
    4. In the command line, type dotnet run. Enter.
    ```
    dotnet run
    ```

  * _View program on web browser at port localhost:5000_

  * _Follow the prompts._

  ## Support and contact details

_To suggest changes, submit a pull request in the GitHub repository._

## Technologies Used

* HTML
* Bootstrap
* C#
* MAMP
* .Net Core 1.1
* Razor
* MySQL

### License

*MIT License*

Copyright (c) 2018 **_Sara Hamilton and Cameron Anderson_**

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
