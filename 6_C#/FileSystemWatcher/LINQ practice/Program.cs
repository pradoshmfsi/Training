using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        //// Data source
        //string[] names = { "Bill", "Steve", "James", "Mohan" };

        //// LINQ Query 
        //var myLinqQuery = from name in names
        //                  where name.Contains('a')
        //                  select name;

        //// Query execution
        //foreach (var name in myLinqQuery)
        //    Console.Write(name + " ");

        //IList<Student> studentList = new List<Student>() {
        //        new Student() { StudentID = 1, StudentName = "John", Age = 18, StandardID = 1 } ,
        //        new Student() { StudentID = 2, StudentName = "Steve",  Age = 21, StandardID = 1 } ,
        //        new Student() { StudentID = 3, StudentName = "Bill",  Age = 18, StandardID = 2 } ,
        //        new Student() { StudentID = 4, StudentName = "Ram" , Age = 20, StandardID = 2 } ,
        //        new Student() { StudentID = 5, StudentName = "Ron" , Age = 21 }
        //    };

        //IList<Standard> standardList = new List<Standard>() {
        //        new Standard(){ StandardID = 1, StandardName="Standard 1"},
        //        new Standard(){ StandardID = 2, StandardName="Standard 2"},
        //        new Standard(){ StandardID = 3, StandardName="Standard 3"}
        //    };

        //var groupJoin = standardList.GroupJoin(studentList,  //inner sequence
        //                        std => std.StandardID, //outerKeySelector 
        //                        s => s.StandardID,     //innerKeySelector
        //                        (std, studentsGroup) => new // resultSelector 
        //                        {
        //                            Students = studentsGroup,
        //                            StandarFulldName = std.StandardName
        //                        });

        //foreach (var item in groupJoin)
        //{
        //    Console.WriteLine(item.StandarFulldName);

        //    foreach (var stud in item.Students)
        //        Console.WriteLine(stud.StudentName);
        //}


        IList<Student> studentList = new List<Student>() {
            new Student() { StudentID = 1, StudentName = "John", Age = 13 } ,
            new Student() { StudentID = 2, StudentName = "Moin",  Age = 21 } ,
            new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
            new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
            new Student() { StudentID = 5, StudentName = "Ron" , Age = 15 }
        };

        // returns collection of anonymous objects with Name and Age property
        //var selectResult = from s in studentList
        //                   select new { Name = "Mr. " + s.StudentName, Age = s.Age };

        //var selectResult = studentList.Select(s => new {
        //    Name = s.StudentName,
        //    Age = s.Age
        //});


        //// iterate selectResult
        //foreach (var item in selectResult)
        //    Console.WriteLine("Student Name: {0}, Age: {1}", item.Name, item.Age);
        //Student std = new Student() { StudentID = 3, StudentName = "Bill" };

        //bool result = studentList.Contains(std, new StudentComparer());

        //Console.WriteLine(result);
        //string commaSeparatedStudentNames = studentList.Aggregate<Student, string, string>(
        //                                    String.Empty, // seed value
        //                                    (str, s) => str += s.StudentName + ",", 
        //                                    str => str.Substring(0, str.Length - 1)); 

        //Console.WriteLine(commaSeparatedStudentNames);

        Student std = new Student() { StudentID = 1, StudentName = "Bill" };
        Student std1 = new Student() { StudentID = 3, StudentName = "Bill" };

        Console.WriteLine(studentList.Any(r => r.StudentID == std.StudentID));

        Console.ReadLine();
    }
}
public class Student
{

    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
    public int StandardID { get; set; }
}

public class Standard
{

    public int StandardID { get; set; }
    public string StandardName { get; set; }
}
public class StudentComparer : IEqualityComparer<Student>
{
    public bool Equals(Student x, Student y)
    {
        if (x.StudentID == y.StudentID &&
                 x.StudentName.ToLower() == y.StudentName.ToLower())
            return true;

        return false;
    }

    public int GetHashCode(Student obj)
    {
        return obj.GetHashCode();
    }
}  