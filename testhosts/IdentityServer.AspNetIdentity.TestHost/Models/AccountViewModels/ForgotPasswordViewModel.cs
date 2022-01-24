using System.ComponentModel.DataAnnotations;

namespace IdentityServer.AspNetIdentity.TestHost.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
