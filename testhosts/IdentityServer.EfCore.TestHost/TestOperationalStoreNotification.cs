using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dbosoft.IdentityServer.EfCore.Storage.Entities;
using Dbosoft.IdentityServer.EfCore.Storage.TokenCleanup;

namespace Dbosoft.IdentityServer.EfCore.TestHost
{
    public class TestOperationalStoreNotification : IOperationalStoreNotification
    {
        public TestOperationalStoreNotification()
        {
            Console.WriteLine("ctor");
        }

        public Task PersistedGrantsRemovedAsync(IEnumerable<PersistedGrant> persistedGrants)
        {
            foreach (var grant in persistedGrants)
            {
                Console.WriteLine("cleaned: " + grant.Type);
            }
            return Task.CompletedTask;
        }

        public Task DeviceCodesRemovedAsync(IEnumerable<DeviceFlowCodes> deviceCodes)
        {
            foreach (var deviceCode in deviceCodes) 
            {
                Console.WriteLine("cleaned device code");
            }
            return Task.CompletedTask;
        }
    }
}
