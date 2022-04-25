using System;
using System.Linq;

namespace eShopEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            eShopConsole console = new eShopConsole();

            while (true)
                if (console.PrincipalMenu())
                    break;
        }
    }
}
