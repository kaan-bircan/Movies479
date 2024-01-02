#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class RoleModel
    {
        #region Properties copied from the related entity
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(5, MinimumLength = 4, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        public string Name { get; set; }
        #endregion



        #region Extra properties required for the views
        [DisplayName("User Count")]
        public int UserCountOutput { get; set; }
        #endregion
    }
}
