using System;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace eShopEF
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Pending seed fixing

            //using (var Context = new RepositoryContext()) 
            //{
            //    try
            //    {
            //        await RepositoryContextSeed.SeedAsync(Context);
            //    }
            //    catch(Exception e)
            //    {
            //        Console.WriteLine(e.Message);
            //    }
            //}

            eShopConsole console = new eShopConsole();

            while (true)
                if (console.PrincipalMenu())
                    break;
        }
    }
}
