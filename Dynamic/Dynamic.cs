using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace Dynamic
{
    internal class MyDynamicObject : DynamicObject
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return dictionary.TryGetValue(binder.Name, out result);
            //binder.Name is just the name of the undefined property. If the property name is a valid key(like Age), then the corresponding value is put into result.
            //else, TryGetValue returns false if unsuccessful(since the undefined property wasn't declared before).
            //By returning false, it gets DynamicObject to throw an exception.
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            dictionary[binder.Name] = value;
            return true;
            //adds the new undefined property to the dictionary along with its value. return true when successful.
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            try
            {
                Func<object[]> method = (Func<object[]>)dictionary[binder.Name];
                result = method();
                return true;
            }

            catch
            {
                result = null;
                return false;
            }
            
        }

    }

    class Test
    {
        static void Main()
        {
            dynamic person = new MyDynamicObject();
            person.Age = 21; //tries to create and set value of undefined property(calls TrySetMember, which does dictionary.Add(Age, 21);)
            Console.WriteLine(person.Age); //tries to get value of undefined property(calls TryGetMember. Successful because Age and its value was already set previously)
            person.WriteAge = (Action<int>)((input) => Console.WriteLine(input));
            person.WriteAge(person.Age);
            dynamic guy = new ExpandoObject(); //ExpandoObject is normally used over creating a custom dynamic object
            guy.X = 1;
            guy.Y = 2;
            guy.Addition = (Func<int, int, int>)((first, second) => first + second);
            Console.WriteLine(guy.Addition(guy.X, guy.Y));
            guy.Hi = (Action)(() => Console.WriteLine("Hi"));
            guy.Hi();
        }
        
    }
}

