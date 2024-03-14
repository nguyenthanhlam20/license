using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class District 
    {
        [Key]
        public int DistrictId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Prefix { get; set; } = string.Empty;

        #region Relationship
        public virtual ICollection<LicensePlate>? LicensePlates { get; set; }
        #endregion
    }
}
