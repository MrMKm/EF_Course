using Business;
using Business.Services.Implementations;
using Data.Entities;
using System;
using System.Linq;

namespace eShop
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
