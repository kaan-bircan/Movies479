#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Role
    {
        public int Id { get; set; } // this is called a property in C# which contains getters and setters 

        [Required]
        [StringLength(5, MinimumLength = 4)]
        public string Name { get; set; } 

        public List<User> Users { get; set; }
    }
}
