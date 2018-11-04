using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Anything
    {
        private int data, data2; //field

        public Anything() => data = default(int);

        public int Data { get; set; }  // Auto Implemented Property. // C# 6.0 later, public int Data { get; set; } = 0;
    }

    public class GenericParentClass<T>
    {
        public static void StaticMethod(T data)
        {
            // do some job
        }

        public void InstanceMethod(T data)
        {
            // do some job
        }
    }

    public sealed class UsefulController<T> : GenericParentClass<T> where  T : Anything, new() // The new constraint specifies that any type argument in a generic class declaration must have a public parameterless constructor.
    {
        // All static public methods must be placed before all non-static public methods. [StyleCop Rule: SA1204]
        public static new void StaticMethod(T data)  // 'UsefulController'.StaticMethod(Anything) hides inherited member 'GenericParentClass<Anything>.StaticMethod(Anything)'. Use the new keyword if hiding was intended.
        {
            GenericParentClass<T>.StaticMethod(data);  // 'data' is a variable but used like a type //arugement type T is not assignable to parameter type 'data'.
        }

        public void EncapsulatedStaticMethod()
        {
            T @class = new T(); // Cannot create an instance of the variable type T because it does not have the new() constraint. //T is type and @class is variable and new is an instance. //Complier doesn't know exactly what type T is refering to, all it knows it T should be Type 'Anything'. if you want to instantiate, you need default construtor.
            StaticMethod(@class);  
        }

        public void EncapsulatedInstanceMethod(T data)
        {
            base.InstanceMethod(data);
        }
    }

    public class Container
    {
        public UsefulController<Anything>  B { get; set; }
    }

    public class Testing   // Testing the post on https://stackoverflow.com/questions/809550/c-sharp-compiler-cannot-access-static-method-in-a-non-static-context?answertab=oldest#tab-top
    // Used Reference Links: https://stackoverflow.com/questions/3419176/how-to-use-the-where-keyword-in-c-sharp-with-a-generic-interface-and-inherita
    //https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-constraint
    //
    {
        public static void Main()
        {
            Anything @var = new Anything();
            var c = new Container();
            c.B.InstanceMethod(null);   
            c.B.EncapsulatedStaticMethod();    
            c.B.EncapsulatedInstanceMethod(var);  
        }
    }

    //public interface IXyzable { void xyz(); }

    //public class MyGenericClass<T> : IXyzable where T : IXyzable
    //{
    //    T obj;
    //    public void xyz()
    //    {
    //        obj.xyz();
    //    }
    //}
}
