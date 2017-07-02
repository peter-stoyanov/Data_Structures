using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Enterprise2 : IEnterprise
{
    //by Guid
    private Dictionary<Guid, Employee> _byGUID;
    //by sorted Date
    private OrderedDictionary<DateTime, List<Guid>> _byDate;
    //by first name
    private Dictionary<string, List<Guid>> _byFirstName;
    //by full name and position
    private Dictionary<string, List<Guid>> _byFullNameAndPosition;
    //by full name
    private Dictionary<string, List<Guid>> _byFullName;
    //by position and salary
    //private Dictionary<string, List<Employee>> _byPositionAndSalary;
    //by position
    private Dictionary<string, List<Guid>> _byPosition;
    //by sorted salary
    private OrderedDictionary<double, List<Guid>> _bySalary;

    public Enterprise2()
    {
        this._byGUID = new Dictionary<Guid, Employee>();
        this._byDate = new OrderedDictionary<DateTime, List<Guid>>();
        this._byFirstName = new Dictionary<string, List<Guid>>();
        this._byFullNameAndPosition = new Dictionary<string, List<Guid>>();
        this._byPosition = new Dictionary<string, List<Guid>>();
        this._bySalary = new OrderedDictionary<double, List<Guid>>();
        this._byFullName = new Dictionary<string, List<Guid>>();
        //this._byPositionAndSalary = new Dictionary<string, List<Employee>>();
    }

    //public int Count;
    public int Count { get { return this._byGUID.Count; } }

    public void Add(Employee employee)
    {
        if (employee != null)
        {
            string firstName = employee.FirstName != null ? employee.FirstName : string.Empty;
            string fullName = employee.FullName != null ? employee.FullName : string.Empty;
            string fullNameAndPosition = employee.FullNameAndPosition;
            string positionAndSalary = employee.PositionSalary;
            double salary = employee.Salary;
            string position = employee.Position.ToString();
            DateTime dateHired = employee.HireDate;
            //Guid guid = employee.Id;


            //add to byGuid
            //Guid newGuid = Guid.NewGuid();
            this._byGUID.Add(employee.Id, employee);

            //add for first name search
            if (!_byFirstName.ContainsKey(firstName))
            {
                _byFirstName.Add(firstName, new List<Guid>(200000) { employee.Id });
            }
            else
            {
                _byFirstName[firstName].Add(employee.Id);
            }

            //add for full name search
            if (!_byFullName.ContainsKey(fullName))
            {
                _byFullName.Add(fullName, new List<Guid>(200000) { employee.Id });
            }
            else
            {
                _byFullName[fullName].Add(employee.Id);
            }

            //add for full name and position search
            if (!_byFullNameAndPosition.ContainsKey(fullNameAndPosition))
            {
                _byFullNameAndPosition.Add(fullNameAndPosition, new List<Guid>(200000) { employee.Id });
            }
            else
            {
                _byFullNameAndPosition[fullNameAndPosition].Add(employee.Id);
            }

            //add for position + salary search
            //if (!_byPositionAndSalary.ContainsKey(positionAndSalary))
            //{
            //    _byPositionAndSalary.Add(positionAndSalary, new List<Employee>() { employee });
            //}
            //else
            //{
            //    _byPositionAndSalary[positionAndSalary].Add(employee);
            //}

            //add for date
            if (!_byDate.ContainsKey(dateHired))
            {
                _byDate.Add(dateHired, new List<Guid>(200000) { employee.Id });
            }
            else
            {
                _byDate[dateHired].Add(employee.Id);
            }

            //add for salary
            if (!_bySalary.ContainsKey(salary))
            {
                _bySalary.Add(salary, new List<Guid>(200000) { employee.Id });
            }
            else
            {
                _bySalary[salary].Add(employee.Id);
            }

            //add for position
            if (!_byPosition.ContainsKey(position))
            {
                _byPosition.Add(position, new List<Guid>(200000) { employee.Id });
            }
            else
            {
                _byPosition[position].Add(employee.Id);
            }

        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<Employee> GetEnumerator()
    {
        return ((IEnumerable<Employee>)this._byFirstName).GetEnumerator();
    }

    public IEnumerable<Employee> AllWithPositionAndMinSalary(Position position, double minSalary)
    {
        //string positionSalary = position.ToString() + " " + minSalary.ToString("F2");
        IEnumerable<Employee> bysalary;
        try
        {
            bysalary = GetBySalary(minSalary);

        }
        catch (Exception)
        {
            return Enumerable.Empty<Employee>();
        }

        return bysalary.Where(e => e.Position == position);
    }

    public bool Change(Guid guid, Employee employee)
    {
        if (!this._byGUID.ContainsKey(guid))
        {
            return false;
        }

        var oldEmployee = _byGUID[guid];
        oldEmployee.FirstName = employee.FirstName;
        oldEmployee.HireDate = employee.HireDate;
        oldEmployee.LastName = employee.LastName;
        oldEmployee.Position = employee.Position;
        oldEmployee.Salary = employee.Salary;

        return true;
    }

    public bool Contains(Guid guid)
    {
        return _byGUID.ContainsKey(guid);
    }

    public bool Contains(Employee employee)
    {
        return _byGUID.ContainsKey(employee.Id);
    }

    public bool Fire(Guid guid)
    {
        if (!this._byGUID.ContainsKey(guid))
        {
            return false;
        }

        Employee employeeToDelete = _byGUID[guid];

        _byGUID.Remove(guid);
        _byDate.Remove(employeeToDelete.HireDate);
        _byFirstName.Remove(employeeToDelete.FirstName);
        _byFullName.Remove(employeeToDelete.FullName);
        _byFullNameAndPosition.Remove(employeeToDelete.FullNameAndPosition);
        _byPosition.Remove(employeeToDelete.Position.ToString());
        //_byPositionAndSalary.Remove(employeeToDelete.PositionSalary);
        _bySalary.Remove(employeeToDelete.Salary);

        return true;
    }

    public Employee GetByGuid(Guid guid)
    {
        if (!this._byGUID.ContainsKey(guid))
        {
            throw new ArgumentException();
        }

        return this._byGUID[guid];
    }

    public IEnumerable<Employee> GetByPosition(Position position)
    {
        if (!_byPosition.ContainsKey(position.ToString()))
        {
            return Enumerable.Empty<Employee>();
        }
        else
        {
            return GetEmployeesByGuids(_byPosition[position.ToString()]);
        }
    }

    private IEnumerable<Employee> GetEmployeesByGuids(Guid guid)
    {
        return GetEmployeesByGuids(new List<Guid>() { guid });
    }

    private IEnumerable<Employee> GetEmployeesByGuids(List<Guid> list)
    {
        var result = new List<Employee>();
        foreach (var i in list)
        {
            var empl = GetByGuid(i);
            if (empl != null)
            {
                result.Add(empl);
            }
        }

        return result;
    }

    public IEnumerable<Employee> GetBySalary(double minSalary)
    {
        double max = double.MaxValue;

        var range = this._bySalary.Range(minSalary, true, max, true);

        if (range.Values.Count == 0)
        {
            throw new InvalidOperationException();
        }

        foreach (var kvp in range)
        {
            foreach (var employee in kvp.Value)
            {
                yield return GetEmployeesByGuids(employee).FirstOrDefault();
            }
        }
    }

    public IEnumerable<Employee> GetBySalaryAndPosition(double salary, Position position)
    {
        if (!_bySalary.ContainsKey(salary))
        {
            throw new InvalidOperationException();
        }
        else
        {
            return GetEmployeesByGuids(_bySalary[salary]).Where(e => e.Position == position);
        }
    }


    public Position PositionByGuid(Guid guid)
    {
        if (!this._byGUID.ContainsKey(guid))
        {
            throw new InvalidOperationException();
        }

        return this._byGUID[guid].Position;
    }

    public bool RaiseSalary(int months, int percent)
    {
        var dateLimit = DateTime.Now.AddMonths(-months);
        bool hasModifiedAny = false;
        var range = this._byDate.Range(DateTime.MinValue, true, dateLimit, true);

        foreach (var kvp in range)
        {
            foreach (var employee in kvp.Value)
            {
                var employeereal = GetEmployeesByGuids(employee).FirstOrDefault();
                employeereal.Salary = employeereal.Salary * (1 + (percent / 100.0));
                hasModifiedAny = true;
            }
        }

        return hasModifiedAny;
    }

    public IEnumerable<Employee> SearchByFirstName(string firstName)
    {
        if (!_byFirstName.ContainsKey(firstName))
        {
            return Enumerable.Empty<Employee>();
        }
        else
        {
            return GetEmployeesByGuids(_byFirstName[firstName]);
        }
    }

    public IEnumerable<Employee> SearchByNameAndPosition(string firstName, string lastName, Position position)
    {
        string nameAndPosition = firstName + " " + lastName + position.ToString();

        if (!_byFullNameAndPosition.ContainsKey(nameAndPosition))
        {
            return Enumerable.Empty<Employee>();
        }
        else
        {
            return GetEmployeesByGuids(_byFullNameAndPosition[nameAndPosition]);
        }
    }

    public IEnumerable<Employee> SearchByPosition(IEnumerable<Position> positions)
    {
        var result = new List<Guid>();
        foreach (var position in positions)
        {
            if (_byPosition.ContainsKey(position.ToString()))
            {
                if (_byPosition[position.ToString()] != null)
                {
                    result.AddRange(_byPosition[position.ToString()].ToList());
                }
            }
        }

        return result.Select(x => GetEmployeesByGuids(x).FirstOrDefault());
    }

    public IEnumerable<Employee> SearchBySalary(double minSalary, double maxSalary)
    {
        var range = this._bySalary.Range(minSalary, true, maxSalary, true);

        foreach (var kvp in range)
        {
            foreach (var employee in kvp.Value)
            {
                yield return GetEmployeesByGuids(employee).FirstOrDefault();
            }
        }
    }

}

