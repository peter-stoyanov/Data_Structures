using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Organization : IOrganization
{
    //keep order of hiring and ensure fast access by index
    private readonly List<Person> _byOrderOfHiring;
    
    //handle fast search by name
    private readonly Dictionary<string, List<Person>> _byName;
    
    //handle fast search by name length and fast range queries
    private readonly OrderedDictionary<int, List<Person>> _byNameLength;

    public Organization()
    {
        _byOrderOfHiring = new List<Person>(100000);
        _byName = new Dictionary<string, List<Person>>();
        _byNameLength = new OrderedDictionary<int, List<Person>>();
    }


    public IEnumerator<Person> GetEnumerator()
    {
        return ((IEnumerable<Person>)_byOrderOfHiring).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count
    {
        get { return this._byName.Count; }
    }

    public bool Contains(Person person)
    {
        return _byName.ContainsKey(person.Name);
    }

    public bool ContainsByName(string name)
    {
        return _byName.ContainsKey(name);
    }

    public void Add(Person person)
    {
        if (person != null)
        {
            string name = person.Name != null ? person.Name : string.Empty;
            int nameLength = person.Name != null ? person.Name.Length : 0;

            //add to order of hiring
            this._byOrderOfHiring.Add(person);

            //add for name search
            if (!_byName.ContainsKey(name)) //no pesho bag inside
            {
                _byName.Add(name, new List<Person>() { person });
            }
            else //pesho bag exists already
            {
                _byName[name].Add(person);
            }

            //add for name length search
            if (!_byNameLength.ContainsKey(nameLength)) //no 5 bag inside
            {
                _byNameLength.Add(nameLength, new List<Person>() { person });
            }
            else //5 bag exists already
            {
                _byNameLength[nameLength].Add(person);
            }

        }
    }

    public Person GetAtIndex(int index)
    {
        if (index >= 0 && index <= this._byOrderOfHiring.Count - 1)
        {
            return this._byOrderOfHiring[index];
        }
        else
        {
            throw new IndexOutOfRangeException();
        }   
    }

    public IEnumerable<Person> GetByName(string name)
    {
        if (!this._byName.ContainsKey(name))
        {
            return Enumerable.Empty<Person>();
        }

        return this._byName[name];
    }

    public IEnumerable<Person> FirstByInsertOrder(int count = 1)
    {
        if (count > 0 && count <= this._byOrderOfHiring.Count)
        {
            return this._byOrderOfHiring.Take(count); //could be slow
        }
        else if (count > 0 && count > this._byOrderOfHiring.Count)
        {
            return this._byOrderOfHiring;
        }
        else
        {
            throw new IndexOutOfRangeException();
        }
    }

    public IEnumerable<Person> SearchWithNameSize(int minLength, int maxLength)
    {
        var range = this._byNameLength.Range(minLength, true, maxLength, true);

        foreach (var kvp in range)
        {
            foreach (var person in kvp.Value)
            {
                yield return person;
            }
        }
    }

    public IEnumerable<Person> GetWithNameSize(int length)
    {
        if (!this._byNameLength.ContainsKey(length))
        {
            return Enumerable.Empty<Person>();
        }

        return this._byNameLength[length];
    }

    public IEnumerable<Person> PeopleByInsertOrder()
    {
        return this._byOrderOfHiring;
    }
}