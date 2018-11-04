using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApp1
{
    // Reference Links:
    // http://dotnetpattern.com/csharp-interface   Interface members access modifiers section.
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers/   Expression Body Definitions section.
    // https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=netframework-4.7.2   

    public class NonGenericCollOverIEnumbleAndIEnumtor
    {
        public class Person
        {
            public Person(string fName, string lName)
            {
                this.firstName = fName;
                this.lastName = lName;
            }

            public string firstName, lastName;
        }

        //Elements defined in a namespace cannot be explicitly declared as private, protected, protected internal, or private protected. Resolve: Change this Impl_class to public.
        public class People : IEnumerable
        {
            private Person[] _people;
            public People(Person[] personArray)
            {
                _people = personArray;
            }

            // #1st Version of GetEnumerator().
            // Auto implemented method().  implicit implemention of Interface IEnumerable's GetEnumerator (normal implementation).
            //public IEnumerator GetEnumerator()
            //{
            //    return _people.GetEnumerator();   // IntelliSence return an IEnumerator for an Array.
            //}

            // #2nd Version of GetEnumerator().
            // Explicit implemention of Interface member = keyword 'Interface.IMethod()/IProperty'. No Access Modifier is allowed.
            // Example use is when implmenting two separate Interface with same method. E.g. I1.Method() {} , I2.Method() {} in same class.
            //IEnumerator IEnumerable.GetEnumerator()
            //{
            //    return _people.GetEnumerator();   
            //}

            // #3rd Version of GetEnumerator().
            public PeopleEnum customEnumeratorAndGetEnumerator()
            {
                return new PeopleEnum(_people);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)customEnumeratorAndGetEnumerator();
            }

        }

        //implementing IEnumerator which has Method: MoveNext(), Reset() and using IEnumerator's Property: Current.
        public class PeopleEnum : IEnumerator
        {
            public Person[] _people;
            int position = -1;

            public PeopleEnum(Person[] list)
            {
                _people = list;
            }

            public bool MoveNext()
            {
                position++;
                return (position < _people.Length);
            }

            public void Reset() => this.position = -1;

            //Explicit interface implementation 'PeopleENum.IEnumerator.Current' is missing accessor 'IEnumerator.Current.get'.
            object IEnumerator.Current
            {
                get { return Current; }
            }

            public Person Current
            {
                get
                {
                    try
                    {
                        return _people[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
        
        //anything declared within Main is static.
        static void Main()
        {
            Person[] persons = new Person[3]
               {
                   new Person("John", "Smith"),
                   new Person("Jim", "Johnson"),
                   new Person("Sue", "Rabon")
               };

            People peopleList = new People(persons);
            foreach (Person p in peopleList)
            {
                Console.WriteLine(p.firstName + ' ' + p.lastName);
            }
        }
    }   
}
