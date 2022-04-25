using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IProviderService
    {
        public List<Provider> GetProviders();
        public Provider GetProviderByID(int ProviderID);
        public void CreateProvider(Provider provider);
    }
}
