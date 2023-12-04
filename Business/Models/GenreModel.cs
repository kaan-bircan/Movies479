#nullable disable
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class GenreModel : Record
    {
        [Required]
        [StringLength(75)]
        public string Name { get; set; }

    }
}
