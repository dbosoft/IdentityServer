using System.ComponentModel.DataAnnotations;

namespace IdentityServer.AspNetIdentity.TestHost.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
