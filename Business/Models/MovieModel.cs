#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        public short? Year { get; set; }
        public double Revenue { get; set; }
        public int? DirectorId { get; set; }
        public int Guid { get; set; }
    }
}
