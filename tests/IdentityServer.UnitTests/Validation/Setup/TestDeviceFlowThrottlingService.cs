using System.Threading.Tasks;
using dbosoft.IdentityServer.Services;
using dbosoft.IdentityServer.Storage.Models;

namespace IdentityServer.UnitTests.Validation.Setup
{
    public class TestDeviceFlowThrottlingService : IDeviceFlowThrottlingService
    {
        private readonly bool shouldSlownDown;

        public TestDeviceFlowThrottlingService(bool shouldSlownDown = false)
        {
            this.shouldSlownDown = shouldSlownDown;
        }

        public Task<bool> ShouldSlowDown(string deviceCode, DeviceCode details) => Task.FromResult(shouldSlownDown);
    }
}