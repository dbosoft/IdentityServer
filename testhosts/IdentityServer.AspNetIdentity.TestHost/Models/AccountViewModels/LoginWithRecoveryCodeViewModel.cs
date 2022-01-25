using System.ComponentModel.DataAnnotations;

namespace Dbosoft.IdentityServer.AspNetIdentity.TestHost.Models.AccountViewModels
{
    public class LoginWithRecoveryCodeViewModel
    {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Recovery Code")]
            public string RecoveryCode { get; set; }
    }
}
