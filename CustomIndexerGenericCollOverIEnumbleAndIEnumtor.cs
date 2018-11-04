using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    // Reference Links:
    // http://dotnetpattern.com/csharp-interface   Interface members access modifiers section.
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers/   Expression Body Definitions section.
    // https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=netframework-4.7.2   

    public class CustIndexerColl_Generic <T>
    {
        private T[] arr = new T[1];
        private int currentIndex = -1;
        public CustIndexerColl_Generic() => IndexProperty = 0;   // Expression-bodied Constructor // Can only consists of a single expression. // default for access-modifier is private.
        
        //Ms also uses indexer in namespace System.Collections.Generic.List<T>.
        public T this[int i]
        {
            get => this.arr[i];
            set => this.arr[i] = value;
        }

        public int IndexProperty { get => this.currentIndex; private set => this.currentIndex = value; }
        
        public int Length() => this.arr.Length;

        public void Add(T v)
        {
            if (IndexProperty >= this.Length())
            {
                T[] temp = arr;
                arr = new T[IndexProperty + 1];
                Array.Copy(temp, this.arr, temp.Length);
            }
            // DEL: throw new IndexOutOfRangeException($"The collection can hold only {this.Length()} elements.");

            arr[IndexProperty++] = v;
        }
    }

    // Expost an Enumtor, which supports a simple iteration over a non-generic collection. 
    // BUT THIS EXAMPLE IS USING CUSTOMIZED INDEXED GENERIC COLL. AM I CRAZY?
    public class CustIEnumbleImpl<T> : IEnumerable
    {
        private CustIndexerColl_Generic<T> _HiddenVar;   //_var means local.

        public CustIEnumbleImpl(CustIndexerColl_Generic<T> copiedArgValueType)
        {
            this._HiddenVar = copiedArgValueType;
        }

        public CustEnumtor<T> GetCustEnumtor()
        {
            return new CustEnumtor<T> (this._HiddenVar);
        }

        // #3rd Version of GetEnumerator().
        // Explicit implemention of Interface member = keyword 'Interface.IMethod()/IProperty'. No Access Modifier is allowed.
        // Example use is when implmenting two separate Interface with same method. E.g. I1.Method() {} , I2.Method() {} in same class.
        IEnumerator IEnumerable.GetEnumerator()
        {
            var CustGetEnumtor = this.GetCustEnumtor();
            return (IEnumerator) CustGetEnumtor;
        }
    }

    //implementing IEnumerator which has Method: MoveNext(), Reset() and using IEnumerator's Property: Current.
    public class CustEnumtor<T> : IEnumerator
    {
        public CustIndexerColl_Generic<T> _var;

        public int pos = -1;

        public CustEnumtor(CustIndexerColl_Generic<T> Arg) => this._var = Arg;

        public bool MoveNext()
        {
            this.pos++;
            return (this.pos < this._var.Length());
        }

        public void Reset() => this.pos = -1;

        // Explicit implemention of Interface member = keyword 'Interface.IMethod()/IProperty'.  
        // This is also regarded as the Implemented Interface's member in Implementing Class. No Access Modifier is allowed.
        object IEnumerator.Current => _Current;   

        public T _Current
        {
            get
            {
                try
                {
                    return this._var[pos];
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e);
                    throw new InvalidOperationException();
                }
            }
        } 
    }

    class Program
    {
        //anything declared within Main is static.
        static void Main()
        {
            var T = "".GetType();
            Console.WriteLine(T);

            var mysteriousColl = new CustIndexerColl_Generic<string>();
            mysteriousColl[0] = "Hello, World.";
            mysteriousColl.Add("String");
            mysteriousColl.Add("String2");
            mysteriousColl.Add("String3");

            Console.WriteLine($"Custom Collection Length: {mysteriousColl.Length()}");

            CustIEnumbleImpl<string> b = new CustIEnumbleImpl<string> (mysteriousColl);
            foreach ( string s in b)
                Console.WriteLine($"{s}");
        }
    }
}
