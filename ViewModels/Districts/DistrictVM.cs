using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.LicensePlates;

namespace ViewModels.Districts
{
    public class DistrictVM
    {
        public int? DistrictId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Prefix { get; set; } = string.Empty;

        public List<LicensePlateVM>? LicensePlates { get; set; } 
    }
}
