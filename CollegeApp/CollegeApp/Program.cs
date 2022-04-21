using Entities;
using System;
using CollegeApp;

using (var repositoryContext = new RepositoryContext()) 
{
    try
    {
        await RepositoryContextSeed.SeedAsync(repositoryContext);
    }
    catch(Exception e)
    {
        Console.WriteLine(e.Message);
        return;
    }
}


var Menu = new Menu();

while(Menu.Show()) { }

