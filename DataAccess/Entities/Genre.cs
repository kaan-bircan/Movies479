#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Genre : Record
    {
        [Required]
        [StringLength(75)]
        public string Name { get; set; }
        public List<MovieGenre> MovieGenres { get; set; }
    }
}
