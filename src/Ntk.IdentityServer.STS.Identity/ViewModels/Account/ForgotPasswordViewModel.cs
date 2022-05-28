using System.ComponentModel.DataAnnotations;
using Ntk.IdentityServer.Shared.Configuration.Configuration.Identity;

namespace Ntk.IdentityServer.STS.Identity.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        public LoginResolutionPolicy? Policy { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

        public string Username { get; set; }
    }
}
