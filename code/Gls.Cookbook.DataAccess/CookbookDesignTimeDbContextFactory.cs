using System;
using Microsoft.EntityFrameworkCore.Design;

namespace Gls.Cookbook.DataAccess
{
    public class CookbookDesignTimeDbContextFactory : IDesignTimeDbContextFactory<CookbookDbContext>
    {
        public CookbookDbContext CreateDbContext(string[] args)
        {
            return CookbookDbContext.CreateAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}