using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BankManager_Csharp_Console
{
    class Program
    {
        public static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {

            new BankManagerCLI().run();

        }

    }
}