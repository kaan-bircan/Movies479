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
	public class DirectorModel : Record
	{

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[Required]
		[StringLength(50)]
		public string Surname { get; set; }

		public DateTime? BirthDate { get; set; }
		public bool IsRetired { get; set; }

		[DisplayName("Retired Statue")]
        public string IsRetiredOutput { get; set; }
    }
}
