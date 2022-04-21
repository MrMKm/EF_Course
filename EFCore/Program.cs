using EFCore;
using EFCore.Data;
using System;
using System.Linq;

using (var repositoryContext = new RepositoryContext()) 
{
    FirstSteps(repositoryContext);
}

 void FirstSteps(RepositoryContext repositoryContext)
{
    //Create
    //Console.WriteLine("Inserting new blog...");
    //repositoryContext.Blogs.Add(new Blog { Url = "https://blog2.com" });
    //repositoryContext.SaveChanges();

    //Read
    Console.WriteLine("Querying for blog...");
    var blog = repositoryContext.Blogs.First();
    Console.WriteLine(blog.Url);

    //Update
    Console.WriteLine("Update for blog...");
    blog.Url = "https://blog2.com";
    blog.Posts.Add(new Post
    {
        BlogID = blog.ID,
        Title = "Hello World",
        Content = "..."
    });
    repositoryContext.SaveChanges();
}
