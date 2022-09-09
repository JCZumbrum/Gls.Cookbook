using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Repositories;

namespace Gls.Cookbook.DataAccess
{
    public class EfCookbookContextFactory : ICookbookContextFactory
    {
        public async Task<ICookbookContext> CreateAsync()
        {
            return await EfCookbookContext.CreateAsync();
        }
    }
}
