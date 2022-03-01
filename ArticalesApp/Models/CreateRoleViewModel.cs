using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArticalesApp.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
