using DataAccess.Models;

namespace ViewModels.Series
{
    public class SeriVM
    {
        public int SeriId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public  List<LicensePlate>? LicensePlates { get; set; }
    }
}
