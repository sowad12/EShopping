using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Library.Domains.Commands.Account
{
    public class ChangePasswordCommand
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [Required]
        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }

        [Required]
        [JsonProperty("currentPassword")]
        public string CurrentPassword { get; set; }
    }
}
