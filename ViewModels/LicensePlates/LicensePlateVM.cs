using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.LicensePlates
{
    public class LicensePlateVM
    {
        public int? LicensePlateId { get; set; }
        public string LicensePlateNumber { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public Guid? Id { get; set; }
        public int? DistrictId { get; set; }
        public int SeriesId { get; set; }
        public int Number { get; set; }
        public Account? Account { get; set; }
        public  District? District { get; set; }
    }
}
