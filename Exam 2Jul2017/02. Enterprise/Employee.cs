using System;

public class Employee
{
    public Guid Id { get; }
    public Position Position { get; set; }
    public DateTime HireDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double Salary { get; set; }

    public string FullName { get { return this.FirstName + " " + this.LastName; } }
    public string PositionSalary { get { return this.Position + " " + this.Salary.ToString("F2"); } }
    public string FullNameAndPosition { get { return this.FullName + " " + this.Position; } }


    public Employee(string firstName, string lastName, double salary, Position position, DateTime hireDate)
    {
        this.Id = Guid.NewGuid();
        this.Position = position;
        this.HireDate = hireDate;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Salary = salary;
    }
}
