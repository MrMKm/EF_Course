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
        private List<Provider> providersList = TestData.ProvidersList;

        public void CreateProvider(Provider provider)
        {
            providersList.Add(provider);
        }

        public Provider GetProviderByID(int ProviderID)
        {
            return providersList
                .FirstOrDefault(p => p.ID.Equals(ProviderID));
        }

        public List<Provider> GetProviders()
        {
            return providersList;
        }
    }
}
