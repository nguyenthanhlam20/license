namespace DataAccess.Models
{
    public class Seri
    {
        public int SeriId { get; set; } 
        public string? Title { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<LicensePlate>? LicensePlates { get; set; }
    }
}
