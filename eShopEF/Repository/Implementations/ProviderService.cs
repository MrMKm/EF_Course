using Entities;
using Entities.Models;
using Repository;
using Repository.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class ProviderService : IProviderService
    {
        private readonly RepositoryContext repositoryContext;

        public ProviderService(RepositoryContext _repositoryContext)
        {
            repositoryContext = _repositoryContext;
        }

        public void CreateProvider(Provider provider)
        {
            repositoryContext.Provider.Add(provider);
            repositoryContext.SaveChanges();
        }

        public Provider GetProviderByID(int ProviderID)
        {
            return repositoryContext.Provider
                .FirstOrDefault(p => p.ID.Equals(ProviderID));
        }

        public List<Provider> GetProviders()
        {
            return repositoryContext.Provider.ToList();
        }
    }
}
