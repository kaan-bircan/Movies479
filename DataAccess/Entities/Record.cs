using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public abstract class Record
    {
        public int Id { get; set; }
        public string? Guid { get; set; }
    }
}
