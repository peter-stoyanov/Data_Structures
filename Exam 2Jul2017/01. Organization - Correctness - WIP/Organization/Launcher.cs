using System.Collections.Generic;
using System.Linq;

public class Launcher
{
    public static void Main()
    {
        // Arrange
        IOrganization org = new Organization();
        List<Person> people = new List<Person>
        {
            new Person("Ivan", 350),
            new Person("Pesho", 1200),
            new Person("Mitko", 20),
            new Person("Maria", 0),
            new Person("Stamat", 1500),
            new Person("Alex", 850),
            new Person("Rosi", 3000)
        };

        foreach (Person person in people)
        {
            org.Add(person);
        }

        // Act & Assert
        var a = people.Where(x => x.Name.Length == 4);
        var b = org.GetWithNameSize(4);

        System.Console.WriteLine();
    }
}
