using System.ComponentModel.DataAnnotations;

namespace Dbosoft.IdentityServer.AspNetIdentity.TestHost.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
