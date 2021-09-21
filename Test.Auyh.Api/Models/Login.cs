using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Auyth.Api.Models
{
    public class Login
    {
        [Required]
        [MaxLength(255)]
        public string UserLogin { get; set; }
        [Required]
        [MaxLength(255)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
