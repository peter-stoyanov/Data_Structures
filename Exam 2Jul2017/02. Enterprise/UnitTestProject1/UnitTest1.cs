using System;
using NUnit.Framework;

[TestFixture]
public class EntpTests01
{
    [Test]
    public void Add_SingleEmployee_ShouldIncreaCount()
    {
        IEnterprise enterprise = new Enterprise();

        enterprise.Add(
            new Employee("pesho", "gosho", 123, Position.Hr, DateTime.Now));

        Assert.AreEqual(1, enterprise.Count);
    }
}
