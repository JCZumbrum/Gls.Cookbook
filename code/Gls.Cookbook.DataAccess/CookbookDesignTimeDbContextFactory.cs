using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Gls.Cookbook.DataAccess
{
    internal class CookbookDesignTimeDbContextFactory : IDesignTimeDbContextFactory<CookbookDbContext>
    {
        public CookbookDbContext CreateDbContext(string[] args)
        {
            return CookbookDbContext.Create("..\\");
        }
    }
}