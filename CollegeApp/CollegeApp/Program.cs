using Entities;
using System;
using CollegeApp;

using (var repositoryContext = new RepositoryContext()) { }


var Menu = new Menu();

while(Menu.Show()) { }

