using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Asynchronous
{
    class Test
    {
        static void Main()
        {
            Test test = new Test();
            test.MakeBread();
            Console.WriteLine("Toasting Bread"); //This section is running at the same time as Toaster or SpreadJam
            Thread.Sleep(13000); 
            Console.WriteLine("Bread should be toasted, jam not yet spread.");
            while (true) ;
        }

        async Task MakeBread()
        {
            string jam = await Task.Run(() => Toaster()); //new thread created via Task.Run to run Toaster. Current thread stops executing MakeBread, goes back to Main() to continue.
            Task.Run(() => SpreadJam(jam)); //When Toaster is finished, await calls the current thread back to run the new thread SpreadJam.
            Console.WriteLine("Spreading...");
        }

         async Task<string> Toaster()
        {
            Thread.Sleep(7000);
            Console.WriteLine("Toasted Bread");
            return "strawberry";
        }

         async Task SpreadJam(string Jam)
        {
            Thread.Sleep(7000);
            Console.WriteLine(Jam + " jam spread");

        }
    }
}

